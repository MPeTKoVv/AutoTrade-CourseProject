using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Services.Data.Models.Statistics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.WebApi.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsWebApiController : ControllerBase
    {
        private readonly ICarService carService;

        public StatisticsWebApiController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                StatisticsServiceModel serviceModel = await
                    this.carService.GetStatisticsAsync();

                return Ok(serviceModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
