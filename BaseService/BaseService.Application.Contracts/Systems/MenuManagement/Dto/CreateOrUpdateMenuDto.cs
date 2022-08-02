using System;

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

        public string Name { get; set; }

        public int Sort { get; set; }

        public string Route { get; set; }

        public string Permission { get; set; }

        public string Icon { get; set; }
    }
}
