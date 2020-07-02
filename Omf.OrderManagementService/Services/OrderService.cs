using Omf.OrderManagementService.Data;
using Omf.OrderManagementService.DomainModel;
using Omf.OrderManagementService.ViewModel;
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
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            return RemoveOrderData(orders);
        }

        public async Task<IEnumerable<Order>> GetUserOrders(Guid userId)
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return RemoveOrderData(orders).Where(x => x.UserId == userId);
        }

        private IEnumerable<Order> RemoveOrderData(IEnumerable<Order> orders)
        {
            foreach (var item in from order in orders
                                 from item in order.OrderItems
                                 select item)
            {
                item.Order = null;
            }
            return orders;
        }
    }
}