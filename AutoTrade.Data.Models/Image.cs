using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Data.Models
{
    using static Common.EntityValidationConstants.Image;

    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        public string Url { get; set; } = null!;

        public string CarId { get; set; } = null!;
        public virtual Car Car { get; set; } = null!;
    }
}
