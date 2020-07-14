using System;
using System.Collections.Generic;

namespace Omf.RestaurantManagementService.DomainModel
{
    public class Restaurant
    {
        public string RestaurantId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rating { get; set; }
        public string Location { get; set; }
        public string ListedCity { get; set; }
        public decimal ApproxCost { get; set; }
        public ICollection<RestaurantMenu> RestaurantMenus { get; set; }
    }
}
