using Business.BaseData.EmployeeManagement;
using Business.BaseData.EmployeeManagement.Dto;
using Business.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Business.BaseData
{
    [Area("business")]
    [Route("api/business/employee")]
    public class EmployeeController : BusinessController, IEmployeeAppService
    {
        private readonly IEmployeeAppService _employeeAppService;

        public EmployeeController(IEmployeeAppService employeeAppService)
        {
            _employeeAppService = employeeAppService;
        }

        [HttpPost]
        public Task<EmployeeDto> Create(CreateOrUpdateEmployeeDto input)
        {
            return _employeeAppService.Create(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _employeeAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<EmployeeDto> Get(Guid id)
        {
            return _employeeAppService.Get(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<EmployeeDto>> GetAll(GetEmployeeInputDto input)
        {
            return _employeeAppService.GetAll(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<EmployeeDto> Update(Guid id, CreateOrUpdateEmployeeDto input)
        {
            return _employeeAppService.Update(id, input);
        }
    }
}
