using BaseService.BaseData.OrganizationManagement.Dto;
using BaseService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BaseService.BaseData.OrganizationManagement
{
    [Authorize(BaseServicePermissions.Organization.Default)]
    public class OrganizationAppService : ApplicationService, IOrganizationAppService
    {
        private readonly IRepository<Organization, Guid> _repository;

        public OrganizationAppService(IRepository<Organization, Guid> repository)
        {
            _repository = repository;
        }

        [Authorize(BaseServicePermissions.Organization.Create)]
        public async Task<OrganizationDto> Create(CreateOrUpdateOrganizationDto input)
        {
            var exist = await _repository.FirstOrDefaultAsync(_ => _.Name == input.Name);
            if (exist != null) throw new BusinessException("名称：" + input.Name + "机构已存在");

            var organization = new Organization(GuidGenerator.Create(),
                                                CurrentTenant.Id,
                                                input.CategoryId,
                                                input.Pid,
                                                input.Name,
                                                "",
                                                input.Sort,
                                                true,
                                                input.Enabled
                                                );
            var parent = await _repository.FirstOrDefaultAsync(_ => _.Id == input.Pid);
            ChangeOrganizationModel(organization, parent);
            await _repository.InsertAsync(organization);

            return ObjectMapper.Map<Organization, OrganizationDto>(organization);
        }

        [Authorize(BaseServicePermissions.Organization.Delete)]
        public async Task Delete(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                var org = await _repository.GetAsync(id);
                await _repository.DeleteAsync(_ => _.CascadeId.Contains(org.CascadeId));
            }
        }

        public async Task<OrganizationDto> Get(Guid id)
        {
            var result = await _repository.GetAsync(id);

            return ObjectMapper.Map<Organization, OrganizationDto>(result);
        }

        public async Task<PagedResultDto<OrganizationDto>> GetAll(GetOrganizationInputDto input)
        {
            var query = _repository
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.Name.Contains(input.Filter))
                .WhereIf(input.CategoryId.HasValue, _ => _.CategoryId == input.CategoryId);
            if (input.Id.HasValue)
            {
                var org = await _repository.GetAsync(input.Id.Value);
                query = query.Where(_ => _.CascadeId.Contains(org.CascadeId));
            }

            var items = await query.OrderBy(input.Sorting ?? "Sort")
                     .Skip(input.SkipCount)
                     .Take(input.MaxResultCount)
                     .ToListAsync();
            var totalCount = await query.CountAsync();

            var dtos = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(items);
            return new PagedResultDto<OrganizationDto>(totalCount, dtos);
        }

        public async Task<ListResultDto<OrganizationDto>> LoadAll(Guid? id, string filter)
        {
            var items = new List<Organization>();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                items = await _repository.Where(_ => _.Name.Contains(filter)).ToListAsync();
            }
            else
            {
                var query = id.HasValue ? _repository.Where(_ => _.Pid == id) :
                                         _repository.Where(_ => _.Pid == null);
                items = await query.ToListAsync();
            }

            var dtos = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(items);
            return new ListResultDto<OrganizationDto>(dtos);
        }

        public async Task<ListResultDto<OrganizationDto>> LoadAllNodes()
        {
            var items = await _repository.GetListAsync();

            var dtos = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(items);
            return new ListResultDto<OrganizationDto>(dtos);
        }

        [Authorize(BaseServicePermissions.Organization.Update)]
        public async Task<OrganizationDto> Update(Guid id, CreateOrUpdateOrganizationDto input)
        {
            if (input.Pid == id) throw new BusinessException("机构上级不能为当前机构！");
            var organization = await _repository.FirstOrDefaultAsync(_ => _.Id == id);

            if (organization.Pid != input.Pid)
            {
                var parent = await _repository.FirstOrDefaultAsync(_ => _.Id == input.Pid);
                var orgs = await _repository.Where(_ => _.CascadeId.Contains(organization.CascadeId) && _.Id != organization.Id)
                                      .OrderBy(_ => _.CascadeId).ToListAsync();
                organization.Pid = input.Pid;
                ChangeOrganizationModel(organization, parent);
                foreach (var org in orgs)
                {
                    if (org.Pid == organization.Id)
                    {
                        ChangeOrganizationModel(org, organization, false);
                    }
                    else
                    {
                        var orgParent = orgs.FirstOrDefault(_ => _.Id == org.Pid);
                        ChangeOrganizationModel(org, orgParent, false);
                    }
                }
            }

            organization.Name = input.Name;
            organization.Sort = input.Sort;
            organization.Enabled = input.Enabled;

            return ObjectMapper.Map<Organization, OrganizationDto>(organization);
        }

        private void ChangeOrganizationModel(Organization org, Organization parent, bool checkLevel = true)
        {
            var cascadeId = org.CascadeId == null ? 1 : int.Parse(org.CascadeId.TrimEnd('.').Split('.').Last());
            if (checkLevel)
            {
                if (parent != null && parent.Leaf) parent.Leaf = false;
                var lastLevel = _repository.Where(_ => _.Pid == org.Pid && _.Id != org.Id)
                      .OrderByDescending(_ => _.CascadeId)
                      .FirstOrDefault();
                cascadeId = lastLevel == null ? 1 : int.Parse(lastLevel.CascadeId.TrimEnd('.').Split('.').Last()) + 1;
            }

            if (org.Pid.HasValue)
            {
                if (parent == null) throw new BusinessException("上级机构查询错误！");
                //TODO：限制层级数
                org.CascadeId = parent.CascadeId + cascadeId.ToString("000") + ".";
                org.FullName = parent.FullName + "/" + org.Name;
            }
            else
            {
                //TODO：限制层级数
                org.CascadeId = ".0." + cascadeId.ToString("000") + ".";
                org.FullName = org.Name;
            }

        }
    }
}
