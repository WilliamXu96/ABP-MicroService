using Business.BaseData.OrganizationManagement;
using Business.BaseData.OrganizationManagement.Dto;
using Business.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
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

        [HttpDelete]
        [Route("{id}")]
        public Task Delete(Guid id)
        {
            return _organizationAppService.Delete(id);
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
