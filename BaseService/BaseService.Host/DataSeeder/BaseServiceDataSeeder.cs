using BaseService.BaseData;
using BaseService.Systems;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace BaseService.DataSeeder
{
    public class BaseServiceDataSeeder : IDataSeedContributor, ITransientDependency
    {
        public IIdentityRoleRepository RoleRepository { get; }
        private readonly IRepository<DataDictionary, Guid> _dataDicRepository;
        private readonly IRepository<DataDictionaryDetail, Guid> _dataDicDetailRepository;
        private readonly IRepository<Menu, Guid> _menuRepository;
        private readonly IRepository<RoleMenu> _roleMenuRepository;
        private readonly IGuidGenerator _guidGenerator;

        public BaseServiceDataSeeder(
            IRepository<DataDictionary, Guid> dataDicRepository,
            IRepository<DataDictionaryDetail, Guid> dataDicDetailRepository,
            IRepository<Menu, Guid> menuRepository,
            IRepository<RoleMenu> roleMenuRepository,
            IGuidGenerator guidGenerator,
            IIdentityRoleRepository roleRepository)
        {
            _dataDicRepository = dataDicRepository;
            _dataDicDetailRepository = dataDicDetailRepository;
            _guidGenerator = guidGenerator;
            _menuRepository = menuRepository;
            _roleMenuRepository = roleMenuRepository;
            RoleRepository = roleRepository;
        }

        public virtual async Task SeedAsync(DataSeedContext context)
        {
            await CreateDataDictionaries();
        }

        //创建数据字典
        private async Task CreateDataDictionaries()
        {
            var existDic = await (await _dataDicRepository.GetQueryableAsync()).AnyAsync(d => d.Name == "condition");
            if (!existDic)
            {
                var id = _guidGenerator.Create();
                await _dataDicRepository.InsertAsync(new DataDictionary(id, "condition", "表单条件"));
                await CreateDataDictionaryDetails(id);
            }
            
            var existMenu = await _menuRepository.AnyAsync();
            if(!existMenu)
            {
                await CreateMenus();
            }

        }

        private async Task CreateDataDictionaryDetails(Guid pid)
        {
            await _dataDicDetailRepository.InsertAsync(new DataDictionaryDetail(_guidGenerator.Create(), pid, "等于", "=", 0));
            await _dataDicDetailRepository.InsertAsync(new DataDictionaryDetail(_guidGenerator.Create(), pid, "大于", ">", 1));
            await _dataDicDetailRepository.InsertAsync(new DataDictionaryDetail(_guidGenerator.Create(), pid, "小于", "<", 2));
        }

        private async Task CreateMenus()
        {
            var adminRole = (await RoleRepository.GetDbSetAsync()).FirstOrDefault();
            var menus = new MenuSeeder().GetSeed();
            foreach (var menu in menus)
            {
                await _menuRepository.InsertAsync(menu);
                await _roleMenuRepository.InsertAsync(new RoleMenu(adminRole.Id,menu.Id));
            }
        }
    }
}
