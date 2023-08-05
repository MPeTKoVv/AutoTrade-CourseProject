using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTrade.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
			Transactions = new HashSet<Transaction>();
            OwnedCars = new HashSet<Car>();
        }

        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; } = null!;

        public virtual ICollection<Car> OwnedCars { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

	}
}
