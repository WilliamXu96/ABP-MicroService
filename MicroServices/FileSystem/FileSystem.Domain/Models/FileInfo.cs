using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace FileSystem.Models
{
    public class FileInfo : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public string FileName { get; set; }

        public string Md5Code { get; set; }

        public long Size { get; set; }

        public string Url { get; set; }

        public bool IsDeleted { get; set; }

        protected FileInfo()
        {

        }

        public FileInfo(Guid id, Guid? tenantId, string fileName, string md5code, long size, string url) : base(id)
        {
            TenantId = tenantId;
            FileName = fileName;
            Md5Code = md5code;
            Size = size;
            Url = url;
        }
    }
}
