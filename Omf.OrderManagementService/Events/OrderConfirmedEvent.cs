using System;
using System.Collections.Generic;
using Omf.OrderManagementService.DomainModel;

namespace Omf.Common.Events
{
    public class OrderConfirmedEvent
    {
        public OrderConfirmedEvent(int orderId, string restaurantId, IEnumerable<Menu> orderItems, string address)
        {
            OrderId = orderId;
            RestaurantId = restaurantId;
            OrderItems = orderItems;
            Address = address;
        }
        public int OrderId { get; set; }
        public string RestaurantId { get; set; }
        public IEnumerable<Menu> OrderItems { get; set; }
        public string Address { get; set; }
    }
}