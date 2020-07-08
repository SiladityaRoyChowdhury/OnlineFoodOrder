using MassTransit;
using Omf.Common.Events;
using Omf.OrderManagementService.Data;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Omf.OrderManagementService.DomainModel;

namespace Omf.OrderManagementService.EventConsumers
{
    public class DeliveryEventConsumer : IConsumer<OrderReadyEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<DeliveryEventConsumer> _logger;

        public DeliveryEventConsumer(IOrderRepository orderRepository, ILogger<DeliveryEventConsumer> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<OrderReadyEvent> context)
        {
            try
            {
                _logger.LogInformation("Delivery Message received");
                var order = await _orderRepository.GetOrderAsync(context.Message.OrderId);
                order.Status = OrderStatus.Delivered.ToString();
                await _orderRepository.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured", ex.Message);
                throw ex;
            }
        }
    }
}
