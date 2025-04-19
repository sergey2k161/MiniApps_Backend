using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.DataBase.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория поддержки
    /// </summary>
    public interface ISupportRepository
    {
        /// <summary>
        /// Создание обращения
        /// </summary>
        /// <param name="support"></param>
        /// <returns></returns>
        Task<ResultDto> CreateSupport(Support support);

        /// <summary>
        /// Взять обращение в работу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="helper"></param>
        /// <returns></returns>
        Task<ResultDto> TakeAppeal(Guid id, long helper);

        /// <summary>
        /// Изменение помощника
        /// </summary>
        /// <param name="id"></param>
        /// <param name="helper"></param>
        /// <returns></returns>
        Task<ResultDto> ChangeHelper(Guid id, long helper);

        /// <summary>
        /// Изменение процесса
        /// </summary>
        /// <param name="id"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        Task<ResultDto> ChangeProcess(Guid id, string process);

        /// <summary>
        /// Изменение статуса
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        Task<ResultDto> ChangeStatus(Guid id, string newStatus);

        /// <summary>
        /// Получение обращений
        /// </summary>
        /// <returns></returns>
        Task<List<Support>> GetSupports();

        /// <summary>
        /// Получение обращения по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Support> GetSupportById(Guid id);
    }
}
