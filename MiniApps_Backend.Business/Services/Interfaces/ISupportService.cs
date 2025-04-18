using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApps_Backend.Business.Services.Interfaces
{
    public interface ISupportService
    {
        Task<ResultDto> CreateSupport(long telegramId, string message);

        Task<ResultDto> TakeAppeal(Guid id, long helper);

        Task<ResultDto> ChangeHelper(Guid id, long helper);

        Task<ResultDto> ChangeProcess(Guid id, string process);

        Task<ResultDto> ChangeStatus(Guid id, string newStatus);

        Task<List<Support>> GetSupports();

        Task<Support> GetSupportById(Guid id);
    }
}
