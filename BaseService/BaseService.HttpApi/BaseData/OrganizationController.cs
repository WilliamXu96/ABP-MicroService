using BaseService.BaseData.OrganizationManagement;
using BaseService.BaseData.OrganizationManagement.Dto;
using BaseService.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BaseService.BaseData
{
    [Area("base")]
    [Route("api/base/orgs")]
    public class OrganizationController : BaseServiceController, IOrganizationAppService
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
        public Task<PagedResultDto<OrganizationDto>> GetAll(GetOrganizationInputDto input)
        {
            return _organizationAppService.GetAll(input);
        }

        [HttpGet]
        [Route("loadOrgs")]
        public Task<ListResultDto<OrganizationDto>> LoadAll(Guid? id, string filter)
        {
            return _organizationAppService.LoadAll(id, filter);
        }

        [HttpGet]
        [Route("loadNodes")]
        public Task<ListResultDto<OrganizationDto>> LoadAllNodes()
        {
            return _organizationAppService.LoadAllNodes();
        }

        [HttpPut]
        [Route("{id}")]
        public Task<OrganizationDto> Update(Guid id, CreateOrUpdateOrganizationDto input)
        {
            return _organizationAppService.Update(id, input);
        }
    }
}
