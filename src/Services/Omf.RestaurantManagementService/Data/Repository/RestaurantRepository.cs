using Microsoft.EntityFrameworkCore;
using Omf.RestaurantManagementService.DomainModel;
using System.Collections.Generic;
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
            var list = await _restaurantContext.RestaurantMenus
                .Include(x=>x.Restaurant)
                .Include(x=>x.Menu)
                .ToListAsync();
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

        public async Task UpdateRestaurant(Restaurant restaurant)
        {
            _restaurantContext.Update(restaurant);
            await _restaurantContext.SaveChangesAsync();
        }

        public async Task UpdateRestaurantMenu(RestaurantMenu restaurant)
        {
            _restaurantContext.Update(restaurant);
            await _restaurantContext.SaveChangesAsync();
        }
    }
}