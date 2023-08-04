using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTrade.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.Garage = new HashSet<Car>();
            this.FavoriteCars = new HashSet<Car>();
            this.Reviews = new HashSet<Review>();
			this.Transactions = new HashSet<Transaction>();
		}

		public virtual ICollection<Car> Garage { get; set; }

        [NotMapped]
        public virtual ICollection<Car> FavoriteCars { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
		public virtual ICollection<Transaction> Transactions { get; set; }

	}
}
