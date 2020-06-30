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
            var orders = await _orderContext.Orders.ToListAsync();
            return orders;
        }

        Task<IEnumerable<Order>> IOrderRepository.GetUserOrders(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
