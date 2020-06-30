using Business.BaseData.JobManagement.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Business.BaseData.JobManagement
{
    public interface IJobAppService : IApplicationService
    {
        Task<PagedResultDto<JobDto>> GetAll(GetJobInputDto input);

        Task<JobDto> Get(Guid id);

        Task<JobDto> Create(CreateOrUpdateJobDto input);

        Task<JobDto> Update(Guid id, CreateOrUpdateJobDto input);

        Task Delete(List<Guid> ids);
    }
}
