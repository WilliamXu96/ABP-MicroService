using AutoMapper;
using Business.BaseData;
using Business.BaseData.DataDictionaryManagement.Dto;
using Business.BaseData.EmployeeManagement.Dto;
using Business.BaseData.JobManagement.Dto;
using Business.BaseData.OrganizationManagement.Dto;
using Business.Systems.AuditLoggingManagement.Dto;
using Volo.Abp.AuditLogging;

namespace Business
{
    public class BusinessApplicationAutoMapperProfile : Profile
    {
        public BusinessApplicationAutoMapperProfile()
        {
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

            CreateMap<Employee, EmployeeDto>();

            CreateMap<Job, JobDto>();
        }
    }
}
