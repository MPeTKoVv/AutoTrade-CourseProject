using AutoTrade.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Data.Configurations
{
	public class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
	{
		public void Configure(EntityTypeBuilder<Image> builder)
		{
			//builder
			//	.HasOne(i => i.Car)
			//	.WithMany(c => c.Images)
			//	.HasForeignKey(i => i.CarId);

			//builder.HasData(GenerateImages());
		}

		private Image[] GenerateImages()
		{
			ICollection<Image> images = new HashSet<Image>();

			Image image;

			image = new Image
			{
				Id = 3,
				Url = "https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg",
			};
			images.Add(image);

			return images.ToArray();
		}
	}
}
