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

        [Required]
        public string Label { get; set; }

        public int Sort { get; set; }

        public string Path { get; set; }

        public string Component { get; set; }

        public string Permission { get; set; }

        public string Icon { get; set; }
    }
}
