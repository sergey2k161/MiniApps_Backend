# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7137


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MiniApps_Backend.API/MiniApps_Backend.API.csproj", "MiniApps_Backend.API/"]
COPY ["MiniApps_Backend.Bot/MiniApps_Backend.Bot.csproj", "MiniApps_Backend.Bot/"]
COPY ["MiniApps_Backend.Business/MiniApps_Backend.Business.csproj", "MiniApps_Backend.Business/"]
COPY ["MiniApps_Backend.Abstractions/MiniApps_Backend.Abstractions.csproj", "MiniApps_Backend.Abstractions/"]
COPY ["MiniApps_Backend.DataBase/MiniApps_Backend.DataBase.csproj", "MiniApps_Backend.DataBase/"]

# Копируем сертификат
COPY https/devcert.pfx /https/devcert.pfx

RUN dotnet restore "./MiniApps_Backend.API/MiniApps_Backend.API.csproj"
COPY . .
WORKDIR "/src/MiniApps_Backend.API"
RUN dotnet build "./MiniApps_Backend.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiniApps_Backend.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniApps_Backend.API.dll"]