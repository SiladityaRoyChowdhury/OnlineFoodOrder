using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.OrderManagementService.DomainModel
{
    public class Menu
    {
        public string MenuId { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string OrderId { get; set; }
    }
}