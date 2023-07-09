using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Data.Models
{
	using static Common.EntityValidationConstants.Review;

	public class Review
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Range(RatingMinValue, RatingMaxValue)]
        public int Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CarId { get; set; }
		public virtual Car Car { get; set; } = null!;

        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;
	}
}
