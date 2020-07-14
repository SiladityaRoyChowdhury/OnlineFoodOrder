using System;
using System.Collections.Generic;

namespace Omf.RestaurantManagementService.DomainModel
{
    public class RestaurantMenu
    {
        public string RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public string MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}