using Blazor.App.Dtos;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blazor.App.Pages
{
    public partial class Users : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; }

        [NotNull]
        private List<UserDto>? Items { get; set; }

        private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var result = await Http.GetFromJsonAsync<PagedResultDto<UserDto>>("/api/base/user");
            Items = result.Items;
            //Items = new List<UserDto>()
            //{
            //    new UserDto
            //    {
            //        UserName="Aa",
            //        Name="a",
            //        Email="aa@aa.ca",
            //        PhoneNumber="123",
            //        CreationTime=DateTime.Now
            //    }
            //};
            //var content = JsonContent.Create(new BookDto());
            //var a = await Http.PostAsync("", content);
        }

        private static Task<UserDto> OnAddAsync() => Task.FromResult(new UserDto());

        private Task<bool> OnSaveAsync(UserDto item, ItemChangedType changedType)
        {
            // 增加数据演示代码
            if (changedType == ItemChangedType.Add)
            {
                //System.Console.WriteLine(JsonSerializer.Serialize(item));
                Items.Add(item);
            }
            else
            {
                //var oldItem = Items.FirstOrDefault(i => i.Id == item.Id);
                //if (oldItem != null)
                //{
                //    oldItem.Name = item.Name;
                //    oldItem.Address = item.Address;
                //    oldItem.DateTime = item.DateTime;
                //    oldItem.Count = item.Count;
                //    oldItem.Complete = item.Complete;
                //    oldItem.Education = item.Education;
                //}
            }
            return Task.FromResult(true);
        }

        private async Task<QueryData<UserDto>> OnQueryAsync(QueryPageOptions options)
        {
            var result = await Http.GetFromJsonAsync<PagedResultDto<UserDto>>("/api/base/user");
            var items = result.Items;

            // 设置记录总数
            var total = result.TotalCount;

            // 内存分页
            //items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

            return new QueryData<UserDto>()
            {
                Items = items,
                TotalCount = total,
                IsSorted = true,
                IsFiltered = true,
                IsSearch = true
            };
        }
    }
    public class UserDto
    {
        [Required(ErrorMessage ="{0}不能为空")]
        [Display(Name = "用户名")]
        [AutoGenerateColumn(Ignore = true)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0}姓名")]
        [Display(Name = "姓名")]
        [AutoGenerateColumn(Ignore = true)]
        public string Name { get; set; }

        [Display(Name = "邮箱")]
        [AutoGenerateColumn(Ignore = true)]
        public string Email { get; set; }

        [Display(Name = "电话")]
        [AutoGenerateColumn(Ignore = true)]
        public string PhoneNumber { get; set; }

        [Display(Name = "创建时间")]
        [AutoGenerateColumn(Ignore = true)]
        public DateTime CreationTime { get; set; }
    }
}
