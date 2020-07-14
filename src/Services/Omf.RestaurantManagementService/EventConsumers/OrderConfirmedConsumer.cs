using MassTransit;
using Omf.Common.Events;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Omf.RestaurantManagementService.DomainModel;
using Omf.RestaurantManagementService.Data.Repository;
using System.Linq;

namespace Omf.RestaurantManagementService.EventConsumers
{
    public class OrderConfirmedConsumer : IConsumer<OrderConfirmedEvent>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<OrderConfirmedConsumer> _logger;
        private readonly IBus _bus;

        public OrderConfirmedConsumer(IRestaurantRepository restaurantRepository, ILogger<OrderConfirmedConsumer> logger, IBus bus)
        {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
            _bus = bus;
        }
        public async Task Consume(ConsumeContext<OrderConfirmedEvent> context)
        {
            try
            {
                _logger.LogInformation("Delivery Message received");

                var menus = await _restaurantRepository.GetAllMenuAsync();
                var restaurant = _restaurantRepository.GetAllRestaurantAsync().Result.ToList()
                                    .Find(x=>x.RestaurantId == context.Message.RestaurantId);

                foreach (var item in context.Message.OrderItems)
                {
                    foreach (var menu in menus)
                    {
                        if(menu.MenuId == item.MenuId)
                        {
                            var restaurantMenu = new RestaurantMenu();
                            restaurantMenu.RestaurantId = context.Message.RestaurantId;
                            item.Quantity = menu.Quantity - item.Quantity;
                            restaurantMenu.Menu = item;
                            await _restaurantRepository.UpdateRestaurantMenu(restaurantMenu);
                        }
                    }                   
                }

                await _bus.Publish(new OrderReadyEvent(restaurant.Address, context.Message.Address, context.Message.OrderId));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured during delivery", ex.Message);
                throw ex;
            }
        }
    }
}
