using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    /// <summary>
    /// 打印模板
    /// </summary>
    public class PrintTemplate: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模板类型：0设计模板，1指令模板
        /// </summary>
        public int TempType { get; set; }
        
        /// <summary>
        /// 默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 状态：0禁用，1启用
        /// </summary>
        public int Status { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        
        /// <summary>
        /// 模板内容
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        #region  PdfExporterAttribute
        /// <summary>
        /// 打印方向
        /// </summary>
        public int? Orientation { get; set; }

        /// <summary>
        /// 纸张类型
        /// </summary>
        public int? PaperKind { get; set; }

        /// <summary>
        /// 纸张宽度
        /// </summary>
        public string PaperWidth { get; set; }

        /// <summary>
        /// 纸张高度
        /// </summary>
        public string PaperHeight { get; set; }
        #endregion
        
		
		public bool IsDeleted { get; set; }
    }
}