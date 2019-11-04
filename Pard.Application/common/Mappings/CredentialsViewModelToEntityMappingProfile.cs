using AutoMapper;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Identity;

namespace Pard.Application.Common.Mappings
{
    public class CredentialsViewModelToEntityMappingProfile : Profile
    {
        public CredentialsViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, AppUser>().ForMember(x => x.UserName, map => map.MapFrom(x => x.Login));
        }
    }
}
