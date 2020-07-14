using MassTransit;
using Omf.Common.Events;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
using Omf.RestaurantManagementService.Service;

namespace Omf.RestaurantManagementService.EventConsumers
{
    public class UpdateRestaurantConsumer : IConsumer<UpdateRestaurantEvent>
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ILogger<UpdateRestaurantConsumer> _logger;
        private readonly IBus _bus;

        public UpdateRestaurantConsumer(IRestaurantService restaurantService, ILogger<UpdateRestaurantConsumer> logger, IBus bus)
        {
            _restaurantService = restaurantService;
            _logger = logger;
            _bus = bus;
        }
        public async Task Consume(ConsumeContext<UpdateRestaurantEvent> context)
        {
            try
            {
                _logger.LogInformation("Restaurant Update Message received");

                var restaurant = _restaurantService.SearchRestaurant(context.Message.RestaurantId, null, null, null, null, null).Result.FirstOrDefault();

                if (restaurant != null)
                {
                    restaurant.Restaurant.Rating = context.Message.Rating;
                    await _restaurantService.UpdateRestaurant(restaurant.Restaurant);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured during updating Restaurant", ex.Message);
                throw ex;
            }
        }
    }
}
