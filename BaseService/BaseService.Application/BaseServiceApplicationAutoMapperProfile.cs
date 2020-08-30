using AutoMapper;
using BaseService.BaseData;
using BaseService.BaseData.DataDictionaryManagement.Dto;
using BaseService.BaseData.JobManagement.Dto;
using BaseService.BaseData.OrganizationManagement.Dto;
using BaseService.Systems.AuditLoggingManagement.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AuditLogging;

namespace BaseService
{
    public class BaseServiceApplicationAutoMapperProfile : Profile
    {
        public BaseServiceApplicationAutoMapperProfile()
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

            CreateMap<Job, JobDto>();
        }
    }
}
