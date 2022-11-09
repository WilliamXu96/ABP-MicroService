using AutoMapper;
using Business.PrintTemplateManagement.Dto;
using Business.Models;

namespace Business.PrintTemplateManagement
{
    public class PrintTemplateAutoMapperProfile : Profile
    {
        public PrintTemplateAutoMapperProfile()
        {
            CreateMap<PrintTemplate, PrintTemplateDto>();
            CreateMap<CreateOrUpdatePrintTemplateDto, PrintTemplate>();
        }
    }
}
