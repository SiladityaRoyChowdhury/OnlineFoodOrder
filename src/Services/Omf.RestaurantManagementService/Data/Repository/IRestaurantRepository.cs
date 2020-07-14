using Omf.RestaurantManagementService.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Omf.RestaurantManagementService.Data.Repository
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<RestaurantMenu>> GetAllRestaurantMenusAsync();
        Task<IEnumerable<Restaurant>> GetAllRestaurantAsync();
        Task<IEnumerable<Menu>> GetAllMenuAsync();
        Task UpdateRestaurant(Restaurant restaurant);
        Task UpdateRestaurantMenu(RestaurantMenu restaurantMenu);
    }
}