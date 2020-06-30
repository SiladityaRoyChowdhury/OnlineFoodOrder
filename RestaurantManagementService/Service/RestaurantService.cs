using Omf.RestaurantManagementService.Data.Repository;
using Omf.RestaurantManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.RestaurantManagementService.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<IEnumerable<RestaurantMenu>> SearchRestaurant(string id, string name, string location,
            string budget, string rating, string food)
        {
            var restaurants = await GetRestarantMenu();

            var filteredRestaurant = from r in restaurants
                                     where (id == null || r.RestaurantId.ToLower() == id.ToLower())
                                        && (name == null || r.Restaurant.Name.ToLower().Contains(name.ToLower()))
                                        && (location == null || r.Restaurant.Location.ToLower().Contains(location.ToLower()) || r.Restaurant.ListedCity.ToLower().Contains(location.ToLower()) ||
                                            location.ToLower().Contains(r.Restaurant.Location.ToLower()) || location.ToLower().Contains(r.Restaurant.ListedCity.ToLower()))
                                        && (budget == null || r.Restaurant.ApproxCost >= Convert.ToDecimal(budget))
                                        && (rating == null || Convert.ToDecimal(r.Restaurant.Rating) <= Convert.ToDecimal(rating))
                                        && (food == null || r.Menu.Item.ToLower().Contains(food.ToLower()) || food.ToLower().Contains(r.Menu.Item.ToLower()))
                                     select r;

            return filteredRestaurant;

        }

        private async Task<IEnumerable<RestaurantMenu>> GetRestarantMenu()
        {
            var restaurantMenus = await _restaurantRepository.GetAllRestaurantMenusAsync();
            var restaurants = await _restaurantRepository.GetAllRestaurantAsync();
            var menus = await _restaurantRepository.GetAllMenuAsync();

            foreach (var item in restaurantMenus)
            {
                foreach (var res in from res in restaurants
                                    where item.RestaurantId == res.RestaurantId
                                    select res)
                {
                    item.Restaurant = res;
                }

                foreach (var menu in from menu in menus
                                     where item.MenuId == menu.MenuId
                                     select menu)
                {
                    item.Menu = menu;
                }
            }
            return restaurantMenus;
        }
    }
}
