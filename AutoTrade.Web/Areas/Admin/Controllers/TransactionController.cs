namespace AutoTrade.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using Services.Data.Interfaces;
	using Web.ViewModels.Transaction;

	public class TransactionController : BaseAdminController
	{
		private readonly ITransactionService transactionService;

		public TransactionController(ITransactionService transactionService)
		{
			this.transactionService = transactionService;
		}

		[Route("Transaction/All")]
		public async Task<IActionResult> All()
		{
			IEnumerable<TransactionViewModel> allTransactions = await this.transactionService.AllAsync();

			return View(allTransactions);
		}
	}
}
