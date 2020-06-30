using Business.BaseData.EmployeeManagement.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Business.BaseData.EmployeeManagement
{
    public interface IEmployeeAppService : IApplicationService
    {
        Task<PagedResultDto<EmployeeDto>> GetAll(GetEmployeeInputDto input);

        Task<EmployeeDto> Get(Guid id);

        Task<EmployeeDto> Create(CreateOrUpdateEmployeeDto input);

        Task<EmployeeDto> Update(Guid id, CreateOrUpdateEmployeeDto input);

        Task Delete(List<Guid> ids);
    }
}
