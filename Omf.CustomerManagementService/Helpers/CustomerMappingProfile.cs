using Omf.CustomerManagementService.DomainModel;
using Omf.CustomerManagementService.ViewModel;
using AutoMapper;

namespace Omf.CustomerManagementService.Helpers
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserDetails, User>();
        }      
    }
}