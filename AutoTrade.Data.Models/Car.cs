using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTrade.Data.Models
{
	using static Common.EntityValidationConstants.Car;

	public class Car
	{
		public Car()
		{
			this.Id = Guid.NewGuid();
			this.Reviews = new HashSet<Review>();
            this.Images  = new HashSet<string>();
        }

        [Key]
		public Guid Id { get; set; }

		[Required]
		[MaxLength(MakeMaxLength)]
		public string Make { get; set; } = null!;

		[Required]
		[MaxLength(ModelMaxLength)]
		public string Model { get; set; } = null!;

		[MaxLength(CountryMaxLength)]
		public string Country { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Range(HorsepowerMinValue, HorsepowerMaxValue)]
		public int Horsepower { get; set; }

		public decimal Price { get; set; }

		public int Year { get; set; }

		public int Mileage { get; set; }

		public DateTime AddedOn { get; set; }

        public int ConditionId { get; set; }
		public virtual Condition Condition { get; set; } = null!;

		public int EngineId { get; set; }
		public virtual EngineType EngineType { get; set; } = null!;

		public int CategoryId { get; set; }
		public virtual Category Category { get; set; } = null!;

		public Guid SellerId { get; set; }
		public virtual Seller Seller { get; set; } = null!;

		public Guid? CustomerId { get; set; }
		public virtual ApplicationUser? Customer { get; set; }

        [NotMapped]
        public virtual ICollection<string> Images { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
