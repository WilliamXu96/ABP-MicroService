<template>
  <div class="app-container">
    <div class="head-container">
      <!-- 搜索 -->
      <el-input
        v-model="listQuery.Filter"
        clearable
        size="small"
        placeholder="搜索..."
        style="width: 200px"
        class="filter-item"
        @keyup.enter.native="handleFilter"
      />
      <el-button
        class="filter-item"
        size="mini"
        type="success"
        icon="el-icon-search"
        @click="handleFilter"
        >搜索</el-button
      >
      <div style="padding: 6px 0">
        <el-button
          class="filter-item"
          size="mini"
          type="primary"
          icon="el-icon-plus"
          @click="handleCreate"
          v-permission="['BaseService.Job.Create']"
          >新增</el-button
        >
        <el-button
          class="filter-item"
          size="mini"
          type="success"
          icon="el-icon-edit"
          v-permission="['BaseService.Job.Update']"
          @click="handleUpdate()"
          >修改</el-button
        >
        <el-button
          slot="reference"
          class="filter-item"
          type="danger"
          icon="el-icon-delete"
          size="mini"
          v-permission="['BaseService.Job.Delete']"
          @click="handleDelete()"
          >删除</el-button
        >
      </div>
    </div>
    <el-dialog
      :visible.sync="dialogFormVisible"
      :close-on-click-modal="false"
      :title="formTitle"
      @close="cancel()"
      width="600px"
    >
      <el-form
        ref="form"
        :inline="true"
        :model="form"
        :rules="rules"
        size="small"
        label-width="80px"
      >
        <el-form-item label="菜单类型" prop="categoryId">
          <el-select
            v-model="form.categoryId"
            placeholder="请选择"
            style="width: 184px;" 
            :disabled="isEdit"
          >
            <el-option label="菜单" :value="1"></el-option>
            <el-option label="按钮" :value="2"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="上级菜单" prop="menuType">
          <treeselect
                v-model="form.pid"
                :load-options="loadMenus"
                :options="menus"
                :disabled="isEdit"
                style="width: 184px;"
                placeholder="根目录"
              />
        </el-form-item>
        <el-form-item label="菜单名称" prop="name">
          <el-input v-model="form.name" placeholder="请输入菜单名称" style="width: 184px;" />
        </el-form-item>
        <el-form-item label="显示名称" prop="label">
          <el-input v-model="form.label" placeholder="请输入显示名称" style="width: 184px;" />
        </el-form-item>
        <el-form-item label="菜单排序" prop="sort">
          <el-input-number
            v-model="form.sort"
            controls-position="right"
            :min="0"
            style="width: 184px;" 
          />
        </el-form-item>
         <el-form-item label="菜单图标" prop="icon" v-if="form.categoryId==1">
          <el-popover
                placement="bottom-start"
                width="460"
                trigger="click"
                @show="$refs['iconSelect'].reset()"
              >
                <IconSelect ref="iconSelect" @selected="selected" />
                <el-input slot="reference" v-model="form.icon" placeholder="点击选择图标" style="width: 184px;" readonly>
                  <svg-icon
                    v-if="form.icon"
                    slot="prefix"
                    :icon-class="form.icon"
                    class="el-input__icon"
                    style="height: 32px;width: 16px;"
                  />
                  <i v-else slot="prefix" class="el-icon-search el-input__icon" />
                </el-input>
              </el-popover>
        </el-form-item>
        <el-form-item label="路由地址" prop="path" v-if="form.categoryId==1">
          <el-input v-model="form.path" placeholder="请输入路由地址" style="width: 184px;" />
        </el-form-item>
        <el-form-item label="组件路径" prop="component" v-if="form.categoryId==1">
          <el-input v-model="form.component" placeholder="请输入组件路径" style="width: 184px;" />
        </el-form-item>
        <el-form-item label="权限标识">
          <el-input
            v-model="form.permission"
            placeholder="请输入权限标识"
            maxlength="50"
           style="width: 184px;" 
          />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button
          size="small"
          v-loading="formLoading"
          type="primary"
          @click="save"
          >确认</el-button
        >
      </div>
    </el-dialog>
    <el-table
      ref="multipleTable"
      v-loading="listLoading"
      :data="list"
      size="small"
      style="width: 90%"
      row-key="id"
      :tree-props="{children: 'children'}"
    >
      <el-table-column
        label="菜单名"
        prop="label"
        sortable="custom"
      >
        <template slot-scope="{ row }">
          <span class="link-type" @click="handleUpdate(row)">{{
            row.label
          }}</span>
        </template>
      </el-table-column>
      <el-table-column prop="icon" label="图标" align="center" width="100">
        <template slot-scope="scope">
          <svg-icon :icon-class="scope.row.icon" />
        </template>
      </el-table-column>
      <el-table-column label="排序" prop="sort" align="center" />
      <el-table-column label="菜单类型" prop="categoryId" align="center">
        <template slot-scope="scope">
              <span>{{scope.row.categoryId | displayCategory}}</span>
            </template>
      </el-table-column>
      <el-table-column label="组件路径" prop="component" align="center" />
      <el-table-column label="操作" align="center">
        <template slot-scope="{ row }">
          <el-button
            type="text"
            size="mini"
            @click="handleUpdate(row)"
            icon="el-icon-edit"
          >修改</el-button>
          <el-button
            type="text"
            size="mini"
            @click="handleDelete(row)"
            icon="el-icon-delete"
          >删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination
      v-show="totalCount > 0"
      :total="totalCount"
      :page.sync="page"
      :limit.sync="listQuery.MaxResultCount"
      @pagination="getList"
    />
  </div>
</template>

<script>
import permission from "@/directive/permission/index.js";
import Treeselect from "@riophae/vue-treeselect";
import "@riophae/vue-treeselect/dist/vue-treeselect.css";
import { LOAD_CHILDREN_OPTIONS } from "@riophae/vue-treeselect";
import IconSelect from "@/components/IconSelect";

const defaultForm = {
  id: null,
  pid:null,
  categoryId:1,
  name: null,
  path: '',
  sort: 999,
  icon: '',
};
export default {
  name: "Menu",
  components: { IconSelect, Treeselect },
  directives: { permission },
  filters: {
    displayCategory(categoryId) {
      const categoryMap = {
        1: "菜单",
        2: "按钮",
      };
      return categoryMap[categoryId];
    }
  },

  data() {
    return {
      rules: {
        name: [{ required: true, message: "请输入菜单名", trigger: "blur" }],
        label: [{ required: true, message: "请输入显示名", trigger: "blur" }],
        sort: [{ required: true, message: "请输入序号", trigger: "blur" }]
      },
      form: Object.assign({}, defaultForm),
      list: [],
      menus:[],
      totalCount: 0,
      listLoading: true,
      formLoading: false,
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
    };
  },
  created() {
    this.getList();
  },
  methods: {
    selected(name) {
      this.form.icon = name;
    },
    getList() {
      this.listLoading = true;
      this.$axios.gets("/api/base/menu/all", this.listQuery).then((response) => {
        this.list = response.items.filter(_=>_.pid==null)
        this.setChildren(this.list,response.items)
        this.listLoading = false;
      });
    },
    setChildren(roots, items) {
      roots.forEach(element => {
        items.forEach(item => {
          if (item.pid == element.id) {
            if(!element.children)
              element.children=[]
            element.children.push(item);
          }
        });
        if (element.children) {
          this.setChildren(element.children, items);
        }
      });
    },
    fetchData(id) {
      this.$axios.gets("/api/base/menu/" + id).then((response) => {
        this.form = response;
        this.menus.push({id:response.pid, label:response.parentLabel})
      });
    },
    loadMenus({ action, parentNode, callback }) {
      if (action === LOAD_CHILDREN_OPTIONS) {
        this.$axios
          .gets("/api/base/menu/loadMenus", { id: parentNode.id })
          .then(response => {
            parentNode.children = response.items.map(function(obj) {
              if (!obj.leaf) {
                obj.children = null;
              }
              return obj;
            });
            setTimeout(() => {
              callback();
            }, 100);
          });
      }
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
              .puts("/api/base/menu/" + this.form.id, this.form)
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
              .posts("/api/base/menu", this.form)
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
    handleCreate() {
      this.formTitle = "新增菜单";
      this.isEdit = false;
      this.dialogFormVisible = true;
      this.$axios.gets("/api/base/menu/loadMenus").then(response => {
        this.menus = response.items.map(function(obj) {
          if (!obj.leaf) {
            obj.children = null;
          }
          return obj;
        });
      });
    },
    handleDelete(row) {
      var params = [];
      let alert = "";
      if (row) {
        params.push(row.id);
        alert = row.name;
      } else {
        if (this.multipleSelection.length === 0) {
          this.$message({
            message: "未选择",
            type: "warning",
          });
          return;
        }
        this.multipleSelection.forEach((element) => {
          let id = element.id;
          params.push(id);
        });
        alert = "选中项";
      }
      this.$confirm("是否删除" + alert + "?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          this.$axios.posts("/api/base/menu/delete", params).then((response) => {
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
      this.formTitle = "修改菜单";
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
    handleRowClick(row, column, event) {
      this.$refs.multipleTable.clearSelection();
      this.$refs.multipleTable.toggleRowSelection(row);
    },
    cancel() {
      this.form = Object.assign({}, defaultForm);
      this.dialogFormVisible = false;
      this.$refs.form.clearValidate();
    },
    changeEnabled(data, val) {
      data.active = val ? "启用" : "停用";
      this.$confirm("是否" + data.active + data.name + "？", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          this.$axios
            .puts("/api/base/menu/" + data.id, data)
            .then((response) => {
              this.$notify({
                title: "成功",
                message: "更新成功",
                type: "success",
                duration: 2000,
              });
            })
            .catch(() => {
              data.enabled = !data.enabled;
            });
        })
        .catch(() => {
          data.enabled = !data.enabled;
        });
    },
  },
};
</script>