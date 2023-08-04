namespace AutoTrade.Data.Models
{
	public class Wallet
	{
        public Wallet()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        public decimal Balance { get; set; }
    }
}
