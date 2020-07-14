using Omf.Common.Events;
using Omf.OrderManagementService.Data;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;
using Omf.OrderManagementService.DomainModel;

namespace Omf.OrderManagementService.EventConsumers
{
    public class PaymentEventConsumer : IConsumer<PaymentInitiatedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<PaymentEventConsumer> _logger;

        public PaymentEventConsumer(IOrderRepository orderRepository, ILogger<PaymentEventConsumer> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentInitiatedEvent> context)
        {
            try
            {
                _logger.LogInformation("Payment Message received");
                var order = await _orderRepository.GetOrderAsync(context.Message.OrderId);
                order.Status = OrderStatus.OrderAccepted.ToString();
                await _orderRepository.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured while Payment"+ ex.Message, ex.Message);
                throw ex;
            }
        }
    }
}