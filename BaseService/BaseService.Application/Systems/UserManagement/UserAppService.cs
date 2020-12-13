using BaseService.BaseData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;

namespace BaseService.Systems.UserManagement
{
    [Authorize(IdentityPermissions.Users.Default)]
    public class UserAppService : ApplicationService, IUserAppService
    {
        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }
        public IIdentityRoleRepository RoleRepository { get; }
        private readonly IRepository<Organization, Guid> _orgRepository;
        private readonly IRepository<UserJobs> _userJobsRepository;

        public UserAppService(
            IdentityUserManager userManager,
            IIdentityUserRepository userRepository,
            IIdentityRoleRepository roleRepository,
            IRepository<Organization, Guid> orgRepository,
            IRepository<UserJobs> empJobsRepository)
        {
            UserManager = userManager;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            _orgRepository = orgRepository;
            _userJobsRepository = empJobsRepository;
        }

        public async Task<IdentityUserDto> Get(Guid id)
        {
            var dto = ObjectMapper.Map<IdentityUser, IdentityUserDto>(await UserManager.GetByIdAsync(id));
            var empJobs = await _userJobsRepository.Where(_ => _.UserId == id).Select(_ => _.JobId).ToListAsync();
            dto.Jobs = empJobs;

            return dto;
        }

        [Authorize(IdentityPermissions.Users.Create)]
        public async Task<IdentityUserDto> Create(IdentityUserCreateDto input)
        {
            var user = new IdentityUser(
                GuidGenerator.Create(),
                input.OrgId,
                input.UserName,
                input.Email,
                CurrentTenant.Id
            );

            input.MapExtraPropertiesTo(user);

            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
            await UpdateUserByInput(user, input);

            var dto = ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);

            foreach (var jid in input.Jobs)
            {
                await _userJobsRepository.InsertAsync(new UserJobs(CurrentTenant.Id, user.Id, jid));
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return dto;
        }

        [Authorize(IdentityPermissions.Users.Update)]
        public async Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserUpdateDto input)
        {
            var user = await UserManager.GetByIdAsync(id);
            user.ConcurrencyStamp = input.ConcurrencyStamp;

            (await UserManager.SetUserNameAsync(user, input.UserName)).CheckErrors();

            await UpdateUserByInput(user, input);
            input.MapExtraPropertiesTo(user);

            (await UserManager.UpdateAsync(user)).CheckErrors();

            if (!input.Password.IsNullOrEmpty())
            {
                (await UserManager.RemovePasswordAsync(user)).CheckErrors();
                (await UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
            }

            var dto = ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);

            await _userJobsRepository.DeleteAsync(_ => _.UserId == id);
            foreach (var jid in input.Jobs)
            {
                await _userJobsRepository.InsertAsync(new UserJobs(CurrentTenant.Id, id, jid));
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return dto;
        }

        public async Task<PagedResultDto<IdentityUserDto>> GetAll(GetIdentityUsersInput input)
        {
            List<Guid> orgIds = null;
            if (input.OrgId.HasValue)
            {
                var org = await _orgRepository.GetAsync(input.OrgId.Value);
                orgIds = await _orgRepository.Where(_ => _.CascadeId.Contains(org.CascadeId)).Select(_ => _.Id).ToListAsync();
            }

            var totalCount = await UserRepository.GetCountAsync(input.Filter);
            var items = await UserRepository.GetListAsync(orgIds, input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);
            var orgs = await _orgRepository.Where(_ => items.Select(i => i.OrgId).Contains(_.Id)).ToListAsync();

            var dtos = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(items);

            foreach (var dto in dtos)
            {
                dto.OrgIdToName = orgs.FirstOrDefault(_ => _.Id == dto.OrgId)?.Name;
            }

            return new PagedResultDto<IdentityUserDto>(totalCount, dtos);
        }

        protected virtual async Task UpdateUserByInput(IdentityUser user, IdentityUserCreateOrUpdateDtoBase input)
        {
            if (!string.Equals(user.Email, input.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetEmailAsync(user, input.Email)).CheckErrors();
            }

            if (!string.Equals(user.PhoneNumber, input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
            }

            (await UserManager.SetLockoutEnabledAsync(user, input.LockoutEnabled)).CheckErrors();

            user.Name = input.Name;
            user.Surname = input.Surname;

            if (input.RoleNames != null)
            {
                (await UserManager.SetRolesAsync(user, input.RoleNames)).CheckErrors();
            }
        }
    }
}
