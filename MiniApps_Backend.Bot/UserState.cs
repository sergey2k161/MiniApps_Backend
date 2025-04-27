namespace MiniApps_Backend.Bot
{
    /// <summary>
    /// Перечисление для состояний пользователя в боте.
    /// </summary>
    public enum UserState
    {
        // Состояния для регистрации пользователя
        AwaitingRealFirstName = 1,
        AwaitingRealLastName = 2,
        AwaitingPhone = 3,
        AwaitingEmail = 4,
        AwaitingWelcomeMessage = 5,

        // Состояния для работы с ботом
        MainMenu = 6,
        Welcome = 7,
        AwaitingSupportMessage = 8
    }

}
