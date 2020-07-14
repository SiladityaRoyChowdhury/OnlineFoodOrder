using System.ComponentModel.DataAnnotations;

namespace Omf.CustomerManagementService.ViewModel
{
    public class UserDetails
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}