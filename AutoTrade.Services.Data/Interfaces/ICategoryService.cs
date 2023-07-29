using AutoTrade.Web.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Services.Data.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CarSelectCategoryViewModel>> AllCategoriesAsync();

		Task<bool>ExistsByIdAsync(int id);
	}
}
