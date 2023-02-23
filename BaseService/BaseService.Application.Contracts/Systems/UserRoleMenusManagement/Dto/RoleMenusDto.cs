using System.Collections.Generic;

namespace BaseService.Systems.UserRoleMenusManagement.Dto
{
    public class RoleMenusDto
    {
        public string Path { get; set; }

        public string Component { get; set; }

        public string Name { get; set; }

        public MenuMeta Meta { get; set; }

        public List<RoleMenusDto> Children { get; set; }

        public bool Hidden { get; set; }

        public bool AlwaysShow { get; set; }
    }

    public class MenuMeta
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public bool IsAffix { get; set; }
    }
}
