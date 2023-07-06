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
        }

        public virtual ICollection<Car> Garage { get; set; }
        public virtual ICollection<Car> FavoriteCars { get; set; }
    }
}
