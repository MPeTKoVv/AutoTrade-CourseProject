using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AutoTrade.Data.Models;

namespace AutoTrade.Data.Configurations
{
    public class TransmissionEntityConfiguration : IEntityTypeConfiguration<Transmission>
    {
        public void Configure(EntityTypeBuilder<Transmission> builder)
        {
            builder
                 .HasData(GenerateTransmissions());
        }

        private Transmission[] GenerateTransmissions()
        {
            ICollection<Transmission> transmissions = new HashSet<Transmission>();

            Transmission transmission;

            transmission = new Transmission
            {
                Id = 1,
                Name = "Manual"
            };
            transmissions.Add(transmission);

            transmission = new Transmission
            {
                Id = 2,
                Name = "Automatic"
            };
            transmissions.Add(transmission);


            return transmissions.ToArray();
        }
    }
}
