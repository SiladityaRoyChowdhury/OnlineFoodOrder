using Omf.OrderManagementService.Data;
using Omf.OrderManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.OrderManagementService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrders() 
            => await _orderRepository.GetAllOrdersAsync();

        public Task<IEnumerable<Order>> GetUserOrders(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
