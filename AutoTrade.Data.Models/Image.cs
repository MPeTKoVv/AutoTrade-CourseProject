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
        public ByteArrayContent Bytes { get; set; } = null!;

        public Guid CarId { get; set; }
        public virtual Car Car { get; set; } = null!;
    }
}
