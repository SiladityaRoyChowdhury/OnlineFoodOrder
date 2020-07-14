using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Omf.OrderManagementService.DomainModel
{
    public class Order
    {    
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public string RestaurantId { get; set; }
        public IEnumerable<Menu> OrderItems { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }
        public string Address { get; set; }
        public decimal TotalAmount => OrderItems.Sum(x => x.Quantity * x.Price);
    }

    public enum OrderStatus
    {
        InProgress = 0,
        OrderAccepted = 1,
        PreparingOrder = 2,
        OrderReady = 3,
        OutForDelivery = 4,
        Delivered = 5,
        FailToOrder = 6,
        OrderCancelled = 7
    }
}