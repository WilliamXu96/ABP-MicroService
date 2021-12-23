using Blazor.App.Common;
using Blazor.App.Dtos;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

        private async Task<bool> OnSaveAsync(UserDto item, ItemChangedType changedType)
        {
            if (changedType == ItemChangedType.Add)
            {
                var response = await Http.PostAsJsonAsync("/api/base/user", item);
                //var result = await response.Content.ReadFromJsonAsync<UserDto>();
                //System.Console.WriteLine(JsonSerializer.Serialize(result));
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
            return false;

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

        private async Task<bool> OnDeleteAsync(IEnumerable<UserDto> items)
        {
            var result = await Http.DeleteAsync("/api/identity/users/" + items.First().Id);
            return true;
        }
    }

    public class UserDto
    {
        [Display(Name = "Id")]
        [AutoGenerateColumn(Ignore = true)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "用户名")]
        [AutoGenerateColumn(Ignore = true)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0}姓名")]
        [Display(Name = "姓名")]
        [AutoGenerateColumn(Ignore = true)]
        public string Name { get; set; }

        //public string Surname { get; set; }

        [Display(Name = "邮箱")]
        [AutoGenerateColumn(Ignore = true)]
        public string Email { get; set; }

        [Display(Name = "允许锁定")]
        [AutoGenerateColumn(Ignore = true)]
        public bool LockoutEnabled { get; set; }

        public List<string> RoleNames { get; set; }

        [Display(Name = "密码")]
        [AutoGenerateColumn(Ignore = true)]
        public string Password { get; set; }

        public List<Guid> OrganizationIds { get; set; } = new List<Guid>();

        public List<Guid> JobIds { get; set; } = new List<Guid>();

        [Display(Name = "电话")]
        [AutoGenerateColumn(Ignore = true)]
        public string PhoneNumber { get; set; }

        [Display(Name = "创建时间")]
        [AutoGenerateColumn(Ignore = true)]
        public DateTime CreationTime { get; set; }
    }
}
