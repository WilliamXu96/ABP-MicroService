using BaseService.BaseData.OrganizationManagement.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.BaseData.OrganizationManagement
{
    public interface IOrganizationAppService : IApplicationService
    {
        Task<PagedResultDto<OrganizationDto>> GetAll(GetOrganizationInputDto input);

        Task<ListResultDto<OrganizationDto>> LoadAll(Guid? id, string filter);

        Task<ListResultDto<OrganizationDto>> LoadAllNodes();

        Task<OrganizationDto> Get(Guid id);

        Task<OrganizationDto> Create(CreateOrUpdateOrganizationDto input);

        Task<OrganizationDto> Update(Guid id, CreateOrUpdateOrganizationDto input);

        Task Delete(List<Guid> ids);
    }
}
