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
    <!--工具栏-->
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
      <div class="opts">
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
        <!-- <el-button
          slot="reference"
          class="filter-item"
          type="danger"
          icon="el-icon-delete"
          size="mini"
          v-permission="['AbpIdentity.Users.Delete']"
          @click="handleDelete()"
        >删除</el-button> -->
      </div>
    </div>
    <!--表单渲染-->
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
        label-width="70px"
      >
        <el-form-item label="用户名" prop="userName">
          <el-input v-model="form.userName" style="width: 184px;" />
        </el-form-item>
        <el-form-item label="电话" prop="phoneNumber">
          <el-input v-model.number="form.phoneNumber" style="width: 184px;" />
        </el-form-item>
        <el-form-item label="姓名" prop="name">
          <el-input v-model="form.name" style="width: 184px;" />
        </el-form-item>
        <el-form-item label="邮箱" prop="email">
          <el-input v-model="form.email" style="width: 184px;" />
        </el-form-item>
        <el-form-item label="密码" prop="password" v-if="!isEdit">
          <el-input type="password" v-model="form.password" style="width: 184px;" />
        </el-form-item>
        <el-form-item label="角色" prop="roles">
          <el-select v-model="checkedRole" multiple style="width: 188px" placeholder="请选择">
            <el-option
              v-for="item in roleList"
              :key="item.name"
              :label="item.name"
              :value="item.name"
            ></el-option>
          </el-select>
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
            <el-form-item label="岗位" prop="job">
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
        <el-form-item label="允许锁定">
          <el-radio-group v-model="form.lockoutEnabled" style="width: 178px">
		    <el-radio :label="true">是</el-radio>
            <el-radio :label="false">否</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button type="text" @click="cancel">取消</el-button>
        <el-button v-loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>
    <!--表格渲染-->
    <el-table
      ref="multipleTable"
      v-loading="listLoading"
      :data="list"
      size="small"
      style="width: 100%;"
      @sort-change="sortChange"
      @selection-change="handleSelectionChange"
      @row-click="handleRowClick"
    >
      <el-table-column type="selection" width="44px"></el-table-column>
      <el-table-column label="用户名" prop="userName" sortable="custom" align="center" width="150px">
        <template slot-scope="{row}">
          <span class="link-type" @click="handleUpdate(row)">{{row.userName}}</span>
        </template>
      </el-table-column>
      <el-table-column label="所属机构" prop="orgIdToName" align="center">
            <template slot-scope="scope">
              <span>{{scope.row.orgIdToName}}</span>
            </template>
          </el-table-column>
      <el-table-column label="邮箱" prop="email" sortable="custom" align="center" width="200px">
        <template slot-scope="scope">
          <span>{{scope.row.email}}</span>
        </template>
      </el-table-column>
      <el-table-column label="电话" prop="phoneNumber" sortable="custom" align="center" width="200px">
        <template slot-scope="scope">
          <span>{{scope.row.phoneNumber}}</span>
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
  id:undefined,
  orgId:undefined,
  userName:'',
  phoneNumber:'',
  name:'',
  email:'',
  password:'',
  lockoutEnabled:false,
  roleNames:[],
  jobs:[],
  orgIdToName: null,
}

export default {
  name: "User",
  components: { Pagination, Treeselect },
  directives: { permission },
  data() {
    // 自定义验证
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
        userName: [
          { required: true, message: "请输入用户名", trigger: "blur" },
          { min: 2, max: 20, message: "长度在 2 到 20 个字符", trigger: "blur" }
        ],
        name: [
          { required: true, message: "请输入用户姓名", trigger: "blur" },
          { min: 2, max: 20, message: "长度在 2 到 20 个字符", trigger: "blur" }
        ],
        email: [
          { required: true, message: "请输入邮箱地址", trigger: "blur" },
          { type: "email", message: "请输入正确的邮箱地址", trigger: "blur" }
        ],
        phoneNumber: [
          { required: true, trigger: "blur", validator: validPhone }
        ]
      },
      defaultProps: { children: "children", label: "name", isLeaf: "leaf" },
      form: Object.assign({}, defaultForm),
      list: null,
      orgName: "",
      orgs: [],
      jobData:[],
      orgData: [],
      roleList: [],
      checkedRole: [],
      totalCount: 0,
      listLoading: true,
      formLoading: false,
      listQuery: {
        OrgId: null,
        Filter: "",
        Sorting: "",
        SkipCount: 0,
        MaxResultCount: 10
      },
      page: 1,
      dialogFormVisible: false,
      multipleSelection: [],
      formTitle: "",
      isEdit: false
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
          .gets("/api/base/orgs/loadOrgs", params)
          .then((response) => {
            if (resolve) {
              resolve(response.items);
            } else {
              this.orgData = response.items;
            }
          });
      }, 100);
    },
    getOrgNodes() {
      this.$axios.gets("/api/base/orgs/loadNodes").then((response) => {
        this.loadTree(response);
      });
    },
    getJobs(){
      this.$axios.gets('/api/base/job/jobs').then((response)=>{
        this.jobData=response.items
      })
    },
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios.gets("/api/base/user", this.listQuery).then(response => {
        this.list = response.items;
        this.totalCount = response.totalCount;
        this.listLoading = false;
      });
    },
    fetchData(id) {
      this.getAllRoles();
      this.getOrgNodes();
      this.getJobs()
      this.$axios.gets("/api/base/user/" + id).then(response => {
        this.form = response;
      });
      this.$axios.gets("/api/identity/users/" + id + "/roles").then(data => {
        
          data.items.forEach(item=>{
            this.checkedRole.push(item.name)
          })
        });
    },
    loadOrgs({ action, parentNode, callback }) {
      if (action === LOAD_CHILDREN_OPTIONS) {
        this.$axios
          .gets("/api/base/orgs/loadOrgs", { id: parentNode.id })
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
    getAllRoles() {
      this.$axios.gets("/api/identity/roles/all").then(response => {
        this.roleList=response.items
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleNodeClick(data) {
      this.listQuery.OrgId = data.id;
      this.getList();
    },
    save() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.formLoading = true;
          this.form.roleNames=this.checkedRole
          if (this.isEdit) {
            this.$axios
              .puts("/api/base/user/" + this.form.id, this.form)
              .then(response => {
                this.formLoading = false;
                this.$notify({
                  title: "成功",
                  message: "更新成功",
                  type: "success",
                  duration: 2000
                });
                this.dialogFormVisible = false;
                this.getList();
              })
              .catch(() => {
                this.formLoading = false;
              });
          } else {
            this.$axios
              .posts("/api/base/user", this.form)
              .then(response => {
                this.formLoading = false;
                this.$notify({
                  title: "成功",
                  message: "新增成功",
                  type: "success",
                  duration: 2000
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
      this.formTitle = "新增用户";
      this.isEdit = false;
      this.dialogFormVisible = true;
      this.getAllRoles();
      this.getJobs()
      this.getOrgNodes();
      
    },
    handleDelete(row) {
      if (row) {
        this.$confirm("是否删除" + row.name + "?", "提示", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning"
        })
          .then(() => {
            this.$axios
              .deletes("/api/identity/users/" + row.id)
              .then(response => {
                const index = this.list.indexOf(row);
                this.list.splice(index, 1);
                this.$notify({
                  title: "成功",
                  message: "删除成功",
                  type: "success",
                  duration: 2000
                });
              });
          })
          .catch(() => {
            this.$message({
              type: "info",
              message: "已取消删除"
            });
          });
      } else {
        this.$alert("暂时不支持用户批量删除", "提示", {
          confirmButtonText: "确定",
          callback: action => {
            //
          }
        });
      }
    },
    handleUpdate(row) {
      this.formTitle = "修改用户";
      this.isEdit = true;

      if (row) {
        this.fetchData(row.id);
        this.dialogFormVisible = true;
      } else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: "编辑必须选择单行",
            type: "warning"
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
      this.form = Object.assign({}, defaultForm),
      this.checkedRole=[]
      this.orgs=[]
      this.jobData=[]
      this.dialogFormVisible = false;
      this.$refs.form.clearValidate();
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
  }
};
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.opts {
  padding: 6px 0;
  display: -webkit-flex;
  display: flex;
  align-items: center;
}
</style>
