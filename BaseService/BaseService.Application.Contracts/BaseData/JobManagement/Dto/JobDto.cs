using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BaseService.BaseData.JobManagement.Dto
{
    public class JobDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public bool Enabled { get; set; }

        public int Sort { get; set; }

        public string Description { get; set; }
    }
}
