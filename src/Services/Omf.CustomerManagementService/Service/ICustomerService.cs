using Omf.CustomerManagementService.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Omf.CustomerManagementService.Service
{
    public interface ICustomerService
    {
        Task<User> AuthenticateCustomer(string username, string password);
        Task<IEnumerable<User>> GetAllCustomers();
        Task<User> GetUserById(Guid userId);
        Task CreateCustomer(User user, string password);
        Task UpdateCustomer(User user, string password = null);
        Task DeleteCustomer(Guid id);
    }
}