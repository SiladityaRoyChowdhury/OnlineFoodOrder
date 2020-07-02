using Microsoft.EntityFrameworkCore;
using Omf.OrderManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.OrderManagementService.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _orderContext;
        public OrderRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
            ((DbContext)orderContext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _orderContext.Orders.Include(x=>x.OrderItems).ToListAsync();
            return orders;
        }

        public Task<IEnumerable<Order>> GetUserOrders(Guid userId)
        {
            var orders = _orderContext.Orders.Where(x => x.UserId == userId);
            return (Task<IEnumerable<Order>>)orders;
        }
    }
}