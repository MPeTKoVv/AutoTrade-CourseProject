﻿using System.ComponentModel.DataAnnotations;

namespace AutoTrade.Data.Models
{
    using static Common.EntityValidationConstants.Car;

    public class Car
    {
        public Car()
        {
            this.Id = Guid.NewGuid();
            //this.Images = new HashSet<Image>();
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

        public int Horsepower { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal Price { get; set; }

        public int Mileage { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        public int ConditionId { get; set; }
        public virtual Condition Condition { get; set; } = null!;

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public Guid SellerId { get; set; }
        public virtual Seller Seller { get; set; } = null!;

        public Guid? CustomerId { get; set; }
        public virtual ApplicationUser? Customer { get; set; }
    }
}
