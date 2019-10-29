using AutoMapper;
using Pard.Domain.Entities.Identity;

namespace Pard.Application.ViewModels.Mappings
{
    public class CredentialsViewModelToEntityMappingProfile : Profile
    {
        public CredentialsViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, AppUser>().ForMember(x => x.UserName, map => map.MapFrom(x => x.Login));
        }
    }
}
