using Business.BaseData.OrganizationManagement.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Business.BaseData.OrganizationManagement
{
    public interface IOrganizationAppService
    {
        Task<PagedResultDto<OrganizationDto>> GetAll(GetOrganizationInputDto input);

        Task<OrganizationDto> Get(Guid id);

        Task<OrganizationDto> Create(CreateOrUpdateOrganizationDto input);

        Task<OrganizationDto> Update(Guid id, CreateOrUpdateOrganizationDto input);

        Task Delete(List<Guid> ids);
    }
}
