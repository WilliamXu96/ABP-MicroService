using AutoMapper;
using Volo.Abp.AutoMapper;

namespace Volo.Abp.Identity
{
    public class AbpIdentityApplicationModuleAutoMapperProfile : Profile
    {
        public AbpIdentityApplicationModuleAutoMapperProfile()
        {
            CreateMap<IdentityUser, IdentityUserDto>()
                .Ignore(dto => dto.OrgIdToName)
                .Ignore(dto => dto.Jobs)
                .MapExtraProperties();

            CreateMap<IdentityRole, IdentityRoleDto>()
                .MapExtraProperties();

            CreateMap<IdentityUser, ProfileDto>()
                .MapExtraProperties();
        }
    }
}