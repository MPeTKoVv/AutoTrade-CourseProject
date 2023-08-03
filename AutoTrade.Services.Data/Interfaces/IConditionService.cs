using AutoTrade.Web.ViewModels.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Services.Data.Interfaces
{
	public interface IConditionService
	{
		Task<IEnumerable<CarSelectConditionViewModel>> AllConditionsAsync();

		Task<bool> ExistsByIdAsync(int id);

		Task<IEnumerable<string>> AllConditionNamesAsync();
	}
}
