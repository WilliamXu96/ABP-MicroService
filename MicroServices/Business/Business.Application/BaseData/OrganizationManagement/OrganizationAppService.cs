using AutoMapper.Execution;
using Business.BaseData.OrganizationManagement.Dto;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Business.BaseData.OrganizationManagement
{
    public class OrganizationAppService : ApplicationService, IOrganizationAppService
    {
        private readonly IRepository<Organization, Guid> _repository;

        public OrganizationAppService(IRepository<Organization, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<OrganizationDto> Create(CreateOrUpdateOrganizationDto input)
        {
            var exist = await _repository.FirstOrDefaultAsync(_ => _.Name == input.Name);
            if (exist != null) throw new BusinessException("名称：" + input.Name + "机构已存在");

            var organization = new Organization(GuidGenerator.Create(),
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

        public async Task Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
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

        public async Task<ListResultDto<OrganizationDto>> LoadAllNodes(Guid id)
        {
            var organization = await _repository.GetAsync(id);
            var orgs = await _repository.Where(_ => _.Pid == null).ToListAsync();
            if (organization.Pid.HasValue)
            {
                for (int i = 1; i < organization.CascadeId.Split(".").Length; i++)
                {
                    var parent = await _repository.GetAsync(_ => _.Id == organization.Pid);
                    orgs.Add(parent);
                    if (orgs.Any(_ => _.Id == parent.Pid)) break;
                }
            }
            orgs.Add(organization);
            var dtos = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(orgs);
            return new ListResultDto<OrganizationDto>(dtos);
        }

        public async Task<ListResultDto<OrganizationDto>> GetAllWithParents(GetOrganizationInputDto input)
        {
            var result = await _repository.Where(_ => _.Pid == null).OrderBy(input.Sorting ?? "Name").ToListAsync();
            var self = await _repository.FirstOrDefaultAsync(_ => _.Id == input.Id);

            var dtos = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(result);
            foreach (var dto in dtos)
            {
                var any = await _repository.AnyAsync(_ => _.Pid == dto.Id);
                dto.HasChildren = any ? true : false;
                dto.Leaf = any ? false : true;
            }
            return new ListResultDto<OrganizationDto>(dtos);
        }

        public async Task<PagedResultDto<OrganizationDto>> GetAllList(GetOrganizationInputDto input)
        {
            var query = _repository
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.Name.Contains(input.Filter))
                .WhereIf(input.Pid.HasValue, _ => _.Pid == input.Pid)
                .WhereIf(input.CategoryId.HasValue, _ => _.CategoryId == input.CategoryId);

            var items = await query.OrderBy(input.Sorting ?? "Name")
                     .Skip(input.SkipCount)
                     .Take(input.MaxResultCount)
                     .ToListAsync();
            var totalCount = await query.CountAsync();

            var dtos = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(items);
            return new PagedResultDto<OrganizationDto>(totalCount, dtos);
        }

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
                org.CascadeId = parent.CascadeId + cascadeId + ".";
                org.FullName = parent.FullName + "/" + org.Name;
            }
            else
            {
                org.CascadeId = ".0." + cascadeId + ".";
                org.FullName = org.Name;
            }

        }
    }
}
