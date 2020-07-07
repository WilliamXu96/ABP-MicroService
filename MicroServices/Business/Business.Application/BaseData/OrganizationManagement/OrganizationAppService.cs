using Business.BaseData.OrganizationManagement.Dto;
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
            var parent = await _repository.FirstOrDefaultAsync(_ => _.Id == input.Pid);

            if (exist != null)
            {
                throw new BusinessException("名称：" + input.Name + "机构已存在");
            }

            var result = await _repository.InsertAsync(new Organization(
                                                            GuidGenerator.Create(),
                                                            input.CategoryId,
                                                            input.Pid,
                                                            input.Name,
                                                            parent == null ? input.Name : parent.FullName + "/" + input.Name,
                                                            input.Sort,
                                                            input.Enabled
                                                            ));
            return ObjectMapper.Map<Organization, OrganizationDto>(result);
        }

        public async Task Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<OrganizationDto> Get(Guid id)
        {
            var result = await _repository.GetAsync(id);
            UpdateCascade(result);

            return ObjectMapper.Map<Organization, OrganizationDto>(result);
        }

        public async Task<ListResultDto<OrganizationDto>> GetAll(GetOrganizationInputDto input)
        {
            var query = _repository
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.Name.Contains(input.Filter))
                .Where(_ => _.Pid == input.Pid)
                .WhereIf(input.CategoryId.HasValue, _ => _.CategoryId == input.CategoryId);

            var items = await query.OrderBy(input.Sorting ?? "Sort")
                     .ToListAsync();

            var dtos = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(items);
            foreach (var dto in dtos)
            {
                var any = await _repository.AnyAsync(_ => _.Pid == dto.Id);
                dto.HasChildren = any ? true : false;
                dto.Leaf = any ? false : true;
            }
            return new ListResultDto<OrganizationDto>(dtos);
        }

        public async Task<ListResultDto<OrganizationDto>> GetAllWithParents(GetOrganizationInputDto input)
        {
            var result = await _repository.Where(_ => _.Pid == null).OrderBy(input.Sorting ?? "Name").ToListAsync();
            var self = await _repository.FirstOrDefaultAsync(_ => _.Id == input.Id);
            //result.Add(self);

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
            var org = await _repository.FirstOrDefaultAsync(_ => _.Id == id);

            org.Pid = input.Pid;
            //TODO：后台任务执行子集fullName修改
            org.Name = input.Name;
            org.Sort = input.Sort;
            org.Enabled = input.Enabled;

            return ObjectMapper.Map<Organization, OrganizationDto>(org);
        }

        private void UpdateCascade(Organization org)
        {
            var parent = _repository.Where(_ => _.Pid == org.Pid && _.Id != org.Id)
                                  .OrderByDescending(_ => _.CascadeId)
                                  .First();

            if (parent != null)
            {
                return;
            }

        }
    }
}
