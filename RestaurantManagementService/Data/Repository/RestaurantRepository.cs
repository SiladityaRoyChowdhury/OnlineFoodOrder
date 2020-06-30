using Microsoft.EntityFrameworkCore;
using Omf.RestaurantManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.RestaurantManagementService.Data.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantContext _restaurantContext;
        public RestaurantRepository(RestaurantContext restaurantcontext)
        {
            _restaurantContext = restaurantcontext;
            ((DbContext)restaurantcontext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
        public async Task<IEnumerable<RestaurantMenu>> GetAllRestaurantMenusAsync()
        { 
            var list = await _restaurantContext.RestaurantMenus.ToListAsync();
            return list;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantAsync()
        {
            var list = await _restaurantContext.Restaurants.ToListAsync();
            return list;
        }

        public async Task<IEnumerable<Menu>> GetAllMenuAsync()
        {
            var list = await _restaurantContext.Menus.ToListAsync();
            return list;
        }
    }
}