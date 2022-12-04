using Business.PrintTemplateManagement;
using Business.PrintTemplateManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Business.Controllers
{
    [RemoteService]
    [Area("Business")]
    [Route("api/business/print-template")]
    public class PrintTemplateController : AbpController
    {
        private readonly IPrintTemplateAppService _PrintTemplateAppService;

        public PrintTemplateController(IPrintTemplateAppService PrintTemplateAppService)
        {
            _PrintTemplateAppService = PrintTemplateAppService;
        }

        [HttpPost]
        [Route("data-post")]
        public Task<PrintTemplateDto> DataPost(CreateOrUpdatePrintTemplateDto input)
        {
            return _PrintTemplateAppService.DataPost(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _PrintTemplateAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<PrintTemplateDto> Get(Guid id)
        {
            return _PrintTemplateAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<PrintTemplateDto>> GetAll(GetPrintTemplateInputDto input)
        {
            return _PrintTemplateAppService.GetAll(input);
        }

        [HttpGet]
        [Route("pdf/{id}")]
        public Task<dynamic> CreatePdf(Guid id)
        {
            return _PrintTemplateAppService.CreatePdf(id);
        }
    }
}
