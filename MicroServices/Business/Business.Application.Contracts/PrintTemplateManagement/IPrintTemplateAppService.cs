using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Business.PrintTemplateManagement.Dto;

namespace Business.PrintTemplateManagement
{
    public interface IPrintTemplateAppService : IApplicationService
    {
        Task<PrintTemplateDto> Get(Guid id);

        Task<PagedResultDto<PrintTemplateDto>> GetAll(GetPrintTemplateInputDto input);

        Task<PrintTemplateDto> DataPost(CreateOrUpdatePrintTemplateDto input);

        Task Delete(List<Guid> ids);

        Task<dynamic> CreatePdf(Guid id);
    }
}
