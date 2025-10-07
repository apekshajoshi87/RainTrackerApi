using FluentMigrator.Runner;
using Microsoft.AspNetCore.Mvc;
using RainTrackerApi.Data.DataProviders;
using RainTrackerApi.Service;
using RainTrackerApi.ServiceProviders;
using System.Reflection;

namespace RainTrackerApi;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IRainRecordServiceProvider, RainRecordServiceProvider>();
        builder.Services.AddScoped<IRainRecordDataProvider, RainRecordDataProvider>();

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddFluentMigratorCore()
            .ConfigureRunner(r => r
            .AddPostgres()
            .WithGlobalConnectionString(builder.Configuration.GetConnectionString("database"))
            .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations()
            )
            .AddLogging(l => l.AddFluentMigratorConsole());
        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
