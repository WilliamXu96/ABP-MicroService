using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Business.BaseData
{
    public class DataDictionary : AuditedAggregateRoot<Guid>, ISoftDelete
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        protected DataDictionary()
        {

        }

        public DataDictionary(Guid id, [NotNull] string name,string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
