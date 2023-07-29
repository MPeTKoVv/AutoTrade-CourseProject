using AutoTrade.Data.Models;
using AutoTrade.Web.ViewModels.Category;
using AutoTrade.Web.ViewModels.Condition;
using AutoTrade.Web.ViewModels.Engine;
using Microsoft.AspNetCore.Http;
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

			//this.Images = new HashSet<Image>();
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

		[Range(typeof(int), HorsepowerMinValue, HorsepowerMaxValue)]
		public int Horsepower { get; set; }

		[Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
		public decimal Price { get; set; }

		[Range(typeof(int), YearMinValue, YearMaxValue)]
		public int Year { get; set; }

		[Range(typeof(int), MileageMinValue, MileageMaxValue)]
		public int Mileage { get; set; }

		[Display(Name = "Category")]
        public int CategoryId { get; set; }

		[Display(Name = "Condition")]
		public int ConditionId { get; set; }

		[Display(Name = "Engine Type")]
		public int EngineTypeId { get; set; }

		[StringLength(UrlMaxLength, MinimumLength = UrlMinLength)]
		public string ImageUrl { get; set; }

		public IEnumerable<CarSelectCategoryViewModel> Categories { get; set; }
        public IEnumerable<CarSelectConditionViewModel> Conditions { get; set; }
		public IEnumerable<CarSelectEngineTypeViewModel> EngineTypes { get; set; }
	}
}
