namespace AutoTrade.Data.Models
{
	using System.ComponentModel.DataAnnotations;

	using Microsoft.AspNetCore.Identity;

	using static Common.EntityValidationConstants.User;

	public class ApplicationUser : IdentityUser<Guid>
	{
		public ApplicationUser()
		{
			this.Id = Guid.NewGuid();
			this.BoughtCarsHistory = new HashSet<Transaction>();
			this.OwnedCars = new HashSet<Car>();
		}

		[Required]
		[MaxLength(FirstNameMaxLength)]
		public string FirstName { get; set; } = null!;


		[Required]
		[MaxLength(LastNameMaxLength)]
		public string LastName { get; set; } = null!;

		public Guid WalletId { get; set; }
		public Wallet Wallet { get; set; } = null!;

		public virtual ICollection<Car> OwnedCars { get; set; }

		public virtual ICollection<Transaction> BoughtCarsHistory { get; set; }
	}
}
