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

        public async Task<ResultDto> ChangeHelper(Guid id, long helper)
        {
            await _supportRepository.ChangeHelper(id, helper);

            return new ResultDto();
        }

        public async Task<ResultDto> ChangeProcess(Guid id, string process)
        {
            await _supportRepository.ChangeProcess(id, process);

            return new ResultDto();
        }

        public async Task<ResultDto> ChangeStatus(Guid id, string newStatus)
        {
            await _supportRepository.ChangeStatus(id, newStatus);

            return new ResultDto();
        }

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

        public async Task<List<Support>> GetSupports()
        {
            return await _supportRepository.GetSupports();
        }

        public async Task<ResultDto> TakeAppeal(Guid id, long helper)
        {
            await _supportRepository.TakeAppeal(id, helper);

            return new ResultDto();
        }

        public async Task<Support> GetSupportById(Guid id)
        {
            return await _supportRepository.GetSupportById(id);
        }

    }
}
