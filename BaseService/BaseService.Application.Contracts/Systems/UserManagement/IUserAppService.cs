using BaseService.Systems.UserManagement.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace BaseService.Systems.UserManagement
{
    public interface IUserAppService : IApplicationService
    {
        Task<IdentityUserDto> Get(Guid id);

        Task<IdentityUserDto> Create(BaseIdentityUserCreateDto input);

        Task<IdentityUserDto> UpdateAsync(Guid id, BaseIdentityUserUpdateDto input);

        Task<PagedResultDto<IdentityUserDto>> GetAll(GetIdentityUsersInput input);
    }
}
