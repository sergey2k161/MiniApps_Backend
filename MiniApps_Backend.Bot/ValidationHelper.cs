using System.Text.RegularExpressions;

namespace MiniApps_Backend.Bot
{
    /// <summary>
    /// Класс для валидации
    /// </summary>
    public static class ValidationHelper
    {
        private const int MinLenght = 2;
        private const int MaxLenght = 32;

        private static readonly Regex ValidNameRegex = new Regex(@"^[\p{L}\s'-]+$", RegexOptions.Compiled);

        /// <summary>
        /// Валидация имени
        /// </summary>
        /// <param name="name">Имя/Фамилия</param>
        /// <returns></returns>
        public static bool IsValidName(string name)
        {
            if (name == null || name.Trim().Length == 0 || name.Length < MinLenght || name.Length > MaxLenght || !ValidNameRegex.IsMatch(name) || name.Contains("  ") || !char.IsLetter(name[0]) || !char.IsLetter(name[^1]))
            {
                return false;
            }

            return true;
        } 
    }
}
