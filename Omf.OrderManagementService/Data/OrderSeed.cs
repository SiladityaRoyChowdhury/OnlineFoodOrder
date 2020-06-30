using Microsoft.EntityFrameworkCore;
using Omf.OrderManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.OrderManagementService.Data
{
    public class OrderSeed
    {
        public static async Task SeedSync(OrderContext context)
        {
            try
            {
                context.Database.Migrate();

                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(GetPreconfiguredOrders());
                    await context.SaveChangesAsync();
                }

                if (!context.Menus.Any())
                {
                    context.Menus.AddRange(GetPreconfiguredMenus());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static ICollection<Menu> GetPreconfiguredMenus()
        {
            return new List<Menu>()
            {
                new Menu() { MenuId="M1", Item="Chocolate Fantasy", Price = 191.0M, Quantity = 20, OrderId ="O1" },
                new Menu() { MenuId="M3", Item="Gulab Jamun (Pack Of 10)", Price=212.0M, Quantity = 20, OrderId ="O1" },
                new Menu() { MenuId="M4", Item="Gulkand Shot (Pack Of 5)", Price=112.0M, Quantity = 20, OrderId ="O1" },
                new Menu() {MenuId="M2", Item="Pan Cake (Pack Of 6)",Price=248.0M, Quantity = 20, OrderId ="O2"}
            };
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>()
            {
                new Order() { OrderTime=DateTime.Now, OrderId="O1", RestaurantId="R1",  
                                Status = "InProgress", UserId= new Guid(), Address="10,  Banshankari"},
                new Order() { OrderTime=DateTime.Now, OrderId="O2", RestaurantId="R2",
                                Status = "InProgress", UserId= new Guid(), Address="10,  Banshankari"}
            };
        }
    }
}

