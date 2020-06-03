using Business.BaseData.DataDictionaryManagement;
using Business.BaseData.DataDictionaryManagement.Dto;
using Business.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Business.BaseData
{
    [Area("business")]
    [Route("api/business/dictDetails")]
    public class DataDictionaryDetailController : BusinessController, IDictionaryDetailAppService
    {
        private readonly IDictionaryDetailAppService _dictionaryDetailAppService;

        public DataDictionaryDetailController(IDictionaryDetailAppService dictionaryDetailAppService)
        {
            _dictionaryDetailAppService = dictionaryDetailAppService;
        }

        [HttpPost]
        public async Task<DictionaryDetailDto> Create(CreateOrUpdateDictionaryDetailDto input)
        {
            return await _dictionaryDetailAppService.Create(input);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(List<Guid> ids)
        {
            await _dictionaryDetailAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<DictionaryDetailDto> Get(Guid id)
        {
            return await _dictionaryDetailAppService.Get(id);
        }

        [HttpGet]
        [Route("all")]
        public async Task<PagedResultDto<DictionaryDetailDto>> GetAll(GetDictionaryDetailInputDto input)
        {
            return await _dictionaryDetailAppService.GetAll(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<DictionaryDetailDto> Update(Guid id, CreateOrUpdateDictionaryDetailDto input)
        {
            return await _dictionaryDetailAppService.Update(id, input);
        }
    }
}
