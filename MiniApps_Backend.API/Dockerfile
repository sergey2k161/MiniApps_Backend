# Базовый образ, который используется для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 443  # Открываем порт 443 для HTTPS

# Образ для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MiniApps_Backend.API/MiniApps_Backend.API.csproj", "MiniApps_Backend.API/"]
COPY ["MiniApps_Backend.Bot/MiniApps_Backend.Bot.csproj", "MiniApps_Backend.Bot/"]
COPY ["MiniApps_Backend.Business/MiniApps_Backend.Business.csproj", "MiniApps_Backend.Business/"]
COPY ["MiniApps_Backend.Abstractions/MiniApps_Backend.Abstractions.csproj", "MiniApps_Backend.Abstractions/"]
COPY ["MiniApps_Backend.DataBase/MiniApps_Backend.DataBase.csproj", "MiniApps_Backend.DataBase/"]
RUN dotnet restore "./MiniApps_Backend.API/MiniApps_Backend.API.csproj"
COPY . . 
WORKDIR "/src/MiniApps_Backend.API"
RUN dotnet build "./MiniApps_Backend.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Стадия публикации
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiniApps_Backend.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальная стадия (для работы в продакшн)
FROM base AS final
WORKDIR /app

# Копируем публикацию из предыдущего этапа
COPY --from=publish /app/publish .

# Копируем сертификаты в контейнер
COPY ./certs /https:ro  # Предполагаем, что сертификаты находятся в директории ./certs на хосте

# Настроим контейнер на прослушивание HTTPS
ENV ASPNETCORE_URLS="https://+:443"

# Конфигурируем Kestrel для использования сертификатов
RUN mkdir /https && \
    chmod 777 /https

# Запуск приложения через HTTPS
ENTRYPOINT ["dotnet", "MiniApps_Backend.API.dll"]
