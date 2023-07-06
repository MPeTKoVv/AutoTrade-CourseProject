using System.ComponentModel.DataAnnotations;

namespace AutoTrade.Data.Models
{
    using static Common.EntityValidationConstants.Condition;

    public class Condition
    {
        public Condition()
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