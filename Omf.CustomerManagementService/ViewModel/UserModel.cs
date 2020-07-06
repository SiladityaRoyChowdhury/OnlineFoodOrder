using System;

namespace Omf.CustomerManagementService.ViewModel
{
  public class UserModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
    }
}