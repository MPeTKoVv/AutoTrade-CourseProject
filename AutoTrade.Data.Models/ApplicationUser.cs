using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTrade.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
			this.Transactions = new HashSet<Transaction>();
		}

        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; } = null!;

		public virtual ICollection<Transaction> Transactions { get; set; }

	}
}
