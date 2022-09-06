using System;
using System.ComponentModel.DataAnnotations;

namespace BaseService.Systems.MenuManagement.Dto
{
    public class CreateOrUpdateMenuDto
    {
        public Guid? FormId { get; set; }

        public Guid? Pid { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public int Sort { get; set; }

        [Required]
        public string Path { get; set; }

        public string Component { get; set; }

        [Required]
        public string Permission { get; set; }

        [Required]
        public string Icon { get; set; }
    }
}
