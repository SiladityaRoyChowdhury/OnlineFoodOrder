using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.OrderManagementService.ViewModel
{
    public class OrderModel
    {
        public DateTime OrderTime { get; set; }
        public string OrderId { get; set; }
        public string RestaurantId { get; set; }
        public IEnumerable<OrderItems> OrderItems { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class OrderItems
    {
        public string MenuId { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}