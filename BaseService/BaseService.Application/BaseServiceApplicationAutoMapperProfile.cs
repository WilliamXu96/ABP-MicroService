using AutoMapper;
using BaseService.BaseData;
using BaseService.BaseData.DataDictionaryManagement.Dto;
using BaseService.BaseData.JobManagement.Dto;
using BaseService.BaseData.OrganizationManagement.Dto;
using BaseService.Systems;
using BaseService.Systems.AuditLoggingManagement.Dto;
using BaseService.Systems.MenuManagement.Dto;
using BaseService.Systems.UserManagement.Dto;
using BaseService.Systems.UserRoleMenusManagement.Dto;
using Volo.Abp.AuditLogging;
using Volo.Abp.Identity;

namespace BaseService
{
    public class BaseServiceApplicationAutoMapperProfile : Profile
    {
        public BaseServiceApplicationAutoMapperProfile()
        {
            CreateMap<IdentityUser, BaseIdentityUserDto>();

            CreateMap<AuditLog, AuditLogDto>()
                .ForMember(t => t.EntityChanges, option => option.MapFrom(l => l.EntityChanges))
                .ForMember(t => t.Actions, option => option.MapFrom(l => l.Actions));
            CreateMap<EntityChange, EntityChangeDto>()
                 .ForMember(t => t.PropertyChanges, option => option.MapFrom(l => l.PropertyChanges));

            CreateMap<AuditLogAction, AuditLogActionDto>();
            CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            CreateMap<DataDictionary, DictionaryDto>();

            CreateMap<DataDictionaryDetail, DictionaryDetailDto>();

            CreateMap<Organization, OrganizationDto>()
                .ForMember(dto => dto.Label, opt => opt.MapFrom(src => src.Name));

            CreateMap<Job, JobDto>();

            CreateMap<Menu, MenuDto>();
            CreateMap<Menu, MenusListDto>();
        }
    }
}
