using AutoTrade.Data.Models;
using AutoTrade.Web.ViewModels.Category;
using AutoTrade.Web.ViewModels.Condition;
using AutoTrade.Web.ViewModels.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Web.ViewModels.Car
{
	using static Common.EntityValidationConstants.Car;

	public class CarViewModel
	{
		public CarViewModel()
		{
			this.EngineTypes = new HashSet<CarSelectEngineTypeViewModel>();
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

        public int CategoryId { get; set; }
        public int ConditionId { get; set; }
        public int EngineTypeId { get; set; }

		public IEnumerable<CarSelectCategoryViewModel> Categories { get; set; }
        public IEnumerable<CarSelectConditionViewModel> Conditions { get; set; }
		public IEnumerable<CarSelectEngineTypeViewModel> EngineTypes { get; set; }
	}
}
