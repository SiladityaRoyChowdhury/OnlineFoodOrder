using Omf.CustomerManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Omf.CustomerManagementService.Data.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<User>> GetAllCustomersAsync();
        Task<User> GetCustomerAsync(string userEmail);
        Task<User> GetUserAsync(Guid userId);
        Task CreateCustomer(User user);
        Task UpdateCustomerer(User user);
        Task DeleteCustomer(Guid userId);
    }
}