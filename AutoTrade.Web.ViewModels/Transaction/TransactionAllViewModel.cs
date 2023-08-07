using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Web.ViewModels.Transaction
{
	public class TransactionAllViewModel
	{
        public string Id { get; set; } = null!;

        public string SellerId { get; set; } = null!;

        public string BuyerId { get; set; } = null!;

		public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
