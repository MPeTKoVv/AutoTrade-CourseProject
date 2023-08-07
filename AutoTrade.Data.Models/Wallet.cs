using System.ComponentModel.DataAnnotations;

namespace AutoTrade.Data.Models
{
	public class Wallet
	{
        public Wallet()
        {
            this.Id = Guid.NewGuid();
            this.Transactions = new HashSet<Transaction>();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        public decimal Balance { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
