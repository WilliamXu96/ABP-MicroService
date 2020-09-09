using BaseService.BaseData.DataDictionaryManagement;
using BaseService.BaseData.DataDictionaryManagement.Dto;
using BaseService.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BaseService.BaseData
{
    [Area("base")]
    [Route("api/base/dictDetails")]
    public class DataDictionaryDetailController : BaseServiceController, IDictionaryDetailAppService
    {
        private readonly IDictionaryDetailAppService _dictionaryDetailAppService;

        public DataDictionaryDetailController(IDictionaryDetailAppService dictionaryDetailAppService)
        {
            _dictionaryDetailAppService = dictionaryDetailAppService;
        }

        [HttpPost]
        public Task<DictionaryDetailDto> Create(CreateOrUpdateDictionaryDetailDto input)
        {
            return _dictionaryDetailAppService.Create(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
           return _dictionaryDetailAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<DictionaryDetailDto> Get(Guid id)
        {
            return _dictionaryDetailAppService.Get(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<DictionaryDetailDto>> GetAll(GetDictionaryDetailInputDto input)
        {
            return _dictionaryDetailAppService.GetAll(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<DictionaryDetailDto> Update(Guid id, CreateOrUpdateDictionaryDetailDto input)
        {
            return _dictionaryDetailAppService.Update(id, input);
        }
    }
}
