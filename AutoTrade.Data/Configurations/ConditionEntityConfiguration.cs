using AutoTrade.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoTrade.Data.Configurations
{
    public class ConditionEntityConfiguration : IEntityTypeConfiguration<Condition>
    {
        public void Configure(EntityTypeBuilder<Condition> builder)
        {
            builder.HasData(this.GenerateConditions());
        }

        private Condition[] GenerateConditions()
        {
            ICollection<Condition> conditions = new HashSet<Condition>();

            Condition condition;

            condition = new Condition
            {
                Id = 1,
                Name = "New"
            };
            conditions.Add(condition);

            condition = new Condition
            {
                Id = 2,
                Name = "Used"
            };
            conditions.Add(condition);

            condition = new Condition
            {
                Id = 3,
                Name = "Renewed"
            };
            conditions.Add(condition);

            return conditions.ToArray();
        }
    }
}
