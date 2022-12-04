using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Business.PrintTemplateManagement.Dto;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Business.Permissions;
using Volo.Abp;
using Magicodes.ExporterAndImporter.Pdf;
using WkHtmlToPdfDotNet;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Business.PrintTemplateManagement
{
    [Authorize(BusinessPermissions.PrintTemplate.Default)]
    public class PrintTemplateAppService : ApplicationService, IPrintTemplateAppService
    {
        private const string FormName = "PrintTemplate";
        private IRepository<PrintTemplate, Guid> _repository;

        public PrintTemplateAppService(
            IRepository<PrintTemplate, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<PrintTemplateDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<PrintTemplate, PrintTemplateDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<PrintTemplateDto>> GetAll(GetPrintTemplateInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToListAsync();

            var dto = ObjectMapper.Map<List<PrintTemplate>, List<PrintTemplateDto>>(items);
            return new PagedResultDto<PrintTemplateDto>(totalCount, dto);
        }

        public async Task<PrintTemplateDto> DataPost(CreateOrUpdatePrintTemplateDto input)
        {
            PrintTemplate result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdatePrintTemplateDto, PrintTemplate>(input));
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
            }
            return ObjectMapper.Map<PrintTemplate, PrintTemplateDto>(result);
        }

        public async Task Delete(List<Guid> ids)
        {
            foreach (var item in ids)
            {
                await _repository.DeleteAsync(item);
            }

        }

        public async Task<dynamic> CreatePdf(Guid id)
        {
            var temp = await _repository.GetAsync(id);
            var exporter = new PdfExporter();
            var pdfAtt = new PdfExporterAttribute();
            pdfAtt.Orientation = Orientation.Landscape;
            pdfAtt.PaperKind = PaperKind.A4;
            var result = await exporter.ExportBytesByTemplate(temp, pdfAtt, temp.Content);
            return new FileStreamResult(new MemoryStream(result), "application/octet-stream") { FileDownloadName = $"{temp.Name}.PDF" };
        }

        #endregion

    }
}