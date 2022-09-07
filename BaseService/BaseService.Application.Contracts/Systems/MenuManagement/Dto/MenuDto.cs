using System;
using Volo.Abp.Application.Dtos;

namespace BaseService.Systems.MenuManagement.Dto
{
    public class MenuDto : EntityDto<Guid>
    {
        public Guid? Pid { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public int CategoryId { get; set; }

        public string Label { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }

        public string Path { get; set; }

        public string Component { get; set; }

        public string Route { get; set; }

        public string Permission { get; set; }

        public string Icon { get; set; }

        public string ParentLabel { get; set; }
    }
}
