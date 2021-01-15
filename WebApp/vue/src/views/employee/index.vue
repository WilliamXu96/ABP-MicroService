<template>
  <div class="app-container">
    <el-row :gutter="20">
      <!--侧边组织机构树形列表-->
      <el-col :xs="9" :sm="6" :md="5" :lg="4" :xl="4">
        <div class="head-container">
          <el-input
            v-model="orgName"
            clearable
            size="small"
            placeholder="搜索..."
            prefix-icon="el-icon-search"
            class="filter-item"
            @input="getOrgs"
          />
        </div>
        <el-tree
          :data="orgData"
          :load="getOrgs"
          :props="defaultProps"
          :expand-on-click-node="false"
          lazy
          @node-click="handleNodeClick"
          style="margin-top:15px"
        />
      </el-col>
      <el-col :xs="15" :sm="18" :md="19" :lg="20" :xl="20">
        <div class="head-container">
          <!-- 搜索 -->
          <el-input
            v-model="listQuery.Filter"
            clearable
            size="small"
            placeholder="搜索..."
            style="width: 200px;"
            class="filter-item"
            @keyup.enter.native="handleFilter"
          />
          <el-button
            class="filter-item"
            size="mini"
            type="success"
            icon="el-icon-search"
            @click="handleFilter"
          >搜索</el-button>
          <div style="padding: 6px 0;">
            <el-button
              class="filter-item"
              size="mini"
              type="primary"
              icon="el-icon-plus"
              @click="handleCreate"
              v-permission="['AbpIdentity.Users.Create']"
            >新增</el-button>
            <el-button
              class="filter-item"
              size="mini"
              type="success"
              icon="el-icon-edit"
              v-permission="['AbpIdentity.Users.Update']"
              @click="handleUpdate()"
            >修改</el-button>
            <el-button
              slot="reference"
              class="filter-item"
              type="danger"
              icon="el-icon-delete"
              size="mini"
              v-permission="['AbpIdentity.Users.Delete']"
              @click="handleDelete()"
            >删除</el-button>
          </div>
        </div>
        <el-dialog
          :visible.sync="dialogFormVisible"
          :close-on-click-modal="false"
          :title="formTitle"
          @close="cancel()"
          width="580px"
        >
          <el-form
            ref="form"
            :inline="true"
            :model="form"
            :rules="rules"
            size="small"
            label-width="70px"
          >
            <el-form-item label="名称" prop="name">
              <el-input v-model="form.name" />
            </el-form-item>
            <el-form-item label="电话" prop="phone">
              <el-input v-model="form.phone" />
            </el-form-item>
            <el-form-item label="邮箱" prop="email">
              <el-input v-model="form.email" />
            </el-form-item>
            <el-form-item label="所属机构" prop="orgId">
              <treeselect
                v-model="form.orgId"
                :load-options="loadOrgs"
                :options="orgs"
                style="width: 184px;"
                placeholder="选择所属机构"
              />
            </el-form-item>
            <el-form-item label="岗位" prop="name">
              <el-select
                class="filter-item"
                size="small"
                style="width: 184px"
                multiple
                v-model="form.jobs"
                placeholder="选择岗位"
              >
              <el-option v-for="item in jobData" :key="item.id" :label="item.name" :value="item.id" />
              </el-select>
            </el-form-item>
            <el-form-item label="关联用户" prop="userId">
              <el-input
                v-model="form.userIdToName"
                readonly
                placeholder="请选择"
                style="width: 184px;"
              >
                <el-button slot="append" icon="el-icon-search" @click="handleSelectUser()"></el-button>
              </el-input>
            </el-form-item>
            <el-form-item label="性别" prop="gender">
              <el-radio-group v-model="form.gender" style="width: 184px">
                <el-radio :label="1">男</el-radio>
                <el-radio :label="0">女</el-radio>
              </el-radio-group>
            </el-form-item>
            <el-form-item label="状态" style="margin:left" prop="enabled">
              <el-radio-group v-model="form.enabled" style="width: 140px">
                <el-radio :label="true">启用</el-radio>
                <el-radio :label="false">禁用</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-form>
          <div slot="footer" class="dialog-footer">
            <el-button size="small" type="text" @click="cancel">取消</el-button>
            <el-button size="small" v-loading="formLoading" type="primary" @click="save">确认</el-button>
          </div>
        </el-dialog>

        <el-dialog
          title="选择用户"
          width="600px"
          top="5vh"
          :close-on-click-modal="false"
          :visible.sync="userDialogTableVisible"
        >
          <div class="filter-container">
            <el-input
              size="small"
              v-model="listQuery.Filter"
              placeholder="搜索..."
              style="width: 200px;"
              class="filter-item"
              @keyup.enter.native="userHandleFilter"
            />
            <el-button
              size="mini"
              class="filter-item"
              type="primary"
              icon="el-icon-search"
              @click="userHandleFilter"
            >搜索</el-button>
          </div>

          <el-table
            v-loading="userListLoading"
            :data="userList"
            height="300"
            @row-click="userHandleRowClick"
          >
            <el-table-column label="用户名" prop="userName" align="center" width="150px">
              <template slot-scope="{row}">
                <span class="link-type">{{row.userName}}</span>
              </template>
            </el-table-column>
            <el-table-column label="邮箱" align="center" width="200px">
              <template slot-scope="scope">
                <span>{{scope.row.email}}</span>
              </template>
            </el-table-column>
            <el-table-column label="电话" align="center" width="200px">
              <template slot-scope="scope">
                <span>{{scope.row.phoneNumber}}</span>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination
            small
            layout="prev, pager, next"
            :total="totalCount"
            :page.sync="page"
            :limit.sync="listQuery.MaxResultCount"
            @pagination="getUserList"
          ></el-pagination>
        </el-dialog>
        <el-table
          ref="multipleTable"
          v-loading="listLoading"
          :data="list"
          size="small"
          style="width: 90%;"
          @sort-change="sortChange"
          @selection-change="handleSelectionChange"
          @row-click="handleRowClick"
        >
          <el-table-column type="selection" width="44px"></el-table-column>
          <el-table-column label="岗位名称" prop="name" sortable="custom" align="center" width="150px">
            <template slot-scope="{row}">
              <span class="link-type" @click="handleUpdate(row)">{{row.name}}</span>
            </template>
          </el-table-column>
          <el-table-column label="性别" prop="gender" align="center">
            <template slot-scope="scope">
              <span>{{scope.row.gender | displayGender}}</span>
            </template>
          </el-table-column>
          <el-table-column label="电话" prop="phone" align="center">
            <template slot-scope="scope">
              <span>{{scope.row.phone}}</span>
            </template>
          </el-table-column>
          <el-table-column label="邮箱" prop="email" align="center">
            <template slot-scope="scope">
              <span>{{scope.row.email}}</span>
            </template>
          </el-table-column>
          <el-table-column label="所属机构" prop="orgIdToName" align="center">
            <template slot-scope="scope">
              <span>{{scope.row.orgIdToName}}</span>
            </template>
          </el-table-column>
          <!-- <el-table-column label="关联用户" prop="userId" align="center">
        <template slot-scope="scope">
          <span>{{scope.row.userId}}</span>
        </template>
          </el-table-column>-->
          <el-table-column label="状态" prop="enable" align="center">
            <template slot-scope="scope">
              <el-switch
                v-model="scope.row.enabled"
                active-color="#409EFF"
                inactive-color="#F56C6C"
                @change="changeEnabled(scope.row, scope.row.enabled,)"
              />
            </template>
          </el-table-column>
          <el-table-column label="操作" align="center" width="125">
            <template slot-scope="{row}">
              <el-button
                type="primary"
                size="mini"
                @click="handleUpdate(row)"
                v-permission="['AbpIdentity.Users.Update']"
                icon="el-icon-edit"
              />
              <el-button
                type="danger"
                size="mini"
                @click="handleDelete(row)"
                :disabled="row.userName==='admin'"
                v-permission="['AbpIdentity.Users.Delete']"
                icon="el-icon-delete"
              />
            </template>
          </el-table-column>
        </el-table>

        <pagination
          v-show="totalCount>0"
          :total="totalCount"
          :page.sync="page"
          :limit.sync="listQuery.MaxResultCount"
          @pagination="getList"
        />
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { isvalidPhone } from "@/utils/validate";
import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";
import Treeselect from "@riophae/vue-treeselect";
import "@riophae/vue-treeselect/dist/vue-treeselect.css";
import { LOAD_CHILDREN_OPTIONS } from "@riophae/vue-treeselect";

const defaultForm = {
  id: null,
  name: null,
  gender: 1,
  phone: null,
  email: null,
  enabled: true,
  orgId: null,
  userId: null,
  jobs: [],
  userIdToName: null,
  orgIdToName: null,
};
export default {
  name: "Employee",
  components: { Pagination, Treeselect },
  directives: { permission },
  filters: {
    displayGender(gender) {
      const genderMap = {
        0: "女",
        1: "男",
      };
      return genderMap[gender];
    },
  },
  data() {
    const validPhone = (rule, value, callback) => {
      if (!value) {
        callback(new Error("请输入电话号码"));
      } else if (!isvalidPhone(value)) {
        callback(new Error("请输入正确的11位手机号码"));
      } else {
        callback();
      }
    };
    return {
      rules: {
        name: [{ required: true, message: "请输入职员姓名", trigger: "blur" }],
        // phone: [{ required: true, trigger: "blur", validator: validPhone }],
        // email: [
        //   { required: true, message: "请输入邮箱地址", trigger: "blur" },
        //   { type: "email", message: "请输入正确的邮箱地址", trigger: "blur" }
        // ]
      },
      defaultProps: { children: "children", label: "name", isLeaf: "leaf" },
      form: Object.assign({}, defaultForm),
      list: null,
      orgName: "",
      orgs: [],
      orgData: [],
      jobData:[],
      userList: null,
      totalCount: 0,
      listLoading: true,
      userListLoading: true,
      formLoading: false,
      listQuery: {
        OrgId: null,
        Filter: "",
        Sorting: "",
        SkipCount: 0,
        MaxResultCount: 10,
      },
      page: 1,
      dialogFormVisible: false,
      userDialogTableVisible: false,
      multipleSelection: [],
      formTitle: "",
      isEdit: false,
    };
  },
  created() {
    this.getList();
  },
  methods: {
    getOrgs(node, resolve) {
      const params = {};
      if (typeof node !== "object") {
        if (node) {
          params["filter"] = node;
        }
      } else if (node.level !== 0) {
        params["id"] = node.data.id;
      }
      //TODO:仅获取启用机构
      setTimeout(() => {
        this.$axios
          .gets("/api/business/orgs/loadOrgs", params)
          .then((response) => {
            if (resolve) {
              resolve(response.items);
            } else {
              this.orgData = response.items;
            }
          });
      }, 100);
    },
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios
        .gets("/api/business/employee/all", this.listQuery)
        .then((response) => {
          this.list = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
    },
    getOrgNodes(id) {
      this.$axios.gets("/api/business/orgs/loadNodes").then((response) => {
        this.loadTree(response);
      });
    },
    getJobs(){
      this.$axios.gets('/api/business/job/jobs').then((response)=>{
        this.jobData=response.items
      })
    },
    fetchData(id) {
      this.orgs = [];
      this.getOrgNodes(id);
      this.getJobs()
      this.$axios.gets("/api/business/employee/" + id).then((response) => {
        this.form = response;
        if (response.userId) {
          this.getUser(response.userId);
        }
      });
    },
    loadOrgs({ action, parentNode, callback }) {
      if (action === LOAD_CHILDREN_OPTIONS) {
        this.$axios
          .gets("/api/business/orgs/loadOrgs", { id: parentNode.id })
          .then((response) => {
            parentNode.children = response.items.map(function (obj) {
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
    //TODO：引用公共方法
    loadTree(data) {
      data.items.forEach((element) => {
        if (!element.pid) {
          element.hasChildren = element.leaf ? false : true;
          if (!element.leaf) {
            element.children = [];
          }
          this.orgs.push(element);
        }
      });
      this.setChildren(this.orgs, data.items);
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
    getSupOrgs(id) {
      this.$axios
        .gets("/api/business/orgs/parents", { id: id })
        .then((response) => {
          this.orgs = response.items.map(function (obj) {
            obj.label = obj.name;
            if (obj.hasChildren) {
              obj.children = null;
            }
            return obj;
          });
        });
    },
    getUserList() {
      this.userListLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios
        .gets("/api/identity/users", this.listQuery)
        .then((response) => {
          this.userList = response.items;
          this.userTotalCount = response.totalCount;
          this.userListLoading = false;
        });
    },
    getUser(id) {
      this.$axios.gets("/api/identity/users/" + id).then((response) => {
        this.form.userIdToName = response.userName;
      });
    },
    userHandleFilter() {
      this.page = 1;
      this.getUserList();
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleSelectUser() {
      this.listQuery.Filter = "";
      this.userDialogTableVisible = true;
      this.getUserList();
    },
    handleNodeClick(data) {
      this.listQuery.OrgId = data.id;
      this.getList();
    },
    save() {
      this.$refs.form.validate((valid) => {
        if (valid) {
          if (!this.form.orgId) {
            this.$message({
              message: "所属机构不能为空",
              type: "warning",
            });
            return;
          }
          this.formLoading = true;
          this.form.roleNames = this.checkedRole;
          if (this.isEdit) {
            this.$axios
              .puts("/api/business/employee/" + this.form.id, this.form)
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
              .posts("/api/business/employee", this.form)
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
      this.formTitle = "新增职员";
      this.isEdit = false;
      this.dialogFormVisible = true;
      this.getJobs()
      this.$axios.gets("/api/business/orgs/loadOrgs").then((response) => {
        this.orgs = response.items.map(function (obj) {
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
          this.$axios
            .posts("/api/business/employee/delete", params)
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
      this.formTitle = "修改职员";
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
    userHandleRowClick(row, column, event) {
      this.userDialogTableVisible = false;
      this.form.userId = row.id;
      this.form.userIdToName = row.name;
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
            .puts("/api/business/employee/" + data.id, data)
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