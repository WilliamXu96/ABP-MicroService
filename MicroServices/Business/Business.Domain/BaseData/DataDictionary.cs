using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Business.BaseData
{
    public class DataDictionary: AuditedAggregateRoot<Guid>, ISoftDelete
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid? CategoryID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public Guid? PID { get; set; }

        /// <summary>
        /// 次序
        /// </summary>
        public int SEQ { get; set; }

        /// <summary>
        /// 能否编辑
        /// </summary>
        public bool IsEdit { get; set; }

        public bool IsDeleted { get; set; }
    }
}
