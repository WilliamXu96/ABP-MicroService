using Blazor.App.Dtos;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        private List<UserDto>? Items;

        private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var result = await Http.GetFromJsonAsync<PagedResultDto<UserDto>>("/api/base/user");
            Items = result.Items;
            //var content = JsonContent.Create(new BookDto());
            //var a = await Http.PostAsync("", content);
        }
    }
    public class UserDto
    {
        [Display(Name = "用户名")]
        [AutoGenerateColumn(Ignore = true)]
        public string UserName { get; set; }

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
