using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Business.BookManagement.Dto;

namespace Business.BookManagement
{
    public interface IBookAppService : IApplicationService
    {
        Task<BookDto> Get(Guid id);
        Task<PagedResultDto<BookDto>> GetAll(GetBookInputDto input);
        Task<BookDto> CreateOrUpdate(CreateOrUpdateBookDto input);
        Task Delete(List<Guid> ids);
    }
}
