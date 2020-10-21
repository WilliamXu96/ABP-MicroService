using FileStorage.Enums;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace FileStorage.Models
{
    public class FileInfo : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public string Name { get; set; }

        public string RealName { get; set; }

        /// <summary>
        /// 后缀
        /// </summary>
        public string Suffix { get; set; }

        public string Md5Code { get; set; }

        public string Size { get; set; }

        public string Path { get; set; }

        public string Url { get; set; }

        public FileType Type { get; set; }

        public bool IsDeleted { get; set; }

        protected FileInfo()
        {

        }

        public FileInfo(Guid id, Guid? tenantId, string name,string realName, string suffix, string md5code, string size, string path,string url, FileType type) : base(id)
        {
            TenantId = tenantId;
            Name = name;
            RealName = realName;
            Suffix = suffix;
            Md5Code = md5code;
            Size = size;
            Path = path;
            Url = url;
            Type = type;
        }
    }
}
