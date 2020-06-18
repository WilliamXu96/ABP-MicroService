using Business.BaseData.OrganizationManagement;
using Business.BaseData.OrganizationManagement.Dto;
using Business.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Business.BaseData
{
    [Area("business")]
    [Route("api/business/orgs")]
    public class OrganizationController : BusinessController, IOrganizationAppService
    {
        private readonly IOrganizationAppService _organizationAppService;

        public OrganizationController(IOrganizationAppService organizationAppService)
        {
            _organizationAppService = organizationAppService;
        }

        [HttpPost]
        public Task<OrganizationDto> Create(CreateOrUpdateOrganizationDto input)
        {
            return _organizationAppService.Create(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _organizationAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<OrganizationDto> Get(Guid id)
        {
            return _organizationAppService.Get(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<ListResultDto<OrganizationDto>> GetAll(GetOrganizationInputDto input)
        {
            return _organizationAppService.GetAll(input);
        }

        [HttpGet]
        [Route("list")]
        public Task<PagedResultDto<OrganizationDto>> GetAllList(GetOrganizationInputDto input)
        {
            return _organizationAppService.GetAllList(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<OrganizationDto> Update(Guid id, CreateOrUpdateOrganizationDto input)
        {
            return _organizationAppService.Update(id, input);
        }
    }
}
