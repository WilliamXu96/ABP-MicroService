using AutoMapper;
using Business.BaseData;
using Business.BaseData.DataDictionaryManagement.Dto;

namespace Business
{
    public class BusinessApplicationAutoMapperProfile : Profile
    {
        public BusinessApplicationAutoMapperProfile()
        {
            CreateMap<DataDictionary, DictionaryDto>();

            CreateMap<DataDictionaryDetail, DictionaryDetailDto>();
        }
    }
}
