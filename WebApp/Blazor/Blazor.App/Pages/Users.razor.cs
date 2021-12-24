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

        private List<SelectedItem> RoleItems { get; set; } = new List<SelectedItem>();

        private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

        [Inject]
        [NotNull]
        public MessageService? MessageService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var result = await Http.GetFromJsonAsync<PagedResultDto<UserDto>>("/api/base/user");
            var roles = await GetRoleListAsync();
            Items = result.Items;
            foreach (var role in roles)
            {
                RoleItems.Add(new SelectedItem
                {
                    Text = role.Name,
                    Value = role.Name
                });
            }
        }

        private static Task<UserDto> OnAddAsync() => Task.FromResult(new UserDto());

        private async Task<bool> OnDeleteAsync(IEnumerable<UserDto> items)
        {
            if (items.Count() > 1)
            {
                await MessageService.Show(new MessageOption()
                {
                    Content = "暂不支持多选删除",
                    Icon = "fa fa-info-circle",
                    Color = Color.Warning
                });

                return false;
            }
            var result = await Http.DeleteAsync("/api/identity/users/" + items.First().Id);
            return true;
        }

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
                //if (oldItem != null)
                //{
                //    oldItem.Name = item.Name;
                //    oldItem.Address = item.Address;
                //    oldItem.DateTime = item.DateTime;
                //    oldItem.Count = item.Count;
                //    oldItem.Complete = item.Complete;
                //    oldItem.Education = item.Education;
                //}
                //var oldItem = Items.FirstOrDefault(i => i.Id == item.Id);
                var jobList = await GetJobListAsync();
                var OrgList = await GetOrgListAsync();
                var user = await Http.GetFromJsonAsync<UserDto>("/api/base/user/" + item.Id);
                var userRoles = await GetUserRoles(item.Id);
                //item.RoleNames = string.Join(",", userRoles.Select(_ => _.Name).ToList());
            }
            return false;

        }

        private async Task<QueryData<UserDto>> OnQueryAsync(QueryPageOptions options)
        {
            var result = await Http.GetFromJsonAsync<PagedResultDto<UserDto>>("/api/base/user");
            var items = result.Items;
            // 设置记录总数
            var total = result.TotalCount;
            return new QueryData<UserDto>()
            {
                Items = items,
                TotalCount = total,
                IsSorted = true,
                IsFiltered = true,
                IsSearch = true
            };
        }

        private async Task<List<RoleItem>> GetRoleListAsync()
        {
            var result = await Http.GetFromJsonAsync<ListResultDto<RoleItem>>("/api/identity/roles/all");
            return new List<RoleItem>(result.Items);
        }

        private async Task<List<JobItem>> GetJobListAsync()
        {
            var result = await Http.GetFromJsonAsync<ListResultDto<JobItem>>("/api/base/job/jobs");
            return new List<JobItem>(result.Items);
        }

        private async Task<List<OrgItem>> GetOrgListAsync()
        {
            var result = await Http.GetFromJsonAsync<ListResultDto<OrgItem>>("/api/base/orgs/loadNodes");
            return new List<OrgItem>(result.Items);
        }
        private async Task<List<RoleItem>> GetUserRoles(Guid id)
        {
            var result = await Http.GetFromJsonAsync<ListResultDto<RoleItem>>("/api/identity/users/" + id + "/roles");
            return new List<RoleItem>(result.Items);
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

        [Display(Name = "角色")]
        [AutoGenerateColumn(Ignore = true)]
        public string RoleNames { get; set; }

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

    public class RoleItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public bool IsStatic { get; set; }

        public bool IsPublic { get; set; }
    }

    public class JobItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Enabled { get; set; }

        public int Sort { get; set; }

        public string Description { get; set; }
    }

    public class OrgItem
    {
        public Guid Id { get; set; }

        public int CategoryId { get; set; }

        public Guid? Pid { get; set; }

        public string Name { get; set; }


        public string FullName { get; set; }

        public int Sort { get; set; }

        public bool Enabled { get; set; }

        public bool HasChildren { get; set; }

        public bool Leaf { get; set; }

        public string Label { get; set; }
    }
}
