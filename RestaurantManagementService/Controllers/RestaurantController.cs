using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Omf.RestaurantManagementService.Service;

namespace Omf.RestaurantManagementService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {

        private readonly ILogger<RestaurantController> _logger;
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(ILogger<RestaurantController> logger, IRestaurantService restaurantService)
        {
            _logger = logger;
            _restaurantService = restaurantService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync(string id, string name, string location,
                string budget, string rating, string food)
        {
            try
            {
                var result = await _restaurantService.SearchRestaurant(id, name, location, budget, rating, food);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while fetching restaurant", ex.Message);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }
    }
}