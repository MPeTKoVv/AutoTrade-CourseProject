using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Data.Models
{
	public class Transaction
	{
        public Transaction()
        {
            this.Id = Guid.NewGuid();
        }

		[Key]
        public Guid Id { get; set; }

		public DateTime TransactionDate { get; set; }
		public decimal Amount { get; set; }

		public Guid CarId { get; set; }
		public virtual Car Car { get; set; } = null!;

		public Guid BuyerId { get; set; }
        public virtual ApplicationUser Buyer { get; set; } = null!;

		public Guid SellerId { get; set; }
        public virtual Seller Seller { get; set; } = null!;
	}
}
