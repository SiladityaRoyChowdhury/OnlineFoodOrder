using Microsoft.EntityFrameworkCore;
using Omf.CustomerManagementService.Data;
using Omf.CustomerManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.OrderManagementService.Data
{
    public class CustomerSeed
    {
        public static async Task SeedSync(CustomerContext context)
        {
            try
            {
                context.Database.Migrate();

                if (!context.Users.Any())
                {
                    context.Users.AddRange(GetPreconfiguredUsers());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static IEnumerable<User> GetPreconfiguredUsers()
        {
            return new List<User>()
            {
                new User() { UserId= Guid.NewGuid(), FirstName="test", LastName = "test", UserEmail = "test@email.com"  }
            };
        }
    }
}
