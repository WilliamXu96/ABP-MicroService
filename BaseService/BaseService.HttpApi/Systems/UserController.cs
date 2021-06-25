using BaseService.Controllers;
using BaseService.Systems.UserManagement;
using BaseService.Systems.UserManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace BaseService.HttpApi.Systems
{
    [Area("base")]
    [Route("api/base/user")]
    public class UserController: BaseServiceController,IUserAppService
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        public Task<IdentityUserDto> Create(BaseIdentityUserCreateDto input)
        {
            return _userAppService.Create(input);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<IdentityUserDto> Get(Guid id)
        {
            return _userAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<IdentityUserDto>> GetAll(GetIdentityUsersInput input)
        {
            return _userAppService.GetAll(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<IdentityUserDto> UpdateAsync(Guid id, BaseIdentityUserUpdateDto input)
        {
            return _userAppService.UpdateAsync(id, input);
        }
    }
}
