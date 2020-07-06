using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Omf.ReviewManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.ReviewManagementService.Data
{
    public class Seed
    {
        public static async Task SeedSync(ReviewContext context)
        {
            try
            {
                context.Database.Migrate();

                if (!context.Reviews.Any())
                {
                    context.Reviews.AddRange(GetPreconfiguredReview());
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        static IEnumerable<Review> GetPreconfiguredReview()
        {
            return new List<Review>()
            {
                new Review() { RestaurantId="R1", Rating= "3", Comments = "good", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R2", Rating="2", Comments = "food is not good", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R3", Rating="3", Comments = "Food is ok", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R4",Rating="2", Comments = "good", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R5",Rating="1", Comments = "Food is Pathetic", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R6",Rating="2", Comments = "Service is not good", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R7",Rating="4", Comments = "good", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R8",Rating="5", Comments = "Tasty food", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R9",Rating="4", Comments = "very good food", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R10",Rating="3", Comments = "good food", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R11",Rating="2", Comments = "bad food", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R12",Rating="4", Comments = "good food", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R13",Rating="5", Comments = "Awesome service, good Food", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R14",Rating="5", Comments = "Very good", ReviewDateTime = DateTime.Now, UserId = new Guid() },
                new Review() { RestaurantId="R15",Rating="3", Comments = "Very good", ReviewDateTime = DateTime.Now, UserId = new Guid() }
            };
        }
    }
}