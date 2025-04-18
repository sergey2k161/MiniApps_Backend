using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.DataBase.Repositories.DataAccess
{
    public class SupportRepository : ISupportRepository
    {
        private readonly MaDbContext _context;

        public SupportRepository(MaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> ChangeHelper(Guid id, long helper)
        {
            try
            {
                await _context.Supports
                    .Where(s => s.Id == id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(s => s.Helper, helper));

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        public async Task<ResultDto> ChangeProcess(Guid id, string process)
        {
            try
            {
                await _context.Supports
                    .Where(s => s.Id == id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(s => s.Process, process));

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        public async Task<ResultDto> ChangeStatus(Guid id, string newStatus)
        {
            try
            {
                await _context.Supports
                    .Where(s => s.Id == id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(s => s.Status, newStatus));

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        public async Task<ResultDto> CreateSupport(Support support)
        {
            try
            {
                await _context.AddAsync(support);
                await _context.SaveChangesAsync();

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }

        public async Task<Support> GetSupportById(Guid id)
        {
            return await _context.Supports.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Support>> GetSupports()
        {
            return await _context.Supports.ToListAsync();
        }

        public async Task<ResultDto> TakeAppeal(Guid id, long helper)
        {
            try
            {
                await _context.Supports
                    .Where(s => s.Id == id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(s => s.Helper, helper));

                return new ResultDto();
            }
            catch (Exception)
            {
                return new ResultDto(new List<string> { $"Ошибка в БД" });
            }
        }


    }
}
