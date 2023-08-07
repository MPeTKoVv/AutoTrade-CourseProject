using AutoTrade.Web.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Services.Data.Interfaces
{
	public interface ITransactionService
	{
		Task RecordTransaction(string carId, string buyerId, string sellerId);
	}
}
