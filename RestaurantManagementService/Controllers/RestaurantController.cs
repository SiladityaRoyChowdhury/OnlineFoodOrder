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
        public async Task<IActionResult> GetAsync(string id, string name, string location,
                string budget, string rating, string food)
        {
            var result = await _restaurantService.SearchRestaurant(id, name, location, budget, rating, food);
            return Ok(result);
        }
    }
}