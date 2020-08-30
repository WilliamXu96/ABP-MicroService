using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BaseService.BaseData.DataDictionaryManagement.Dto
{
    public class DictionaryDetailDto : EntityDto<Guid>
    {
        public Guid Pid { get; set; }

        public string Label { get; set; }

        public string Value { get; set; }

        public short Sort { get; set; }
    }
}
