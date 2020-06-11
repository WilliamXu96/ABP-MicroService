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

            if (exist != null)
            {
                throw new BusinessException("名称：" + input.Name + "字典已存在");
            }

            var result = await _repository.InsertAsync(new Organization(
                                                            GuidGenerator.Create(),
                                                            input.CategoryId,
                                                            input.Pid,
                                                            input.Code,
                                                            input.Name,
                                                            "",     //TODO:自动生成fullName
                                                            input.AreaId,
                                                            input.Address,
                                                            input.Tel,
                                                            input.Remark));
            return ObjectMapper.Map<Organization, OrganizationDto>(result);
        }

        public async Task Delete(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await _repository.DeleteAsync(id);
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
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.Code.Contains(input.Filter) ||
                                                                        _.Name.Contains(input.Filter))
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
            org.Code = input.Code;
            org.Name = input.Name;
            org.AreaId = input.AreaId;
            org.Address = input.Address;
            org.Tel = input.Tel;
            org.Remark = input.Remark;

            return ObjectMapper.Map<Organization, OrganizationDto>(org);
        }
    }
}
