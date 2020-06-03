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
    [Route("api/business/dict")]
    public class DataDictionaryController : BusinessController, IDictionaryAppService
    {
        private readonly IDictionaryAppService _dictionaryAppService;

        public DataDictionaryController(IDictionaryAppService dictionaryAppService)
        {
            _dictionaryAppService = dictionaryAppService;
        }

        [HttpPost]
        public async Task<DictionaryDto> Create(CreateOrUpdateDictionaryDto input)
        {
            return await _dictionaryAppService.Create(input);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(List<Guid> ids)
        {
            await _dictionaryAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<DictionaryDto> Get(Guid id)
        {
            return await _dictionaryAppService.Get(id);
        }

        [HttpGet]
        [Route("all")]
        public async Task<PagedResultDto<DictionaryDto>> GetAll(GetDictionaryInputDto input)
        {
            return await _dictionaryAppService.GetAll(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<DictionaryDto> Update(Guid id, CreateOrUpdateDictionaryDto input)
        {
            return await _dictionaryAppService.Update(id, input);
        }
    }
}
