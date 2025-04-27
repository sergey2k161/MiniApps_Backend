using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.Business.Services.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса поддержки
    /// </summary>
    public interface ISupportService
    {
        /// <summary>
        /// Создание обращения в поддержку
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<ResultDto> CreateSupport(long telegramId, string message);

        /// <summary>
        /// Взять обращение в работу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="helper"></param>
        /// <returns></returns>
        Task<ResultDto> TakeAppeal(Guid id, long helper);

        /// <summary>
        /// Изменение помощника обращения
        /// </summary>
        /// <param name="id"></param>
        /// <param name="helper"></param>
        /// <returns></returns>
        Task<ResultDto> ChangeHelper(Guid id, long helper);

        /// <summary>
        /// Изменение процесса обращения
        /// </summary>
        /// <param name="id"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        Task<ResultDto> ChangeProcess(Guid id, string process);

        /// <summary>
        /// Изменение статуса обращения
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        Task<ResultDto> ChangeStatus(Guid id, string newStatus);

        /// <summary>
        /// Получение обращений в поддержку
        /// </summary>
        /// <returns></returns>
        Task<List<Support>> GetSupports();

        /// <summary>
        /// Получение обращения в поддержку по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Support> GetSupportById(Guid id);
    }
}
