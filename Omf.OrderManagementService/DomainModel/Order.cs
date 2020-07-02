using System;
using System.Collections.Generic;
using System.Linq;

namespace Omf.OrderManagementService.DomainModel
{
    public class Order
    {
        public DateTime OrderTime { get; set; }
        public string OrderId { get; set; }
        public string RestaurantId { get; set; }
        public IEnumerable<Menu> OrderItems { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }
        public string Address { get; set; }
        public decimal TotalAmount => OrderItems.Sum(x => x.Quantity * x.Price);
    }
}