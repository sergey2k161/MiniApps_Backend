using Microsoft.EntityFrameworkCore;
using MiniApps_Backend.DataBase.Models.Dto;
using MiniApps_Backend.DataBase.Models.Entity;
using MiniApps_Backend.DataBase.Repositories.Interfaces;

namespace MiniApps_Backend.DataBase.Repositories.DataAccess
{
    /// <summary>
    /// Репозиторий для работы с сущностью Support
    /// </summary>
    public class SupportRepository : ISupportRepository
    {
        private readonly MaDbContext _context;

        /// <summary>
        /// Конструктор репозитория SupportRepository
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public SupportRepository(MaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Изменяет помощника для указанного обращения
        /// </summary>
        /// <param name="id">Идентификатор обращения</param>
        /// <param name="helper">Идентификатор помощника</param>
        /// <returns>Результат выполнения операции</returns>
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

        /// <summary>
        /// Изменяет процесс для указанного обращения
        /// </summary>
        /// <param name="id">Идентификатор обращения</param>
        /// <param name="process">Новое значение процесса</param>
        /// <returns>Результат выполнения операции</returns>
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

        /// <summary>
        /// Изменяет статус для указанного обращения
        /// </summary>
        /// <param name="id">Идентификатор обращения</param>
        /// <param name="newStatus">Новый статус</param>
        /// <returns>Результат выполнения операции</returns>
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

        /// <summary>
        /// Создает новое обращение
        /// </summary>
        /// <param name="support">Объект обращения</param>
        /// <returns>Результат выполнения операции</returns>
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

        /// <summary>
        /// Получает обращение по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор обращения</param>
        /// <returns>Объект обращения</returns>
        public async Task<Support> GetSupportById(Guid id)
        {
            return await _context.Supports.FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// Получает список всех обращений
        /// </summary>
        /// <returns>Список обращений</returns>
        public async Task<List<Support>> GetSupports()
        {
            return await _context.Supports.ToListAsync();
        }

        /// <summary>
        /// Привязывает помощника к указанному обращению
        /// </summary>
        /// <param name="id">Идентификатор обращения</param>
        /// <param name="helper">Идентификатор помощника</param>
        /// <returns>Результат выполнения операции</returns>
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
