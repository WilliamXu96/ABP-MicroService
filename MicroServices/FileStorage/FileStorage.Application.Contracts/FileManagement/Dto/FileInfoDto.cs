using FileStorage.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace FileStorage.FileManagement.Dto
{
    public class FileInfoDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string RealName { get; set; }

        public string Suffix { get; set; }

        public string Size { get; set; }

        public string Url { get; set; }

        public FileType Type { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
