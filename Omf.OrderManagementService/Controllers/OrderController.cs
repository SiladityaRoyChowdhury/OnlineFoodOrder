using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);         
        }

        [HttpGet("{orderId}", Name = "GetOrder")]
        //[Route("GetOrder/{orderId}")]
        public async Task<ActionResult> GetOrder(int orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            return Ok(order);
        }

        [HttpGet]
        [Route("GetUserOrders/{userId}")]
        public async Task<ActionResult> GetUserOrders(Guid userId)
        {
            var orders = await _orderService.GetUserOrders(userId);
            return Ok(orders);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            order.Status = OrderStatus.InProgress.ToString();
            order.OrderTime = DateTime.Now;
            _logger.LogInformation("Creating new order");

            try
            {                
                await _orderService.CreateOrder(order);
                _logger.LogInformation("Order added to database Successfully");
                order.Status = OrderStatus.OrderAccepted.ToString();
                order.OrderItems.ToList().ForEach(s => s.Order = null);
                return CreatedAtRoute("GetOrder", new { orderId = order.OrderId }, order);
            }
            catch(Exception ex)
            {
                _logger.LogError("An error occured while adding new order", ex.Message);
                order.Status = OrderStatus.FailToOrder.ToString();
                return Ok(order);
            }
        }

        [HttpPost]
        [Route("Update")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
                        await _orderService.CreateOrder(order);
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