using BaseService.Systems;
using System;
using System.Collections.Generic;

namespace BaseService.DataSeeder
{
    public class MenuSeeder
    {
        public List<Menu> GetSeed()
        {
            var seed = new List<Menu>();
            var saas = new Menu(Guid.NewGuid()) { CategoryId = 1, Name = "SaaS", Label = "SaaS", Sort = 1, Path = "/saas", Component = "Layout", Permission = "AbpTenantManagement.Tenants", Icon = "cloud", AlwaysShow = true, IsHost = true };
            var tenant = new Menu(Guid.NewGuid()) { Pid = saas.Id, CategoryId = 1, Name = "tenant", Label = "租户管理", Sort = 1, Path = "tenant", Component = "tenant/index", Permission = "AbpTenantManagement.Tenants", Icon = "users", IsHost = true };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = tenant.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "AbpTenantManagement.Tenants.Create", Icon = "create", Hidden = true, IsHost = true });
            seed.Add(saas); seed.Add(tenant);

            var systemManagement = new Menu(Guid.NewGuid()) { CategoryId = 1, Name = "systemManagement", Label = "系统管理", Sort = 2, Path = "/system", Component = "Layout", Icon = "system", AlwaysShow = true };
            var user = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "user", Label = "用户管理", Sort = 3, Path = "user", Component = "user/index", Permission = "AbpIdentity.Users", Icon = "user" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = user.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "AbpIdentity.Users.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = user.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "AbpIdentity.Users.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = user.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "AbpIdentity.Users.Delete", Icon = "delete", Hidden = true });

            var menu = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "menu", Label = "菜单管理", Sort = 4, Path = "menu", Component = "menu/index", Permission = "BaseService.Menu", Icon = "menu" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = menu.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "BaseService.Menu.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = menu.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "BaseService.Menu.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = menu.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "BaseService.Menu.Delete", Icon = "delete", Hidden = true });

            var role = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "role", Label = "角色管理", Sort = 5, Path = "role", Component = "role/index", Permission = "AbpIdentity.Roles", Icon = "role" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = role.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "AbpIdentity.Roles.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = role.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "AbpIdentity.Roles.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = role.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "AbpIdentity.Roles.Delete", Icon = "delete", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = role.Id, CategoryId = 2, Name = "RolePermissions", Label = "角色授权", Sort = 3, Permission = "AbpIdentity.Roles.ManagePermissions", Hidden = true });

            var org = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "org", Label = "组织机构", Sort = 6, Path = "org", Component = "org/index", Permission = "BaseService.Organization", Icon = "org" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = org.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "BaseService.Organization.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = org.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "BaseService.Organization.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = org.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "BaseService.Organization.Delete", Icon = "delete", Hidden = true });

            var dict = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "dict", Label = "数据字典", Sort = 7, Path = "dict", Component = "dict/index", Permission = "BaseService.DataDictionary", Icon = "data" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = dict.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "BaseService.DataDictionary.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = dict.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "BaseService.DataDictionary.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = dict.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "BaseService.DataDictionary.Delete", Icon = "delete", Hidden = true });

            var job = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "job", Label = "岗位管理", Sort = 8, Path = "job", Component = "job/index", Permission = "BaseService.Job", Icon = "job" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = job.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "BaseService.Job.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = job.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "BaseService.Job.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = job.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "BaseService.Job.Delete", Icon = "delete", Hidden = true });

            var log = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "log", Label = "系统日志", Sort = 9, Path = "log", Component = "log/index", Permission = "BaseService.AuditLogging", Icon = "log" };
            seed.Add(systemManagement);
            seed.Add(user); seed.Add(menu); seed.Add(role); seed.Add(org); seed.Add(dict); seed.Add(job); seed.Add(log);

            var baseData = new Menu(Guid.NewGuid()) { CategoryId = 1, Name = "base", Label = "基础资料", Sort = 3, Path = "/base", Component = "Layout", Icon = "base", AlwaysShow = true };
            var book = new Menu(Guid.NewGuid()) { Pid = baseData.Id, CategoryId = 1, Name = "book", Label = "Book", Sort = 10, Path = "book", Component = "book/index", Permission = "Business.Book", Icon = "book" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = book.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "Business.Book.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = book.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "Business.Book.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = book.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "Business.Book.Delete", Icon = "delete", Hidden = true });

            var print = new Menu(Guid.NewGuid()) { Pid = baseData.Id, CategoryId = 1, Name = "print", Label = "打印模板", Sort = 9, Path = "print", Component = "print/index", Permission = "Business.PrintTemplate", Icon = "printer" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = print.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "Business.PrintTemplate", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = print.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "Business.PrintTemplate.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = print.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "Business.PrintTemplate.Delete", Icon = "delete", Hidden = true });
            seed.Add(baseData); seed.Add(book); seed.Add(print);

            var systemTool = new Menu(Guid.NewGuid()) { CategoryId = 1, Name = "tool", Label = "系统工具", Sort = 4, Path = "/tool", Component = "Layout", Icon = "tool", AlwaysShow = true };
            var form = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "form", Label = "表单管理", Sort = 11, Path = "form", Component = "form/index", Permission = "FormManagement.Form", Icon = "control" };
            var flow = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "flow", Label = "流程管理", Sort = 12, Path = "flow", Component = "flow-design/index", Permission = "FlowManagement.Flow", Icon = "flow" };
            var flowDisplay = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "flowDisplay", Label = "流程详细", Sort = 13, Path = "flowDisplay/:id", Component = "flow/display", Permission = "FlowManagement.Flow", Hidden = true };
            var flowCreate = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "flowCreate", Label = "新增流程", Sort = 14, Path = "flowCreate", Component = "flow-design/create", Permission = "FlowManagement.Flow.Create", Hidden = true };
            var flowEdit = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "flowEdit", Label = "修改流程", Sort = 15, Path = "flowEdit/:id", Component = "flow-design/edit", Permission = "FlowManagement.Flow.Update", Hidden = true };
            var build = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "build", Label = "代码生成", Sort = 16, Path = "build", Component = "build/index", Permission = "FormManagement.FormBuild", Icon = "code" };
            var storage = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "storage", Label = "文件存储", Sort = 17, Path = "storage", Component = "storage/index", Permission = "StorageManagement.File", Icon = "storage" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = storage.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "StorageManagement.File.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = storage.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "StorageManagement.File.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = storage.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "StorageManagement.File.Delete", Icon = "delete", Hidden = true });

            var formCreate = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "formCreate", Label = "新增表单", Sort = 18, Path = "form/create", Component = "form/create", Permission = "FormManagement.Form.Create", Hidden = true };
            var formEdit = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "formEdit", Label = "修改表单", Sort = 19, Path = "form/edit/:id", Component = "form/edit", Permission = "FormManagement.Form.Update", Hidden = true };
            var buildEdit = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "buildEdit", Label = "生成配置", Sort = 20, Path = "buildEdit/:id", Component = "build/components/index", Permission = "FormManagement.FormBuild.Update", Hidden = true };
            seed.Add(systemTool);
            seed.Add(form); seed.Add(flow); seed.Add(flowDisplay); seed.Add(flowCreate); seed.Add(flowEdit); seed.Add(build); seed.Add(storage); seed.Add(formCreate);
            seed.Add(formEdit); seed.Add(buildEdit);

            return seed;
        }

        public List<Menu> GetVue3Seed()
        {
            var seed = new List<Menu>();
            var saas = new Menu(Guid.NewGuid()) { CategoryId = 1, Name = "SaaS", Label = "SaaS", Sort = 1, Path = "/saas", Component = "Layout", Permission = "AbpTenantManagement.Tenants", Icon = "ele-Cloudy", IsHost = true, Title = "message.router.SaaS" };
            var tenant = new Menu(Guid.NewGuid()) { Pid = saas.Id, CategoryId = 1, Name = "tenant", Label = "租户管理", Sort = 1, Path = "tenant", Component = "tenant/index", Permission = "AbpTenantManagement.Tenants", Icon = "ele-Avatar", IsHost = true, Title = "message.router.tenant" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = tenant.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "AbpTenantManagement.Tenants.Create", Icon = "create", Hidden = true, IsHost = true });
            seed.Add(saas); seed.Add(tenant);

            var home = new Menu(Guid.NewGuid()) { CategoryId = 1, Name = "home", Label = "首页", Sort = 1, Path = "/home", Component = "Layout", Permission = "AbpTenantManagement.Tenants", Icon = "ele-House", IsHost = true, Title = "message.router.home" };
            var dashboard = new Menu(Guid.NewGuid()) { Pid = home.Id, CategoryId = 1, Name = "dashboard", Label = "工作台", Sort = 1, Path = "/home/dashboard", Component = "/home/index", Icon = "ele-HomeFilled", IsHost = true, Title = "message.router.dashboard", IsAffix = true };
            seed.Add(home); seed.Add(dashboard);

            var systemManagement = new Menu(Guid.NewGuid()) { CategoryId = 1, Name = "systemManagement", Label = "系统管理", Sort = 2, Path = "/system", Component = "Layout", Icon = "ele-Setting", AlwaysShow = true, Title = "message.router.systemManagement" };
            var user = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "user", Label = "用户管理", Sort = 3, Path = "/system/user", Component = "/system/user/index", Permission = "AbpIdentity.Users", Icon = "iconfont icon-icon-", Title = "message.router.user" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = user.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "AbpIdentity.Users.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = user.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "AbpIdentity.Users.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = user.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "AbpIdentity.Users.Delete", Icon = "delete", Hidden = true });

            var menu = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "menu", Label = "菜单管理", Sort = 4, Path = "/system/menu", Component = "/system/menu/index", Permission = "BaseService.Menu", Icon = "fa fa-sliders", Title = "message.router.menu" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = menu.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "BaseService.Menu.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = menu.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "BaseService.Menu.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = menu.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "BaseService.Menu.Delete", Icon = "delete", Hidden = true });

            var role = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "role", Label = "角色管理", Sort = 5, Path = "/system/role", Component = "/system/role/index", Permission = "AbpIdentity.Roles", Icon = "iconfont icon-shenqingkaiban", Title = "message.router.role" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = role.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "AbpIdentity.Roles.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = role.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "AbpIdentity.Roles.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = role.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "AbpIdentity.Roles.Delete", Icon = "delete", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = role.Id, CategoryId = 2, Name = "RolePermissions", Label = "角色授权", Sort = 3, Permission = "AbpIdentity.Roles.ManagePermissions", Hidden = true });

            var org = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "org", Label = "组织机构", Sort = 6, Path = "/system/org", Component = "/system/org/index", Permission = "BaseService.Organization", Icon = "fa fa-sitemap", Title = "message.router.org" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = org.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "BaseService.Organization.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = org.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "BaseService.Organization.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = org.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "BaseService.Organization.Delete", Icon = "delete", Hidden = true });

            var dict = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "dict", Label = "数据字典", Sort = 7, Path = "/system/dict", Component = "/system/dict/index", Permission = "BaseService.DataDictionary", Icon = "ele-Folder", Title = "message.router.dict" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = dict.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "BaseService.DataDictionary.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = dict.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "BaseService.DataDictionary.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = dict.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "BaseService.DataDictionary.Delete", Icon = "delete", Hidden = true });

            var job = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "job", Label = "岗位管理", Sort = 8, Path = "job", Component = "job/index", Permission = "BaseService.Job", Icon = "job", Title = "message.router.job" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = job.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "BaseService.Job.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = job.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "BaseService.Job.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = job.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "BaseService.Job.Delete", Icon = "delete", Hidden = true });

            var log = new Menu(Guid.NewGuid()) { Pid = systemManagement.Id, CategoryId = 1, Name = "log", Label = "系统日志", Sort = 9, Path = "log", Component = "log/index", Permission = "BaseService.AuditLogging", Icon = "log", Title = "message.router.log" };
            seed.Add(systemManagement);
            seed.Add(user); seed.Add(menu); seed.Add(role); seed.Add(org); seed.Add(dict); seed.Add(job); seed.Add(log);

            var baseData = new Menu(Guid.NewGuid()) { CategoryId = 1, Name = "base", Label = "基础资料", Sort = 3, Path = "/base", Component = "Layout", Icon = "base", AlwaysShow = true, Title = "message.router.base" };
            var book = new Menu(Guid.NewGuid()) { Pid = baseData.Id, CategoryId = 1, Name = "book", Label = "Book", Sort = 10, Path = "book", Component = "book/index", Permission = "Business.Book", Icon = "book" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = book.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "Business.Book.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = book.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "Business.Book.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = book.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "Business.Book.Delete", Icon = "delete", Hidden = true });

            var print = new Menu(Guid.NewGuid()) { Pid = baseData.Id, CategoryId = 1, Name = "print", Label = "打印模板", Sort = 9, Path = "print", Component = "print/index", Permission = "Business.PrintTemplate", Icon = "printer", Title = "message.router.print" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = print.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "Business.PrintTemplate", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = print.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "Business.PrintTemplate.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = print.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "Business.PrintTemplate.Delete", Icon = "delete", Hidden = true });
            seed.Add(baseData); seed.Add(book); seed.Add(print);

            var systemTool = new Menu(Guid.NewGuid()) { CategoryId = 1, Name = "tool", Label = "系统工具", Sort = 4, Path = "/tool", Component = "Layout", Icon = "tool", AlwaysShow = true, Title = "message.router.tool" };
            var form = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "form", Label = "表单管理", Sort = 11, Path = "form", Component = "form/index", Permission = "FormManagement.Form", Icon = "control", Title = "message.router.form" };
            var flow = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "flow", Label = "流程管理", Sort = 12, Path = "flow", Component = "flow-design/index", Permission = "FlowManagement.Flow", Icon = "flow", Title = "message.router.flow" };
            var flowDisplay = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "flowDisplay", Label = "流程详细", Sort = 13, Path = "flowDisplay/:id", Component = "flow/display", Permission = "FlowManagement.Flow", Hidden = true, Title = "message.router.flowDisplay" };
            var flowCreate = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "flowCreate", Label = "新增流程", Sort = 14, Path = "flowCreate", Component = "flow-design/create", Permission = "FlowManagement.Flow.Create", Hidden = true, Title = "message.router.flowCreate" };
            var flowEdit = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "flowEdit", Label = "修改流程", Sort = 15, Path = "flowEdit/:id", Component = "flow-design/edit", Permission = "FlowManagement.Flow.Update", Hidden = true, Title = "message.router.flowEdit" };
            var build = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "build", Label = "代码生成", Sort = 16, Path = "build", Component = "build/index", Permission = "FormManagement.FormBuild", Icon = "code", Title = "message.router.build" };
            var storage = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "storage", Label = "文件存储", Sort = 17, Path = "storage", Component = "storage/index", Permission = "StorageManagement.File", Icon = "storage", Title = "message.router.storage" };
            seed.Add(new Menu(Guid.NewGuid()) { Pid = storage.Id, CategoryId = 2, Name = "Create", Label = "新增", Sort = 3, Permission = "StorageManagement.File.Create", Icon = "create", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = storage.Id, CategoryId = 2, Name = "Update", Label = "修改", Sort = 3, Permission = "StorageManagement.File.Update", Icon = "update", Hidden = true });
            seed.Add(new Menu(Guid.NewGuid()) { Pid = storage.Id, CategoryId = 2, Name = "Delete", Label = "删除", Sort = 3, Permission = "StorageManagement.File.Delete", Icon = "delete", Hidden = true });

            var formCreate = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "formCreate", Label = "新增表单", Sort = 18, Path = "form/create", Component = "form/create", Permission = "FormManagement.Form.Create", Hidden = true, Title = "message.router.formCreate" };
            var formEdit = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "formEdit", Label = "修改表单", Sort = 19, Path = "form/edit/:id", Component = "form/edit", Permission = "FormManagement.Form.Update", Hidden = true, Title = "message.router.formEdit" };
            var buildEdit = new Menu(Guid.NewGuid()) { Pid = systemTool.Id, CategoryId = 1, Name = "buildEdit", Label = "生成配置", Sort = 20, Path = "buildEdit/:id", Component = "build/components/index", Permission = "FormManagement.FormBuild.Update", Hidden = true, Title = "message.router.buildEdit" };
            seed.Add(systemTool);
            seed.Add(form); seed.Add(flow); seed.Add(flowDisplay); seed.Add(flowCreate); seed.Add(flowEdit); seed.Add(build); seed.Add(storage); seed.Add(formCreate);
            seed.Add(formEdit); seed.Add(buildEdit);

            return seed;
        }
    }
}
