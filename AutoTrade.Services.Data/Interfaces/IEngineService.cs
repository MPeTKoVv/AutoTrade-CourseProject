using AutoTrade.Web.ViewModels.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Services.Data.Interfaces
{
	public interface IEngineService
	{
		Task<IEnumerable<CarSelectEngineTypeViewModel>> AllEngineTypesAsync();
	}
}
