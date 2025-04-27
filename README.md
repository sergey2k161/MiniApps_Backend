
# MiniApps Backend — Настройка проекта

## Шаги для запуска проекта

### 1. Настройка конфигурации
Откройте файлы:

- `appsettings.json`
- `appsettings.Development.json`

И укажите в них свои данные:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=*****;Database=*****;Username=*****;Password=****",
    "Redis": "redis:6379"
  },
  "Jwt": {
    "Key": "**************************************************************",
    "Issuer": "MiniApp",
    "Audience": "MiniApp"
  },
  "TelegramBotToken": "*****************************",
  "AllowedHosts": "*"
}
```

> Укажите реальные значения для подключения к базе данных, Redis, JWT ключ и Telegram Bot токен.

---

### 2. Настройка CORS

Перейдите в файл:

```
MiniApps_Backend.API -> Extensions -> CorsExtension
```

И измените URL на адрес вашего фронтенд-клиента:

```csharp
policy.WithOrigins("https://ваш-домен.com")
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowCredentials();
```

---

### 3. Обновление ссылки в меню бота

Перейдите в файл:

```
MiniApps_Backend.Bot -> Handlers -> BotMenu
```

В методе `GetMiniAppButton` замените ссылку на вашу:

```csharp
new InlineKeyboardButton[]
{
    InlineKeyboardButton.WithWebApp(
        text: "Открыть MiniApp 🚀",
        webApp: new WebAppInfo { Url = "https://ваш-домен.com/miniapp" }
    )
});
```

---

### 4. Проверка перед запуском

- Проверьте корректность данных подключения к базе данных.
- Убедитесь, что сервер Redis запущен и доступен.
- Миграции базы данных будут применены автоматически при старте проекта.

---

