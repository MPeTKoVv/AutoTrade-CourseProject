using AutoTrade.Data.Models;
using AutoTrade.Web.ViewModels.Category;
using AutoTrade.Web.ViewModels.Condition;
using AutoTrade.Web.ViewModels.Engine;
using System.ComponentModel.DataAnnotations;


namespace AutoTrade.Web.ViewModels.Car
{
	using static Common.EntityValidationConstants.Car;

	public class CarViewModel
	{
		public CarViewModel()
		{
			this.Categories = new HashSet<CarSelectCategoryViewModel>();
			this.Conditions = new HashSet<CarSelectConditionViewModel>();
			this.EngineTypes = new HashSet<CarSelectEngineTypeViewModel>();

			this.Images = new HashSet<Image>();
		}

		[Required]
		[StringLength(MakeMaxLength, MinimumLength = MakeMinLength)]
		public string Make { get; set; } = null!;

		[Required]
		[StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
		public string Model { get; set; } = null!;

		[Required]
		[StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
		public string Country { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		public int Horsepower { get; set; }

		[Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
		public decimal Price { get; set; }

		public int Year { get; set; }

		public int Mileage { get; set; }

        public ICollection<Image> Images { get; set; }

		[Display(Name = "Category")]
        public int CategoryId { get; set; }

		[Display(Name = "Condition")]
		public int ConditionId { get; set; }

		[Display(Name = "Engine Type")]
		public int EngineTypeId { get; set; }

		public IEnumerable<CarSelectCategoryViewModel> Categories { get; set; }
        public IEnumerable<CarSelectConditionViewModel> Conditions { get; set; }
		public IEnumerable<CarSelectEngineTypeViewModel> EngineTypes { get; set; }
	}
}
