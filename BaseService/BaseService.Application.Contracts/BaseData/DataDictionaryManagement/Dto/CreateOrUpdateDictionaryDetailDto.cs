using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseService.BaseData.DataDictionaryManagement.Dto
{
    public class CreateOrUpdateDictionaryDetailDto
    {
        public Guid Pid { get; set; }

        [Required]
        public string Label { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public short Sort { get; set; }
    }
}
