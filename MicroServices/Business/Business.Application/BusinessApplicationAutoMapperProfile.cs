using AutoMapper;
using Business.BaseData;
using Business.BaseData.DataDictionaryManagement.Dto;
using Business.BaseData.EmployeeManagement.Dto;
using Business.BaseData.JobManagement.Dto;
using Business.BaseData.OrganizationManagement.Dto;

namespace Business
{
    public class BusinessApplicationAutoMapperProfile : Profile
    {
        public BusinessApplicationAutoMapperProfile()
        {
            CreateMap<DataDictionary, DictionaryDto>();

            CreateMap<DataDictionaryDetail, DictionaryDetailDto>();

            CreateMap<Organization, OrganizationDto>();

            CreateMap<Employee, EmployeeDto>();

            CreateMap<Job, JobDto>();
        }
    }
}
