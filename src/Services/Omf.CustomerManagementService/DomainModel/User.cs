using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.CustomerManagementService.DomainModel
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}