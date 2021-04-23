using Business.BookManagement;
using Business.BookManagement.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Business.Controllers
{
    [RemoteService]
    [Area("Business")]
    [Route("api/app/Book")]
    public class BookController : AbpController
    {
        private readonly IBookAppService _BookAppService;

        public BookController(IBookAppService BookAppService)
        {
            _BookAppService = BookAppService;
        }

        [HttpPost]
        public Task<BookDto> CreateOrUpdate(CreateOrUpdateBookDto input)
        {
            return _BookAppService.CreateOrUpdate(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task Delete(List<Guid> ids)
        {
            return _BookAppService.Delete(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<BookDto> Get(Guid id)
        {
            return _BookAppService.Get(id);
        }

        [HttpGet]
        public Task<PagedResultDto<BookDto>> GetAll(GetBookInputDto input)
        {
            return _BookAppService.GetAll(input);
        }
    }
}
