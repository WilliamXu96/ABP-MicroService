using Business.BaseData.EmployeeManagement.Dto;
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

namespace Business.BaseData.EmployeeManagement
{
    public class EmployeeAppService : ApplicationService, IEmployeeAppService
    {
        private readonly IRepository<Employee, Guid> _repository;
        private readonly IRepository<Organization, Guid> _orgRepository;

        public EmployeeAppService(
            IRepository<Employee, Guid> repository,
            IRepository<Organization, Guid> orgRepository)
        {
            _repository = repository;
            _orgRepository = orgRepository;
        }

        public async Task<EmployeeDto> Create(CreateOrUpdateEmployeeDto input)
        {
            var exist = await _repository.FirstOrDefaultAsync(_ => _.Name == input.Name);

            if (exist != null)
            {
                throw new BusinessException("名称：" + input.Name + "职员已存在");
            }

            var result = await _repository.InsertAsync(new Employee(GuidGenerator.Create(), input.Name, input.Gender, input.Phone, input.Email, input.Enabled, input.OrgId, input.UserId));
            return ObjectMapper.Map<Employee, EmployeeDto>(result);
        }

        public async Task Delete(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await _repository.DeleteAsync(id);
            }
        }

        public async Task<EmployeeDto> Get(Guid id)
        {
            var result = await _repository.GetAsync(id);
            return ObjectMapper.Map<Employee, EmployeeDto>(result);
        }

        public async Task<PagedResultDto<EmployeeDto>> GetAll(GetEmployeeInputDto input)
        {
            var query = _repository.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Name")
                        .ToListAsync();

            var orgs = await _orgRepository.Where(_ => items.Select(i => i.OrgId).Contains(_.Id)).ToListAsync();

            var dots = ObjectMapper.Map<List<Employee>, List<EmployeeDto>>(items);
            foreach (var dto in dots)
            {
                dto.OrgIdToName = orgs.FirstOrDefault(_ => _.Id == dto.OrgId)?.Name;
            }
            return new PagedResultDto<EmployeeDto>(totalCount, dots);
        }

        public async Task<EmployeeDto> Update(Guid id, CreateOrUpdateEmployeeDto input)
        {
            var employee = await _repository.GetAsync(id);

            employee.Name = input.Name;
            employee.Enabled = input.Enabled;
            employee.Email = input.Email;
            employee.Gender = input.Gender;
            employee.OrgId = input.OrgId;
            employee.UserId = input.UserId;

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }
    }
}
