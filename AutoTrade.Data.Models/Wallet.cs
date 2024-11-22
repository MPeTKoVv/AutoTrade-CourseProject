namespace AutoTrade.Data.Models
{
	using System.ComponentModel.DataAnnotations;

	public class Wallet
	{
		public Wallet()
		{
			this.Id = Guid.NewGuid();
			Balance = 10000m;
			this.Transactions = new HashSet<Transaction>();
		}

		[Key]
		public Guid Id { get; set; }

		public Guid UserId { get; set; }
		public virtual ApplicationUser User { get; set; } = null!;

        public Guid? CreditCardId { get; set; }
        public CreditCard? CreditCard { get; set; }

        public decimal Balance { get; set; }

		public virtual ICollection<Transaction> Transactions { get; set; }
	}
}
