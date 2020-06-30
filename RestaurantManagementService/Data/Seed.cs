using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Omf.RestaurantManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.RestaurantManagementService.Data
{
    public class Seed
    {
        public static async Task SeedSync(RestaurantContext context)
        {
            try
            {
                context.Database.Migrate();

                if (!context.Menus.Any())
                {
                    context.Menus.AddRange(GetPreconfiguredMenus());
                    await context.SaveChangesAsync();
                }

                if (!context.Restaurants.Any())
                {
                    context.Restaurants.AddRange(GetPreconfiguredRestaurant());
                    await context.SaveChangesAsync();
                }

                if (!context.RestaurantMenus.Any())
                {
                    context.RestaurantMenus.AddRange(GetPreconfiguredRestaurantMenus());
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private static IEnumerable<RestaurantMenu> GetPreconfiguredRestaurantMenus()
        {
            return new List<RestaurantMenu>()
            {
                new RestaurantMenu() { MenuId = "M1", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M1", RestaurantId="R2"},
                new RestaurantMenu() { MenuId = "M2", RestaurantId="R2"},
                new RestaurantMenu() { MenuId = "M3", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M4", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M5", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M6", RestaurantId="R2"},
                new RestaurantMenu() { MenuId = "M7", RestaurantId="R2"},
                new RestaurantMenu() { MenuId = "M8", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M9", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M10", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M11", RestaurantId="R2"},
                new RestaurantMenu() { MenuId = "M12", RestaurantId="R2"},
                new RestaurantMenu() { MenuId = "M13", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M14", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M15", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M16", RestaurantId="R2"},
                new RestaurantMenu() { MenuId = "M17", RestaurantId="R2"},
                new RestaurantMenu() { MenuId = "M18", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M19", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M20", RestaurantId="R1"},
                new RestaurantMenu() { MenuId = "M21", RestaurantId="R2"},
                new RestaurantMenu() { MenuId = "M22", RestaurantId="R3"},
                new RestaurantMenu() { MenuId = "M23", RestaurantId="R4"},
                new RestaurantMenu() { MenuId = "M24", RestaurantId="R5"},
                new RestaurantMenu() { MenuId = "M25", RestaurantId="R6"},
                new RestaurantMenu() { MenuId = "M26", RestaurantId="R7"},
                new RestaurantMenu() { MenuId = "M27", RestaurantId="R8"},
                new RestaurantMenu() { MenuId = "M28", RestaurantId="R9"},
                new RestaurantMenu() { MenuId = "M29", RestaurantId="R10"},
                new RestaurantMenu() { MenuId = "M30", RestaurantId="R11"},
                new RestaurantMenu() { MenuId = "M31", RestaurantId="R12"},
                new RestaurantMenu() { MenuId = "M32", RestaurantId="R13"},
                new RestaurantMenu() { MenuId = "M3", RestaurantId="R14"},
                new RestaurantMenu() { MenuId = "M4", RestaurantId="R15"}
            };
        }

        static IEnumerable<Restaurant> GetPreconfiguredRestaurant()
        {
            return new List<Restaurant>()
            {
                new Restaurant() { RestaurantId="R1",Rating= "3", Address = "Shoes for next century", Name = "World Star", ApproxCost = 199.5M, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R2",Rating="2", Address = "will make you world champions", Name = "White Line", ApproxCost= 88.50M, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R3",Rating="3", Address = "You have already won gold medal", Name = "Prism White Shoes", ApproxCost = 129, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R4",Rating="2", Address = "Olympic runner", Name = "Foundation Hitech", ApproxCost = 12, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R5",Rating="1", Address = "Roslyn Red Sheet", Name = "Roslyn White", ApproxCost = 188.5M, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R6",Rating="2", Address = "Lala Land", Name = "Blue Star", ApproxCost = 112, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R7",Rating="4", Address = "High in the sky", Name = "Roslyn Green", ApproxCost = 212, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R8",Rating="5", Address = "Light as carbon", Name = "Deep Purple", ApproxCost = 238.5M, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R9",Rating="4", Address = "High Jumper", Name = "AddRestaurantIdas", ApproxCost = 129, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R10",Rating="3", Address = "Dunker", Name = "Elequent", ApproxCost = 12, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R11",Rating="2", Address = "All round", Name = "Inredeible", ApproxCost = 248.5M, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R12",Rating="4", Address = "ApproxCostsless", Name = "London Sky", ApproxCost = 412, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R13",Rating="5", Address = "Tennis Star", Name = "Elequent", ApproxCost = 123, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R14",Rating="5", Address = "Wimbeldon", Name = "London Star", ApproxCost = 218.5M, Location = "Bangalore", ListedCity = "Bangalore" },
                new Restaurant() { RestaurantId="R15",Rating="3", Address = "Rolan Garros", Name = "Paris Blues", ApproxCost = 312, Location = "Bangalore", ListedCity = "Bangalore"}

            };
        }

        private static IEnumerable<Menu> GetPreconfiguredMenus()
        {
            return new List<Menu>()
            { 
                new Menu() {MenuId="M1", Item="Chocolate Fantasy", Price = 191.0M, Quantity = 20},
                new Menu() {MenuId="M2", Item="Pan Cake (Pack Of 6)",Price=248.0M, Quantity = 20},
                new Menu() {MenuId="M3", Item="Gulab Jamun (Pack Of 10)",Price=212.0M, Quantity = 20},
                new Menu() {MenuId="M4", Item="Gulkand Shot (Pack Of 5)",Price=112.0M, Quantity = 20},
                new Menu() {MenuId="M5", Item="Chocolate Decadence",Price=76.0M, Quantity = 20},
                new Menu() {MenuId="M6", Item="CheeseCake (Pack Of 2)",Price=229.0M, Quantity = 20},
                new Menu() {MenuId="M7", Item="Red Velvet Slice Cake",Price=242.0M, Quantity = 20},
                new Menu() {MenuId="M8", Item="Red Velvet Slice Cake & Cheese Cake",Price=237.0M, Quantity = 20},
                new Menu() {MenuId="M9", Item="Red Velvet Slice & Chocolate Decadence Cake",Price=235.0M, Quantity = 20},
                new Menu() {MenuId="M10", Item="Hazelnut Brownie",Price=154.0M, Quantity = 20},
                new Menu() {MenuId="M11", Item="Moments",Price=164.0M, Quantity = 20},
                new Menu() {MenuId="M12", Item="Red Velvet Cake With Butter Cream Frosting",Price=139.0M, Quantity = 20},
                new Menu() {MenuId="M13", Item="Red Velvet Slice Cake (Pack of 2)",Price=204.0M, Quantity = 20},
                new Menu() {MenuId="M14", Item="Red Velvet Slice Cake & Cheese Cake",Price=65.0M, Quantity = 20},
                new Menu() {MenuId="M15", Item="Red Velvet Slice Cake (Pack of 1)",Price=127.0M, Quantity = 20},
                new Menu() {MenuId="M16", Item="Valentine Red Velvet Jar",Price=109.0M, Quantity = 20},
                new Menu() {MenuId="M17", Item="Valentine Chocolate Jar",Price=89.0M, Quantity = 20},
                new Menu() {MenuId="M18", Item="Valentines Jar Combo",Price=169.0M, Quantity = 20},
                new Menu() {MenuId="M19", Item="Pink Guava 500 ML",Price=151.0M, Quantity = 20},
                new Menu() {MenuId="M20", Item="Oreo Vanilla 500 ML",Price=149.0M, Quantity = 20},
                new Menu() {MenuId="M21", Item="Cookie Crumble 500 ML",Price=63.0M, Quantity = 20},
                new Menu() {MenuId="M22", Item="Chocolate Fantasy",Price=139.0M, Quantity = 20},
                new Menu() {MenuId="M23", Item="Gulkand-E-Bahar",Price=232.0M, Quantity = 20},
                new Menu() {MenuId="M24", Item="Pan Cake",Price=65.0M, Quantity = 20},
                new Menu() {MenuId="M25", Item="Hazelnut Brownie (Pack Of 1)",Price=118.0M, Quantity = 20},
                new Menu() {MenuId="M26", Item="Gulab Jamun (Pack Of 2)",Price=248.0M, Quantity = 20},
                new Menu() {MenuId="M27", Item="Plum Cake",Price=174.0M, Quantity = 20},
                new Menu() {MenuId="M28", Item="Red Velvet Cake With Butter Cream Frosting",Price=130.0M, Quantity = 20},
                new Menu() {MenuId="M29", Item="Chocolate Mud Cake",Price=137.0M, Quantity = 20},
                new Menu() {MenuId="M30", Item="CheeseCake",Price=187.0M, Quantity = 20},
                new Menu() {MenuId="M31", Item="Chocolate Decadence",Price=248.0M, Quantity = 20},
                new Menu() {MenuId="M32", Item="Red Velvet Slice Cake",Price=86.0M, Quantity = 20}
            };
        }
    }
}
