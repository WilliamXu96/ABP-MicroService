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
