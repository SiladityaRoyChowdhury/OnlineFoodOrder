using Omf.OrderManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.OrderManagementService.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderAsync(int orderId);
        Task<IEnumerable<Order>> GetUserOrders(Guid userId);
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
    }
}