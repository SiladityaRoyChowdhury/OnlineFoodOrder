﻿using Omf.RestaurantManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.RestaurantManagementService.Service
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantMenu>> SearchRestaurant(string id, string name, string location,
            string budget, string rating, string food);
    }
}