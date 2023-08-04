using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace AutoTrade.Data.Models
{
    using static Common.EntityValidationConstants.Dealer;

    public class Dealer
    {
        public Dealer()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
    }
}
