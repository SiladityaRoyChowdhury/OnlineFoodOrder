using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Omf.OrderManagementService.DomainModel;
using Omf.OrderManagementService.Services;
using Omf.OrderManagementService.ViewModel;

namespace Omf.OrderManagementService.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet]
        [Route("GetUserOrders")]
        public async Task<ActionResult> GetUserOrders(Guid userId)
        {
            var orders = await _orderService.GetUserOrders(userId);
            return Ok(orders);
        }
    }
}