using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseService.BaseData.DataDictionaryManagement.Dto
{
    public class CreateOrUpdateDictionaryDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
