using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace BaseService.Systems.UserRoleMenusManagement.Dto
{
    public class MenusTreeDto : EntityDto<Guid>
    {
        public Guid? Pid { get; set; }

        public string Label { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }

        public string Permission { get; set; }
    }
}
