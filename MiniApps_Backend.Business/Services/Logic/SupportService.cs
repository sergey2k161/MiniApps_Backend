using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.Business.Services.Logic
{
    internal class SupportService : ISupportService
    {
        private readonly ISupportRepository _supportRepository;

        public SupportService(ISupportRepository supportRepository)
        {
            _supportRepository = supportRepository;
        }

        /// <summary>
        /// Изменение помощника
        /// </summary>
        /// <param name="id"></param>
        /// <param name="helper"></param>
        /// <returns></returns>
        public async Task<ResultDto> ChangeHelper(Guid id, long helper)
        {
            await _supportRepository.ChangeHelper(id, helper);

            return new ResultDto();
        }

        /// <summary>
        /// Изменение процесса
        /// </summary>
        /// <param name="id"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        public async Task<ResultDto> ChangeProcess(Guid id, string process)
        {
            await _supportRepository.ChangeProcess(id, process);

            return new ResultDto();
        }

        /// <summary>
        /// Изменение статуса
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public async Task<ResultDto> ChangeStatus(Guid id, string newStatus)
        {
            await _supportRepository.ChangeStatus(id, newStatus);

            return new ResultDto();
        }

        /// <summary>
        /// Создание обращения
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ResultDto> CreateSupport(long telegramId, string message)
        {
            var support = new Support
            {
                UserTelegramId = telegramId,
                InWork = false,
                Status = "Новое",
                Helper = null,
                Process = "Новое",
                Message = message
            };

            await _supportRepository.CreateSupport(support);

            return new ResultDto();
        }

        /// <summary>
        /// Получение всех обращений
        /// </summary>
        /// <returns></returns>
        public async Task<List<Support>> GetSupports()
        {
            return await _supportRepository.GetSupports();
        }

        /// <summary>
        /// Взять обращение в работу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="helper"></param>
        /// <returns></returns>
        public async Task<ResultDto> TakeAppeal(Guid id, long helper)
        {
            await _supportRepository.TakeAppeal(id, helper);

            return new ResultDto();
        }

        /// <summary>
        /// Получение обращения по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Support> GetSupportById(Guid id)
        {
            return await _supportRepository.GetSupportById(id);
        }
    }
}
