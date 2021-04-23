using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;
namespace Business.Models
{
    public class Book: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public bool IsDeleted { get; set; }
        
        /// <summary>
        /// 书名
        /// </summary>
        [Required]
        public string Name { get; set; }

        
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        
        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }

        
    }
}