namespace AutoTrade.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Caching.Memory;
	using Services.Data.Interfaces;
	using Web.ViewModels.Transaction;

	using static Common.GeneralApplicationConstants;

	public class TransactionController : BaseAdminController
	{
		private readonly ITransactionService transactionService;
		private readonly IMemoryCache memoryCache;

		public TransactionController(ITransactionService transactionService, IMemoryCache memoryCache)
		{
			this.transactionService = transactionService;
			this.memoryCache = memoryCache;
		}

		[Route("Transaction/All")]
		[ResponseCache(Duration = 60)]
		public async Task<IActionResult> All()
		{
			IEnumerable<TransactionViewModel> allTransactions = this.memoryCache.Get<IEnumerable<TransactionViewModel>>(TransactionsCacheKey);
			if (allTransactions == null)
			{
				allTransactions = await this.transactionService.AllAsync();

				MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(TimeSpan.FromMinutes(TransactionsChacheDurationMinutes));

				this.memoryCache.Set(UsersCacheKey, allTransactions, cacheEntryOptions);
			}
			return View(allTransactions);
		}
	}
}
