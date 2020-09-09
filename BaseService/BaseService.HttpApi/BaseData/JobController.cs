using BaseService.BaseData.JobManagement;
using BaseService.BaseData.JobManagement.Dto;
using BaseService.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace BaseService.BaseData
{
    [Area("base")]
    [Route("api/base/job")]
    public class JobController : BaseServiceController, IJobAppService
    {
        private readonly IJobAppService _jobAppService;

        public JobController(IJobAppService jobAppService)
        {
            _jobAppService = jobAppService;
        }

        [HttpPost]
        public Task<JobDto> Create(CreateOrUpdateJobDto input)
        {
            return _jobAppService.Create(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _jobAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<JobDto> Get(Guid id)
        {
            return _jobAppService.Get(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<JobDto>> GetAll(GetJobInputDto input)
        {
            return _jobAppService.GetAll(input);
        }

        [HttpGet]
        [Route("jobs")]
        public Task<ListResultDto<JobDto>> GetAllJobs()
        {
            return _jobAppService.GetAllJobs();
        }

        [HttpPut]
        [Route("{id}")]
        public Task<JobDto> Update(Guid id, CreateOrUpdateJobDto input)
        {
            return _jobAppService.Update(id, input);
        }
    }
}
