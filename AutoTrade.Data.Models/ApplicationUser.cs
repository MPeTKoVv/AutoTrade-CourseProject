using Microsoft.AspNetCore.Identity;

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
        }

        public virtual ICollection<Car> Garage { get; set; }
        public virtual ICollection<Car> FavoriteCars { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
	}
}
