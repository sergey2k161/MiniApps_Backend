using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MiniApps_Backend.Bot.Handlers
{
    public static class BotMenu
    {
        public static ReplyKeyboardMarkup GetMainKeyboard()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "📲 MiniApp" },
                new KeyboardButton[] { "📊 FAQ", "ℹ️ Помощь" },
                new KeyboardButton[] { "🆘 Техническая поддержка"  }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = false,
                InputFieldPlaceholder = "Выберите действие..."
            };
        }


        public static ReplyKeyboardMarkup GetPhoneButton()
        {
            return new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton("📱 Отправить номер") { RequestContact = true },
                })
                {
                    ResizeKeyboard = true,
                    OneTimeKeyboard = true
                };
        }

        public static InlineKeyboardMarkup GetMiniAppButton()
        {
            return new InlineKeyboardMarkup(
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithWebApp(
                        text: "Открыть MiniApp 🚀",
                        webApp: new WebAppInfo { Url = "https://kmp3b968-3000.euw.devtunnels.ms/" }
                    )
                });
        }


        public static async Task SendIntroMessagesAsync(ITelegramBotClient client, long chatId, CancellationToken cancellationToken)
        {

            var messages = new[]
            {
                "Welcome! 👋 Я твой помощник в обучении от команды БАРС-Груп и команды '3 Кота и 1 Кошечка'.",
                "У каждой компании есть дух. У нас — тотемное животное: 🐆 барс. Он умный, быстрый и наблюдательный — как ты 😉",
                "Здесь ты найдешь курсы, тесты, помощь от экспертов и прокачку навыков! 🚀",
                "Не теряй ни минуты — заходи в MiniApp и начни свой путь!",
            };


            foreach (var msg in messages)
            {
                await client.SendMessage(chatId, msg, cancellationToken: cancellationToken);
                await Task.Delay(100, cancellationToken); 
            }
        }


    }
}
