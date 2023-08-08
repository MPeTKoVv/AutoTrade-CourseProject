namespace AutoTrade.Web.ViewModels.Car
{
	using System.ComponentModel.DataAnnotations;
	using AutoMapper;
	using AutoTrade.Data.Models;
	using AutoTrade.Services.Mapping;
	using AutoTrade.Web.ViewModels.Category;
	using AutoTrade.Web.ViewModels.Engine;
	using AutoTrade.Web.ViewModels.Transmission;

	using static Common.EntityValidationConstants.Car;

	public class CarFormModel : IMapTo<Car>, IHaveCustomMappings, IMapFrom<Car>
	{
		public CarFormModel()
		{
			this.Categories = new HashSet<CarSelectCategoryViewModel>();
			this.EngineTypes = new HashSet<CarSelectEngineTypeViewModel>();
			this.Transmissions = new HashSet<CarSelectTransmissionViewModel>();
		}

		[Required]
		[StringLength(MakeMaxLength, MinimumLength = MakeMinLength)]
		public string Make { get; set; } = null!;

		[Required]
		[StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
		public string Model { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Range(typeof(int), HorsepowerMinValue, HorsepowerMaxValue)]
		public int Horsepower { get; set; }

		[Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
		public decimal Price { get; set; }

		[Range(typeof(int), YearMinValue, YearMaxValue)]
		public int Year { get; set; }

		[Range(typeof(int), MileageMinValue, MileageMaxValue)]
		[Display(Name = "Mileage(km)")]
		public int Mileage { get; set; }

		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		[Display(Name = "Engine Type")]
		public int EngineTypeId { get; set; }

		[Display(Name = "Transmission")]
		public int TransmissionId { get; set; }

		[Required]
		[StringLength(ImageUrlMaxLength, MinimumLength = ImageUrlMinLength)]
		public string ImageUrl { get; set; } = null!;

		public IEnumerable<CarSelectCategoryViewModel> Categories { get; set; }
		public IEnumerable<CarSelectEngineTypeViewModel> EngineTypes { get; set; }
		public IEnumerable<CarSelectTransmissionViewModel> Transmissions { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<CarFormModel, Car>()
				.ForMember(d => d.AddedOn, opt => opt.Ignore())
				.ForMember(d => d.SellerId, opt => opt.Ignore());
		}
	}
}
