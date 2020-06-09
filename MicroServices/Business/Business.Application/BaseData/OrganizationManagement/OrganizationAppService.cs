using Business.BaseData.OrganizationManagement.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Business.BaseData.OrganizationManagement
{
    public class OrganizationAppService : ApplicationService, IOrganizationAppService
    {

        public Task<OrganizationDto> Create(CreateOrUpdateOrganizationDto input)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<OrganizationDto>> GetAll(GetOrganizationInputDto input)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationDto> Update(Guid id, CreateOrUpdateOrganizationDto input)
        {
            throw new NotImplementedException();
        }
    }
}
