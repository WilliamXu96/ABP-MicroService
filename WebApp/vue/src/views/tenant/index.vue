<template>
  <div class="app-container">
    <div class="head-container">
      <el-input v-model="listQuery.Filter" clearable size="small" placeholder="搜索..." style="width: 200px;"
        class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button class="filter-item" size="mini" type="success" icon="el-icon-search"
        @click="handleFilter">搜索</el-button>
      <div style="padding: 6px 0;">
        <el-button class="filter-item" size="mini" type="primary" icon="el-icon-plus" @click="handleCreate"
          v-permission="['AbpTenantManagement.Tenants.Create']">新增</el-button>
      </div>
    </div>

    <el-dialog :visible.sync="dialogFormVisible" :close-on-click-modal="false" :title="formTitle" @close="cancel()"
      width="500px">
      <el-form ref="form" :inline="true" :model="form" :rules="rules" size="small" label-width="90px">
        <el-form-item label="名称" prop="name">
          <el-input v-model="form.name" style="width: 350px;" />
        </el-form-item>
        <el-form-item label="管理员邮箱" prop="adminEmailAddress" v-if="!isEdit">
          <el-input v-model="form.adminEmailAddress" style="width: 350px;" />
        </el-form-item>
        <el-form-item label="管理员密码" prop="adminPassword" v-if="!isEdit">
          <el-input type="password" v-model="form.adminPassword" style="width: 350px;" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>

    <el-row :gutter="15">
      <!--租户管理-->
      <el-col :md="16" style="margin-bottom: 10px">
        <el-card class="box-card" shadow="never">
          <div slot="header" class="clearfix" style="height: 20px">
            <span class="role-span">租户列表</span>
          </div>
          <el-table ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="width: 90%"
            @sort-change="sortChange" @selection-change="handleSelectionChange" @row-click="handleRowClick">
            <el-table-column type="selection" width="44px"></el-table-column>
            <el-table-column label="租户名称" prop="name" sortable="custom" align="center" width="150px">
              <template slot-scope="{row}">
                <span class="link-type" @click="handleUpdate(row)">{{ row.name }}</span>
              </template>
            </el-table-column>
            <el-table-column label="操作" align="center">
              <template slot-scope="{row}">
                <el-button type="text" size="mini" @click="handleUpdate(row)"
                  v-permission="['AbpTenantManagement.Tenants.Update']" icon="el-icon-edit">修改</el-button>
                <el-button type="text" size="mini" @click="handleDelete(row)"
                  v-permission="['AbpTenantManagement.Tenants.Delete']" icon="el-icon-delete">删除</el-button>
              </template>
            </el-table-column>
          </el-table>
          <pagination v-show="totalCount > 0" :total="totalCount" :page.sync="page"
            :limit.sync="listQuery.MaxResultCount" @pagination="getList" />
        </el-card>
      </el-col>

      <el-col :md="8">
        <el-card class="box-card" shadow="never">
          <div slot="header" style="height: 20px; line-height: 20px">
            <el-tooltip class="item" effect="dark" content="选择租户菜单权限" placement="top">
              <span>租户菜单</span>
            </el-tooltip>
            <span style="float: right; margin-top: -8px">
              <el-checkbox v-model="checked" @change="checkedAll" :disabled="multipleSelection.length != 1">
                全选</el-checkbox>
              <el-button v-permission="['AbpIdentity.Roles.ManagePermissions']" :loading="savePerLoading"
                :disabled="multipleSelection.length != 1" icon="el-icon-check" type="text"
                @click="savePer">保存</el-button>
            </span>
          </div>
          <el-tree ref="tree" v-loading="treeLoading" :check-strictly="true" :data="menus" show-checkbox node-key="id"
            class="permission-tree" />
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";

const defaultForm = {
  id: null,
  adminEmailAddress: '',
  adminPassword: '',
  name: ''
};
export default {
  name: "Tenant",
  components: { Pagination },
  directives: { permission },
  data() {
    return {
      rules: {
        name: [{ required: true, message: "请输入租户名", trigger: "blur" }],
      },
      form: Object.assign({}, defaultForm),
      list: null,
      menuData: [],
      menus: [],
      totalCount: 0,
      listLoading: true,
      formLoading: false,
      treeLoading: false,
      savePerLoading: false,
      listQuery: {
        Filter: "",
        Sorting: "",
        SkipCount: 0,
        MaxResultCount: 10,
      },
      page: 1,
      dialogFormVisible: false,
      multipleSelection: [],
      formTitle: "",
      isEdit: false,
      checked: false,
    };
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios
        .gets("/api/multi-tenancy/tenants", this.listQuery)
        .then((response) => {
          this.list = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
    },
    fetchData(id) {
      this.$axios.gets("/api/multi-tenancy/tenants/" + id).then((response) => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    save() {
      this.$refs.form.validate((valid) => {
        if (valid) {
          this.formLoading = true;
          this.form.roleNames = this.checkedRole;
          if (this.isEdit) {
            this.$axios
              .puts("/api/multi-tenancy/tenants/" + this.form.id, this.form)
              .then((response) => {
                this.formLoading = false;
                this.$notify({
                  title: "成功",
                  message: "更新成功",
                  type: "success",
                  duration: 2000,
                });
                this.dialogFormVisible = false;
                this.getList();
              })
              .catch(() => {
                this.formLoading = false;
              });
          } else {
            this.$axios
              .posts("/api/multi-tenancy/tenants", this.form)
              .then((response) => {
                this.formLoading = false;
                this.$notify({
                  title: "成功",
                  message: "新增成功",
                  type: "success",
                  duration: 2000,
                });
                this.dialogFormVisible = false;
                this.getList();
              })
              .catch(() => {
                this.formLoading = false;
              });
          }
        }
      });
    },
    savePer() {
      this.savePerLoading = true;
      let params = {};
      let checkedKeys = this.$refs.tree.getCheckedKeys();
      this.$axios
        .posts("/api/base/tenant/menu", {
          tenantId: this.multipleSelection[0].id,
          menuIds: checkedKeys,
        }).then(() => {
          this.$notify({
            title: "成功",
            message: "更新成功",
            type: "success",
            duration: 2000,
          });
          this.savePerLoading = false;
        }).catch(() => {
          this.savePerLoading = false;
        });
    },
    handleCreate() {
      this.formTitle = "新增租户";
      this.isEdit = false;
      this.dialogFormVisible = true;
    },
    handleDelete(row) {
      this.$confirm("是否删除" + row.name + "?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          this.$axios
            .deletes("/api/multi-tenancy/tenants/" + row.id)
            .then((response) => {
              this.$notify({
                title: "成功",
                message: "删除成功",
                type: "success",
                duration: 2000,
              });
              this.getList();
            });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除",
          });
        });
    },
    handleUpdate(row) {
      this.formTitle = "修改租户";
      this.isEdit = true;
      if (row) {
        this.fetchData(row.id);
        this.dialogFormVisible = true;
      } else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: "编辑必须选择单行",
            type: "warning",
          });
          return;
        } else {
          this.fetchData(this.multipleSelection[0].id);
          this.dialogFormVisible = true;
        }
      }
    },
    handleRowClick(row, column, event) {
      if (
        this.multipleSelection.length == 1 &&
        this.multipleSelection[0].id == row.id
      ) {
        return;
      }
      this.treeLoading = true;
      this.$refs.multipleTable.clearSelection();
      this.$refs.multipleTable.toggleRowSelection(row);
      this.$axios.gets("/api/base/tenant/menu/" + row.id).then((response) => {
        this.$refs.tree.setCheckedKeys(response.items);
        this.menuData = response.items;
        this.menus = response.items.filter((_) => _.pid == null);
        this.setChildren(this.menus, response.items);
        this.$refs.tree.setCheckedKeys(response.items.filter(_ => _.isHost == false).map(_ => _.id));
        this.treeLoading = false;
      });
    },
    checkedAll() {
      if (this.checked) {
        //全选
        this.$refs.tree.setCheckedNodes(this.menuData);
      } else {
        //取消选中
        this.$refs.tree.setCheckedKeys([]);
      }
    },
    setChildren(roots, items) {
      roots.forEach((element) => {
        items.forEach((item) => {
          if (item.pid == element.id) {
            if (!element.children) element.children = [];
            element.children.push(item);
          }
        });
        if (element.children) {
          this.setChildren(element.children, items);
        }
      });
    },
    sortChange(data) {
      const { prop, order } = data;
      if (!prop || !order) {
        this.handleFilter();
        return;
      }
      this.listQuery.Sorting = prop + " " + order;
      this.handleFilter();
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    cancel() {
      this.form = Object.assign({}, defaultForm);
      this.dialogFormVisible = false;
      this.$refs.form.clearValidate();
    },
  },
};
</script>