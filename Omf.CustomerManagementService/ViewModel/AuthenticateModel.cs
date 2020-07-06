using System.ComponentModel.DataAnnotations;

namespace Omf.CustomerManagementService.ViewModel
{
    public class AuthenticateModel
    {
        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}