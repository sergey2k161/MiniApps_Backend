using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Models.Entity.Ammount;
using MiniApps_Backend.DataBase.Models.Entity.Analysis;

namespace MiniApps_Backend.Business.Services.Interfaces
{
    public interface IAnalyticsService
    {
        Task<byte[]> GenerateAnalyticsReport(bool accurate);

        Task LogActionAsync(string actionName, string result, long userId);
    }
}
