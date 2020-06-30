using Business.BaseData.JobManagement.Dto;
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

namespace Business.BaseData.JobManagement
{
    public class JobAppService : ApplicationService, IJobAppService
    {
        private readonly IRepository<Job, Guid> _repository;

        public JobAppService(IRepository<Job, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<JobDto> Create(CreateOrUpdateJobDto input)
        {
            var exist = await _repository.FirstOrDefaultAsync(_ => _.Name == input.Name);
            if (exist != null)
            {
                throw new BusinessException("名称：" + input.Name + "岗位已存在");
            }

            var result = await _repository.InsertAsync(new Job(GuidGenerator.Create(), input.Name, input.Enabled, input.Sort, input.Description));
            return ObjectMapper.Map<Job, JobDto>(result);
        }

        public async Task Delete(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await _repository.DeleteAsync(id);
            }
        }

        public async Task<JobDto> Get(Guid id)
        {
            var result = await _repository.GetAsync(id);
            return ObjectMapper.Map<Job, JobDto>(result);
        }

        public async Task<PagedResultDto<JobDto>> GetAll(GetJobInputDto input)
        {
            var query = _repository.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Sort")
                        .ToListAsync();

            var dots = ObjectMapper.Map<List<Job>, List<JobDto>>(items);
            return new PagedResultDto<JobDto>(totalCount, dots);
        }

        public async Task<JobDto> Update(Guid id, CreateOrUpdateJobDto input)
        {
            var job = await _repository.GetAsync(id);

            job.Name = input.Name;
            job.Enabled = input.Enabled;
            job.Sort = input.Sort;
            job.Description = input.Description;

            return ObjectMapper.Map<Job, JobDto>(job);
        }
    }
}
