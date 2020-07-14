using System.Collections.Generic;

namespace Omf.RestaurantManagementService.DomainModel
{
    public class Menu
    {
        public string MenuId { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ICollection<RestaurantMenu> RestaurantMenus { get; set; }
    }
}