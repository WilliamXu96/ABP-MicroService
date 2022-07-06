using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Business.BookManagement.Dto;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Business.Permissions;
using XCZ;

namespace Business.BookManagement
{
    [Authorize(BusinessPermissions.Book.Default)]
    public class BookAppService : BusinessWorkFlowAppService, IBookAppService
    {
        private const string FormName = "Book";
        private IRepository<Book, Guid> _repository;

        public BookAppService(
            IRepository<Book, Guid> repository
            )
        {
            _repository = repository;
        }
        #region 增删改查基础方法

        public async Task<BookDto> Get(Guid id)
        {
            var data = await _repository.GetAsync(id);
            var dto = ObjectMapper.Map<Book, BookDto>(data);
            return dto;
        }

        public async Task<PagedResultDto<BookDto>> GetAll(GetBookInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Id")
                        .ToListAsync();

            var dtos = ObjectMapper.Map<List<Book>, List<BookDto>>(items);
            return new PagedResultDto<BookDto>(totalCount, dtos);
        }

        public async Task<BookDto> CreateOrUpdate(CreateOrUpdateBookDto input)
        {
            Book result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _repository.InsertAsync(ObjectMapper.Map<CreateOrUpdateBookDto, Book>(input));

                //创建工作流
                await WorkFlow.CreateWorkFlow(FormName, result);
            }
            else
            {
                var data = await _repository.GetAsync(input.Id.Value);
                result = await _repository.UpdateAsync(ObjectMapper.Map(input, data));
                //检查流程状态
                await WorkFlow.CheckFormStatus(input.Id.Value, result);
            }
            return ObjectMapper.Map<Book, BookDto>(result);
        }

        public async Task Delete(List<Guid> ids)
        {
            foreach (var item in ids)
            {
                await _repository.DeleteAsync(item);
            }

        }
        #endregion

    }
}