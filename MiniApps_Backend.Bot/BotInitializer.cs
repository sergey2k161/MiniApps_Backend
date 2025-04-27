using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.Bot.Handlers;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.Business.Services.Logic;
using System.Threading;

namespace MiniApps_Backend.Bot
{
    /// <summary>
    /// Инициализация и запуск бота
    /// </summary>
    public class BotInitializer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITelegramBotClient _botClient;

        private static readonly Dictionary<long, UserState> _userStates = new();

        private static string? Phone;
        private static string? RealLastName;
        private static string? RealFirstName;

        public BotInitializer(IServiceProvider serviceProvider, ITelegramBotClient botClient)
        {
            _serviceProvider = serviceProvider;
            _botClient = botClient;
        }

        /// <summary>
        /// Основной метод для запуска бота, слушает обновления и обрабатывает их.
        /// </summary>
        /// <param name="stoppingToken">Токен отмены</param>
        /// <returns>Задача завершения работы</returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() 
            };

            var cts = new CancellationTokenSource();

            _botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                errorHandler: HandleErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Обрабатывает обновления от пользователя, такие как текстовые сообщения или контактные данные
        /// </summary>
        /// <param name="client">Клиент бота</param>
        /// <param name="update">Обновление от Telegram</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Задача обработки обновления</returns>
        public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message || message.From is not { } user) return;

            if (message.Type == MessageType.Text)
            {
                await HandleTextMessageAsync(client, message, user, cancellationToken);
            }
            else if (message.Type == MessageType.Contact)
            {
                await HandleContactMessageAsync(client, message, cancellationToken);
            }
            else
            {
                await client.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Я понимаю только текст или контакт.",
                    cancellationToken: cancellationToken);
            }
        }

        /// <summary>
        /// Обрабатывает текстовые сообщения от пользователей.
        /// </summary>
        /// <param name="client">Клиент бота.</param>
        /// <param name="message">Сообщение от пользователя.</param>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Задача обработки текстового сообщения.</returns>
        public async Task HandleTextMessageAsync(ITelegramBotClient client, Message message, Telegram.Bot.Types.User user, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;
            var userId = user.Id;

            using var scope = _serviceProvider.CreateScope();
            var _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var _analyticsService = scope.ServiceProvider.GetRequiredService<IAnalyticsService>();
            var _supportService = scope.ServiceProvider.GetRequiredService<ISupportService>();


            //await _analyticsService.LogActionAsync((message.Type).ToString(), "Обработано", userId);

            if (_userStates.TryGetValue(chatId, out var state))
            {
                switch (state)
                {
                    case UserState.AwaitingRealFirstName:
                        await HandleRealFirstName(client, message, chatId, user);
                        return;

                    case UserState.AwaitingRealLastName:
                        await HandleRealLastName(client, message, chatId);
                        return;

                    case UserState.AwaitingPhone:
                        await HandlePhone(client, message, chatId);
                        break;

                    case UserState.AwaitingEmail:
                        await HandleEmail(client, message, chatId, userId, _userService, cancellationToken);
                        break;

                    case UserState.Welcome:
                        await SendMainMenu(client, chatId, cancellationToken);
                        _userStates[chatId] = UserState.MainMenu;
                        break;

                    case UserState.MainMenu:
                        break;

                    case UserState.AwaitingSupportMessage:
                        if (message.Text == "Отмена")
                        {
                            await client.SendMessage(chatId,
                                "Отмена обращения в техническую поддержку.",
                                cancellationToken: cancellationToken);
                            _userStates[chatId] = UserState.MainMenu;
                            return;
                        }

                        if (message.Text.Length < 50)
                        {
                            await client.SendMessage(chatId,
                                "Минимальная длина сообщения — 50 символов. Пожалуйста, опишите проблему подробнее.",
                                cancellationToken: cancellationToken);
                            return;
                        }
                        await _supportService.CreateSupport(chatId, message.Text);
                        

                        await client.SendMessage(chatId,
                            "Спасибо! Ваше сообщение отправлено в техническую поддержку. Мы свяжемся с вами при необходимости.",
                            cancellationToken: cancellationToken);

                        _userStates[chatId] = UserState.MainMenu;
                        return;


                    default:
                        await SendMainMenu(client, chatId, cancellationToken);

                        await client.SendMessage(chatId,
                            "Не понимаю команду. Введите /help для списка доступных команд.",
                            cancellationToken: cancellationToken);
                        break;
                }
            }

            // Команды
            switch (message.Text)
            {
                case "/start":
                    await _analyticsService.LogActionAsync("Команда /start", "Обработано", userId);
                    Console.WriteLine(userId);
                    var userClient = _userService.GetUserByTelegramId(userId);
                    Console.WriteLine(userClient);
                    if (userClient.Result != null)
                    {
                        await client.SendMessage(
                            chatId,
                            "<b>Вы уже зарегистрированы.Введите /help для списка доступных команд.</b>",
                            parseMode: ParseMode.Html,
                            cancellationToken: cancellationToken);
                        return;
                    }

                    await client.SendMessage(
                        chatId,
                        "<b>Добро пожаловать!</b>\n\n" +
                        "Пожалуйста, введите ваше Имя (Например: Иван)",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
                    _userStates[chatId] = UserState.AwaitingRealFirstName;

                    break;

                case "/help":
                case "ℹ️ Список команд":
                    await _analyticsService.LogActionAsync("Команда /help", "Обработано", userId);
                    await client.SendMessage(
                        chatId,
                        "📌 <b>Список команд:</b>\n\n" +
                        "🔹 /start — регистрация\n" +
                        "🔹 /app — MiniApp\n" +
                        "🔹 /faq — часто задаваеммые вопросы\n" +
                        "🔹 /support — обращение в техническую поддержку\n" +
                        "🔹 /help — список команд\n",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);

                    break;

                case "/app":
                case "📲 MiniApp":
                    await _analyticsService.LogActionAsync("Команда /app", "Обработано", userId);
                    await client.SendMessage(
                        chatId, 
                        "Нажми на кнопку ниже, чтобы открыть MiniApp 📲", 
                        replyMarkup: BotMenu.GetMiniAppButton(), 
                        cancellationToken: cancellationToken);
                    break;

                case "/faq":
                case "📊 FAQ":
                    await _analyticsService.LogActionAsync("Команда /faq", "Обработано", userId);
                    await client.SendMessage(
                        chatId,
                        "📚 <b>Часто задаваемые вопросы</b>:\n\n" +
                        "❓ <b>Как получить доступ к приложению?</b>\n" +
                        "🔹 Введите команду: /start\n\n" +
                        "❓ <b>Как записаться на курс?</b>\n" +
                        "🔹 Перейдите в раздел курсов в приложении, выберите нужный и нажмите «Подписаться»\n\n" +
                        "❓ <b>Что делать, если возникли технические проблемы?</b>\n" +
                        "🔹 Введите команду: /support и опишите вашу проблему\n\n" +
                        "❓ <b>Можно ли обучаться с телефона?</b>\n" +
                        "🔹 Да, приложение адаптировано для мобильных устройств\n\n" +
                        "❓ <b>Где посмотреть прогресс?</b>\n" +
                        "🔹 В разделе <b>«Профиль»</b> внутри приложения",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);

                    
                    break;


                case "/support":
                case "🆘 Техническая поддержка":
                    await _analyticsService.LogActionAsync("Команда /support", "Обработано", userId);
                    await client.SendMessage(
                        chatId,
                        "Пожалуйста, опишите вашу проблему (минимум 50 символов).\n\n" +
                        "Для отмены введите: Отмена",
                        cancellationToken: cancellationToken);
                        _userStates[chatId] = UserState.AwaitingSupportMessage;
                    break;
                
                case "/adminka":
                    await _analyticsService.LogActionAsync("Команда /adminka", "Обработано", userId);
                    await HandleAdminCommand(client, message, cancellationToken);
                    break;

                default:
                    if (_userStates.ContainsKey(chatId) && _userStates[chatId] == UserState.Welcome)
                    {
                        return; 
                    }

                    await _analyticsService.LogActionAsync(message.Type.ToString(), "Не обработано", userId);

                    await client.SendMessage(chatId,
                        "Не понимаю команду. Введите /help для списка доступных команд.",
                        cancellationToken: cancellationToken);
                    break;
            }
        }

        /// <summary>
        /// Отправляет главное меню пользователю.
        /// </summary>
        /// <param name="client">Клиент бота.</param>
        /// <param name="chatId">Идентификатор чата пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Задача отправки меню.</returns>
        private async Task SendMainMenu(ITelegramBotClient client, long chatId, CancellationToken cancellationToken)
        {
            var mainMenuKeyboard = BotMenu.GetMainKeyboard();

            await client.SendMessage(
                chatId,
                "Выберите действие:",
                replyMarkup: mainMenuKeyboard,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Обрабатывает ввод имени пользователя.
        /// </summary>
        /// <param name="client">Клиент бота.</param>
        /// <param name="message">Сообщение с именем.</param>
        /// <param name="chatId">Идентификатор чата пользователя.</param>
        /// <returns>Задача обработки имени.</returns>
        public async Task HandleRealFirstName(ITelegramBotClient client, Message message, long chatId, Telegram.Bot.Types.User user)
        {
            using var scope = _serviceProvider.CreateScope();
            var _analyticsService = scope.ServiceProvider.GetRequiredService<IAnalyticsService>();

            if (message.Text == "Отмена")
            {
                await _analyticsService.LogActionAsync((message.Type).ToString(), "Отмена операции", user.Id);

                _userStates[chatId] = UserState.AwaitingRealFirstName;

                await client.SendMessage(chatId, "Регистрация отменена. Пожалуйста, введите ваше имя снова:");
                await client.SendMessage(chatId, "Введите ваше имя:");

                return;
            }

            await _analyticsService.LogActionAsync((message.Type).ToString(), "Обработано", user.Id);

            var firstName = message.Text;
            
            var validName = ValidationHelper.IsValidName(firstName);

            if (validName)
            {
                RealFirstName = firstName;

                _userStates[chatId] = UserState.AwaitingRealLastName;

                await client.SendMessage(chatId, "Введите вашу фамилию:");
            }
            else
            {
                await client.SendMessage(chatId, "Имя введено не верно, попробуй еще раз.");
            }

            
        }

        /// <summary>
        /// Обрабатывает ввод фамилии пользователя.
        /// </summary>
        /// <param name="client">Клиент бота.</param>
        /// <param name="message">Сообщение с фамилией.</param>
        /// <param name="chatId">Идентификатор чата пользователя.</param>
        /// <returns>Задача обработки фамилии.</returns>
        public async Task HandleRealLastName(ITelegramBotClient client, Message message, long chatId)
        {
            if (message.Text == "Отмена")
            {
                await client.SendMessage(chatId, "Регистрация - обязательный этап.");

                await client.SendMessage(chatId, "Введите ваше имя:");

                _userStates[chatId] = UserState.AwaitingRealFirstName;

                return;
            }

            var lastName = message.Text;

            var validName = ValidationHelper.IsValidName(lastName);

            if (validName)
            {
                RealLastName = lastName;

                _userStates[chatId] = UserState.AwaitingPhone;

                var phoneKeyboard = BotMenu.GetPhoneButton();

                await client.SendMessage(chatId, "Нажмите кнопку ниже, чтобы отправить номер телефона", replyMarkup: phoneKeyboard);
            }
            else
            {
                await client.SendMessage(chatId, "Фамилия введена не верно, попробуй еще раз.");
            }

            
        }

        /// <summary>
        /// Обрабатывает ввод номера телефона от пользователя.
        /// </summary>
        /// <param name="client">Клиент бота.</param>
        /// <param name="message">Сообщение с контактом.</param>
        /// <param name="chatId">Идентификатор чата пользователя.</param>
        /// <returns>Задача обработки телефона.</returns>
        private async Task HandlePhone(ITelegramBotClient client, Message message, long chatId)
        {
            if (message.Contact != null)
            {
                var phone = message.Contact.PhoneNumber;
                Phone = phone;
                _userStates[chatId] = UserState.AwaitingEmail; 

                await client.SendMessage(chatId, "Введите ваш email:");
            }
            else
            {
                await client.SendMessage(chatId, "Пожалуйста, отправьте номер телефона.");
            }
        }

        /// <summary>
        /// Обрабатывает ввод email от пользователя.
        /// </summary>
        /// <param name="client">Клиент бота.</param>
        /// <param name="message">Сообщение с email.</param>
        /// <param name="chatId">Идентификатор чата пользователя.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Задача обработки email.</returns>
        private async Task HandleEmail(ITelegramBotClient client, Message message, long chatId, long userId, IUserService userService, CancellationToken cancellationToken)
        {
            var email = message.Text;
            bool isValidFormat = System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (isValidFormat)
            {
                // Создаём пользователя через сервис
                var userRequest = new UserRequest
                {
                    TelegramId = userId,
                    FirstName = message.From.FirstName,
                    LastName = message.From.LastName,
                    UserName = message.From.Username,
                    Email = email,
                    Phone = Phone,
                    RealFirstName = RealFirstName,
                    RealLastName = RealLastName
                };

                var result = await userService.AddUser(userRequest);

                if (result.IsSuccess)
                {
                    _userStates[chatId] = UserState.Welcome;

                    var mainMenuKeyboard = BotMenu.GetMainKeyboard();

                    await client.SendMessage(
                        chatId,
                        "Регистрация завершена!",
                        replyMarkup: mainMenuKeyboard,
                        cancellationToken: cancellationToken);

                    await BotMenu.SendIntroMessagesAsync(client, chatId, cancellationToken);

                }
                else
                {
                    // Ошибка при регистрации
                    await client.SendMessage(chatId, "Произошла ошибка при регистрации. Попробуйте снова.");
                }
            }
            else
            {
                await client.SendMessage(chatId, "Пожалуйста, введите корректный Email 📧", cancellationToken: cancellationToken);
            }
            
        }

        /// <summary>
        /// Обрабатывает контактные сообщения от пользователя 
        /// </summary>
        /// <param name="client">Клиент бота.</param>
        /// <param name="message">Сообщение с контактом.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Задача обработки контактных данных.</returns>
        public async Task HandleContactMessageAsync(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;

            // Проверяем, что пользователь ожидает ввод телефона
            if (!_userStates.TryGetValue(chatId, out var state) || state != UserState.AwaitingPhone)
            {
                return;
            }


            if (message.Contact != null)
            {
                if (message.Contact.UserId != message.From.Id)
                {
                    await client.SendMessage(chatId, "Пожалуйста, отправьте именно ваш контакт, используя кнопку.", cancellationToken: cancellationToken);
                    return;
                }

                var phone = message.Contact.PhoneNumber;

                Phone = phone;

                _userStates[chatId] = UserState.AwaitingEmail; 

                await client.SendMessage(chatId, "Пожалуйста, введите ваш email:", cancellationToken: cancellationToken);
            }
            else
            {
                await client.SendMessage(chatId, "Пожалуйста, отправьте свой номер телефона, используя кнопку.", cancellationToken: cancellationToken);
            }
        }

        /// <summary>
        /// Обрабатывает команду администратора для получения временного ключа доступа к админ-панели.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task HandleAdminCommand(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;

            using var scope = _serviceProvider.CreateScope();
            var _userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            var key = await _userService.GenerateAdminKey(chatId);
            if (key == null)
            {
                await client.SendMessage(chatId, "У вас нет прав для доступа к админ-панели.", cancellationToken: cancellationToken);
                return;
            }

            await client.SendMessage(
                        chatId,
                        $"Для доступа к админ панели, перейдите по ссылке, " +
                        $"введите ваш TelegramId и ваш временный ключ\n\n" +
                        $"<code>{key}</code>\n\nОн действителен в течение 1 минуты.",
                        parseMode: ParseMode.Html
                        );
        }

        /// <summary>
        /// Обработчик ошибок для бота.
        /// </summary>
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"❌ Ошибка: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}
