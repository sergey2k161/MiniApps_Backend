using Microsoft.AspNetCore.Mvc;
using MiniApps_Backend.Business.Services.Interfaces;
using MiniApps_Backend.DataBase.Models.Dto;

namespace MiniApps_Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public TransactionController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        /// <summary>
        /// Создание новой транзакции в системе
        /// </summary>
        /// <param name="transaction">Дто Транзакции</param>
        /// <returns>Возвращает статус выполнения операции</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDto transaction)
        {
            var result = await _walletService.CreateTransaction
                (transaction.TelegramId, transaction.Type, transaction.WithDiscount, transaction.PercentageDiscounts, transaction.Total);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        /// <summary>
        /// Получение текущего баланса пользователя
        /// </summary>
        /// <param name="telegramId">Идентификатор пользователя</param>
        /// <returns>Возвращает текущий баланс пользователя</returns>
        [HttpGet]
        public async Task<IActionResult> GetBalance(long telegramId)
        {
            var balance = await _walletService.GetBalance
                (telegramId);

            return Ok(balance);
        }
    }
}
