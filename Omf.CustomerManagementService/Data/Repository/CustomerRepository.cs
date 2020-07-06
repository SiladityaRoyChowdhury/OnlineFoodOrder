using Microsoft.EntityFrameworkCore;
using Omf.CustomerManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Omf.CustomerManagementService.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly  CustomerContext _customerContext;
        public CustomerRepository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
            ((DbContext)customerContext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task CreateCustomer(User user)
        {
            await _customerContext.Users.AddAsync(user);
            await _customerContext.SaveChangesAsync();
        }

        public async Task DeleteCustomer(Guid userId)
        {
            var user = _customerContext.Users.Find(userId);
            if (user != null)
            {
                _customerContext.Users.Remove(user);
                await _customerContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllCustomersAsync()
        {
            var customers = await _customerContext.Users.ToListAsync();
            return customers;
        }

        public async Task<User> GetUserAsync(Guid userId) => await _customerContext.Users.FindAsync(userId);

        public Task<User> GetCustomerAsync(string userEmail) => _customerContext.Users.FirstOrDefaultAsync(x => x.UserEmail.Equals(userEmail));

        public async Task UpdateCustomerer(User user)
        {
            _customerContext.Update(user);
            await _customerContext.SaveChangesAsync();
        }
    }
}