using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;

namespace MiniApps_Backend.DataBase.Repositories.Interfaces
{
    public interface ISupportRepository
    {
        Task<ResultDto> CreateSupport(Support support);

        Task<ResultDto> TakeAppeal(Guid id, long helper);

        Task<ResultDto> ChangeHelper(Guid id, long helper);

        Task<ResultDto> ChangeProcess(Guid id, string process);

        Task<ResultDto> ChangeStatus(Guid id, string newStatus);

        Task<List<Support>> GetSupports();

        Task<Support> GetSupportById(Guid id);
    }
}
