using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Business.BaseData
{
    public class Organization : AuditedAggregateRoot<Guid>, ISoftDelete
    {
        /// <summary>
        /// 机构分类：1.公司；2.组织；3.部门；4.供应商
        /// </summary>
        public short CategoryId { get; set; }

        public Guid? Pid { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        /// <summary>
        /// 行政地区ID
        /// </summary>
        public int AreaId { get; set; }

        public string Address { get; set; }

        public string Tel { get; set; }

        public string Remark { get; set; }

        public bool IsDeleted { get; set; }

        public Organization(Guid id,short categoryId,Guid? pid,[NotNull]string code,[NotNull]string name,string fullName,int areaId,string address,string tel,string remark)
        {
            Id = id;
            CategoryId = categoryId;
            Pid = pid;
            Code = code;
            Name = name;
            FullName = fullName;
            AreaId = areaId;
            Address = address;
            Tel = tel;
            Remark = remark;
        }
    }
}
