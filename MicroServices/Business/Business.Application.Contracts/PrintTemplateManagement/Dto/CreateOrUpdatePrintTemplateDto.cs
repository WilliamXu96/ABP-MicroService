using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Business.PrintTemplateManagement.Dto
{
    public class CreateOrUpdatePrintTemplateDto: EntityDto<Guid?>
    {
        
        /// <summary>
        /// 模板名称
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// 模板类型
        /// </summary>
        [Required]
        public int TempType { get; set; }
        
        /// <summary>
        /// 默认
        /// </summary>
        [Required]
        public bool IsDefault { get; set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public int Status { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }
        
        /// <summary>
        /// 模板内容
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

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

    }
}