﻿using System.ComponentModel.DataAnnotations;

namespace AutoTrade.Data.Models
{
    using static Common.EntityValidationConstants.Category;

    public class Category
    {
        public Category()
        {
            this.Cars = new HashSet<Car>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }
    }
}