using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Omf.Common.Events;
using Omf.OrderManagementService.DomainModel;
using Omf.OrderManagementService.Services;

namespace Omf.OrderManagementService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        private IBus _bus;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger, IBus bus)
        {
            _orderService = orderService;
            _logger = logger;
            _bus = bus;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var orders = await _orderService.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while getting order", ex.Message);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{orderId}", Name = "GetOrder")]
        public async Task<ActionResult> GetOrder(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(orderId);
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while getting order", ex.Message);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetUserOrders/{userId}")]
        public async Task<ActionResult> GetUserOrders(Guid userId)
        {
            try
            {
                var orders = await _orderService.GetUserOrders(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while getting order", ex.Message);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task CreateOrder([FromBody] Order order)
        {
            order.Status = OrderStatus.InProgress.ToString();
            order.OrderTime = DateTime.Now;
            _logger.LogInformation("Creating new order");

            try
            {                
                await _orderService.CreateOrder(order);
                _logger.LogInformation("Order added to database Successfully");

                await _bus.Publish(new PaymentInitiatedEvent(order.OrderId));
                _logger.LogInformation("Payment is Successfull");

                order.Status = OrderStatus.PreparingOrder.ToString();
                await _orderService.UpdateOrder(order);
                _logger.LogInformation("Order is getting Prepared");

                //order.OrderItems.ToList().ForEach(s => s.Order = null);
                //return CreatedAtRoute("GetOrder", new { orderId = order.OrderId }, order);

                await _bus.Publish(new OrderConfirmedEvent(order.OrderId, order.RestaurantId, order.OrderItems,
                        order.Address));
                _logger.LogInformation("Order Confirmed event");
            }
            catch(Exception ex)
            {
                _logger.LogError("An error occured while adding new order", ex.Message);
                order.Status = OrderStatus.FailToOrder.ToString();
                //return Ok(order);
            }
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateOrder([FromBody] Order order)
        {
            order.OrderTime = DateTime.Now;
            _logger.LogInformation("Creating new order");

            try
            {
                if (await _orderService.GetOrderAsync(order.OrderId) != null)
                {
                    if (order.Status == OrderStatus.OrderAccepted.ToString())
                    {
                        order.Status = OrderStatus.InProgress.ToString();
                        await _orderService.UpdateOrder(order);
                        _logger.LogInformation("Order Updated to database Successfully");
                        order.Status = OrderStatus.OrderAccepted.ToString();
                    }
                    if (order.Status == OrderStatus.OrderCancelled.ToString())
                    {
                        await _orderService.UpdateOrder(order);
                        _logger.LogInformation("Order Cancelled Successfully");
                        order.Status = OrderStatus.OrderCancelled.ToString();
                    }
                    order.OrderItems.ToList().ForEach(s => s.Order = null);
                    return CreatedAtRoute("GetOrder", new { orderId = order.OrderId }, order);
                }
                _logger.LogInformation("Order details not found");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while updating order", ex.Message);
                order.Status = OrderStatus.FailToOrder.ToString();
                return Ok(order);
            }
        }
    }
}