# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RainTrackerApi/RainTrackerApi.csproj", "RainTrackerApi/"]
RUN dotnet restore "./RainTrackerApi/RainTrackerApi.csproj"
COPY . .
WORKDIR "/src/RainTrackerApi"
RUN dotnet build "./RainTrackerApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RainTrackerApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

# Install Postgres client for pg_isready
RUN apt-get update && \
    apt-get install -y postgresql-client && \
    rm -rf /var/lib/apt/lists/*
COPY RainTrackerApi/wait-for-postgres.sh /wait-for-postgres.sh
RUN chmod +x /wait-for-postgres.sh
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_URLS=http://+:8080
ENTRYPOINT ["/wait-for-postgres.sh"]
EXPOSE 8080
