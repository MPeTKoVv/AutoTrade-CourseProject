namespace AutoTrade.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using Models;

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
