using Business.BaseData.JobManagement;
using Business.BaseData.JobManagement.Dto;
using Business.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Business.BaseData
{
    [Area("business")]
    [Route("api/business/job")]
    public class JobController : BusinessController, IJobAppService
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

        [HttpPut]
        [Route("{id}")]
        public Task<JobDto> Update(Guid id, CreateOrUpdateJobDto input)
        {
            return _jobAppService.Update(id, input);
        }
    }
}
