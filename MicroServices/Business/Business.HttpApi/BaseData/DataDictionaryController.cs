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
        public Task<DictionaryDto> Create(CreateOrUpdateDictionaryDto input)
        {
            return _dictionaryAppService.Create(input);
        }

        [HttpPost]
        [Route("Delete")]
        public Task Delete(List<Guid> ids)
        {
            return _dictionaryAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<DictionaryDto> Get(Guid id)
        {
            return _dictionaryAppService.Get(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<DictionaryDto>> GetAll(GetDictionaryInputDto input)
        {
            return _dictionaryAppService.GetAll(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<DictionaryDto> Update(Guid id, CreateOrUpdateDictionaryDto input)
        {
            return _dictionaryAppService.Update(id, input);
        }
    }
}
