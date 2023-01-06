using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace BaseService.Systems
{
    public class Menu : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid? FormId { get; set; }

        public Guid? Pid { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Label { get; set; }

        public int Sort { get; set; }

        public string Path { get; set; }

        public string Component { get; set; }

        public string Permission { get; set; }

        public string Icon { get; set; }

        public bool Hidden { get; set; }

        public bool AlwaysShow { get; set; }

        /// <summary>
        /// 是否宿主菜单，if(TenantId!=null&&IsHost==true)：停用租户菜单
        /// </summary>
        public bool IsHost { get; set; }

        public bool IsDeleted { get; set; }

        public Menu(Guid id) : base(id)
        {
            
        }
    }
}
