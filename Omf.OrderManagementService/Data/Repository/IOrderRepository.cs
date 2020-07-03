using Omf.OrderManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.OrderManagementService.Data
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderAsync(int orderId);
        Task<IEnumerable<Order>> GetUserOrders(Guid userId);
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
    }
}