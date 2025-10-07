using Microsoft.AspNetCore.Mvc.Testing;
using RainTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace RainTrackerApi.Tests
{
    public class RainTrackerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "/api/data";

        public RainTrackerIntegrationTests(WebApplicationFactory<Program> webApplicationFactory)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8080/swagger") };
        }

        private HttpRequestMessage ArrangeValidPostRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _url);
            request.Headers.Add("x-userId", "testuser");
            request.Content = JsonContent.Create(new { ItRained = true });
            return request;
        }

        [Fact]
        public async Task PostRainData_ValidPayload_ReturnsCreated()
        { 
            var request = ArrangeValidPostRequest();

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PostRainData_InvalidPayload_ReturnBadRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _url);
            request.Headers.Add("x-userId", "testuser");
            request.Content = JsonContent.Create(new { ItRained = "maybe" });
            
            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostRainData_MissingHeader_ReturnsBadRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _url);
            request.Content = JsonContent.Create(new { ItRained = false });

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetRainData_ValidUser_ReturnsData()
        {
            var request = ArrangeValidPostRequest();
            await _httpClient.SendAsync(request);
            
            var getRequest = new HttpRequestMessage(HttpMethod.Get, _url);
            getRequest.Headers.Add("x-userId", "testuser");
            var response = await _httpClient.SendAsync(getRequest);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task GetRainData_ReturnsEmptyArray_WhenNoDataForUser()
        {
            var getRequest = new HttpRequestMessage(HttpMethod.Get, _url);
            getRequest.Headers.Add("x-userId", "nouser");
            var response = await _httpClient.SendAsync(getRequest);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("[]", content);
        }
    }
}
