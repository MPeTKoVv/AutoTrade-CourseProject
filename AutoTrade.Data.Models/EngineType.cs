using System.ComponentModel.DataAnnotations;

namespace AutoTrade.Data.Models
{
    using static Common.EntityValidationConstants.Engine;

	public class EngineType
	{
        public EngineType()
        {
            this.Cars = new HashSet<Car>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TypeMaxLength)]
        public string Type { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }
    }
}
