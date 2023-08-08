namespace AutoTrade.Data.Models
{
	using System.ComponentModel.DataAnnotations;

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
