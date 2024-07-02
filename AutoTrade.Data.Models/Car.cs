namespace AutoTrade.Data.Models
{
	using System.ComponentModel.DataAnnotations;

	using static Common.EntityValidationConstants.Car;

	public class Car
	{
		public Car()
		{
			this.Id = Guid.NewGuid();
			this.Transactions = new HashSet<Transaction>();
		}

		[Key]
		public Guid Id { get; set; }

		[Required]
		[MaxLength(MakeMaxLength)]
		public string Make { get; set; } = null!;

		[Required]
		[MaxLength(ModelMaxLength)]
		public string Model { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		[MaxLength(ImageUrlMaxLength)]
		public string ImageUrl { get; set; } = null!;

		public int Horsepower { get; set; }

		public decimal Price { get; set; }

		public int Year { get; set; }

		public int Mileage { get; set; }

		public DateTime AddedOn { get; set; }

		public DateTime AddedOnForSale { get; set; }

		public bool IsActive { get; set; }

		public bool IsForSale { get; set; }

		public int EngineId { get; set; }
		public virtual EngineType EngineType { get; set; } = null!;

		public int CategoryId { get; set; }
		public virtual Category Category { get; set; } = null!;

		public int TransmissionId { get; set; }
		public virtual Transmission Transmission { get; set; }

		public Guid? SellerId { get; set; }
		public virtual Seller? Seller { get; set; }

		public Guid OwnerId { get; set; }
		public virtual ApplicationUser Owner { get; set; }

		public virtual ICollection<Transaction> Transactions { get; set; }
	}
}
