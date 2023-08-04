using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTrade.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.FavoriteCars = new HashSet<Car>();
            this.Reviews = new HashSet<Review>();
        }

        [NotMapped]
        public virtual ICollection<Car> FavoriteCars { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
	}
}
