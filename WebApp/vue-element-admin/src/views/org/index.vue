<template>
  <div class="app-container">
    <el-row :gutter="20">
      <!--侧边部门树形列表-->
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
          :data="orgDatas"
          :load="getOrgs"
          :props="defaultProps"
          :expand-on-click-node="false"
          lazy
          @node-click="handleNodeClick"
        />
      </el-col>
      <el-col :xs="15" :sm="18" :md="19" :lg="20" :xl="20">
        <div class="head-container">
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
              v-permission="['AbpIdentity.Roles.Create']"
            >新增</el-button>
            <el-button
              class="filter-item"
              size="mini"
              type="success"
              icon="el-icon-edit"
              v-permission="['AbpIdentity.Roles.Update']"
              @click="handleUpdate()"
            >修改</el-button>
            <el-button
              slot="reference"
              class="filter-item"
              type="danger"
              icon="el-icon-delete"
              size="mini"
              v-permission="['AbpIdentity.Roles.Delete']"
              @click="handleDelete()"
            >删除</el-button>
          </div>
        </div>
        <el-dialog
          :visible.sync="dialogFormVisible"
          :close-on-click-modal="false"
          :title="formTitle"
          width="580px"
        >
          <el-form
            ref="form"
            :inline="true"
            :model="form"
            :rules="rules"
            size="small"
            label-width="80px"
          >
            <el-form-item label="机构类型" prop="CategoryId">
              <el-input v-model="form.CategoryId" style="width: 380px;" />
            </el-form-item>
            <el-form-item label="机构编号" prop="code">
              <el-input v-model="form.code" style="width: 380px;" />
            </el-form-item>
            <el-form-item label="机构名称" prop="name">
              <el-input v-model="form.name" style="width: 380px;" />
            </el-form-item>
            <el-form-item label="机构排序" prop="sort">
              <el-input-number
                v-model.number="form.sort"
                :min="0"
                :max="999"
                controls-position="right"
                style="width: 370px;"
              />
            </el-form-item>
            <el-form-item label="行政地区" prop="areaId">
              <el-input v-model="form.areaId" style="width: 380px;" />
            </el-form-item>
            <el-form-item label="地址" prop="address">
              <el-input v-model="form.address" style="width: 380px;" />
            </el-form-item>
            <el-form-item label="电话" prop="tel">
              <el-input v-model="form.tel" style="width: 380px;" />
            </el-form-item>
            <el-form-item label="备注" prop="remark">
              <el-input v-model="form.remark" style="width: 380px;" />
            </el-form-item>
            <el-form-item label="顶级组织">
              <el-radio-group v-model="form.isTop" style="width: 140px">
                <el-radio label="0">是</el-radio>
                <el-radio label="1">否</el-radio>
              </el-radio-group>
            </el-form-item>
            <el-form-item label="状态" prop="enabled">
              <el-radio-group v-model="form.enable" style="width: 178px">
                <el-radio label="0">禁用</el-radio>
                <el-radio label="1">启用</el-radio>
              </el-radio-group>
            </el-form-item>
            <el-form-item
              v-if="form.isTop === '1'"
              style="margin-bottom: 0;"
              label="上级部门"
              prop="pid"
            >
              <!-- <treeselect
                v-model="form.pid"
                :load-options="loadOrgs"
                :options="orgs"
                style="width: 370px;"
                placeholder="选择上级类目"
              />-->
            </el-form-item>
          </el-form>
          <div slot="footer" class="dialog-footer">
            <el-button type="text" @click="cancel">取消</el-button>
            <el-button v-loading="formLoading" type="primary" @click="save">确认</el-button>
          </div>
        </el-dialog>
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
          <el-table-column
            label="机构类型"
            prop="categoryId"
            sortable="custom"
            align="center"
            width="100px"
          >
            <template slot-scope="scope">
              <span>{{scope.row.categoryId}}</span>
            </template>
          </el-table-column>
          <el-table-column label="机构编号" prop="code" sortable="custom" align="center" width="100px">
            <template slot-scope="scope">
              <span>{{scope.row.code}}</span>
            </template>
          </el-table-column>
          <el-table-column label="机构名称" prop="name" sortable="custom" align="center" width="100px">
            <template slot-scope="{row}">
              <span class="link-type" @click="handleUpdate(row)">{{row.name}}</span>
            </template>
          </el-table-column>
          <el-table-column label="全称" prop="fullName" align="center" width="100px">
            <template slot-scope="scope">
              <span>{{scope.row.fullName}}</span>
            </template>
          </el-table-column>
          <el-table-column label="排序" prop="sort" align="center" width="100px">
            <template slot-scope="scope">
              <span>{{scope.row.sort}}</span>
            </template>
          </el-table-column>
          <el-table-column label="行政地区" prop="areaId" align="center" width="150px">
            <template slot-scope="scope">
              <span>{{scope.row.areaId}}</span>
            </template>
          </el-table-column>
          <el-table-column label="联系电话" prop="tel" align="center" width="150px">
            <template slot-scope="scope">
              <span>{{scope.row.tel}}</span>
            </template>
          </el-table-column>
          <el-table-column label="地址" prop="address" align="center" width="150px">
            <template slot-scope="scope">
              <span>{{scope.row.address}}</span>
            </template>
          </el-table-column>
          <el-table-column label="状态" prop="enable" align="center" width="150px">
            <template slot-scope="scope">
              <span>{{scope.row.enable}}</span>
            </template>
          </el-table-column>
          <el-table-column label="操作" align="center" width="125">
            <template slot-scope="{row}">
              <el-button
                type="primary"
                size="mini"
                @click="handleUpdate(row)"
                v-permission="['AbpIdentity.Roles.Update']"
                icon="el-icon-edit"
              />
              <el-button
                type="danger"
                size="mini"
                @click="handleDelete(row)"
                :disabled="row.name==='admin'"
                v-permission="['AbpIdentity.Roles.Delete']"
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

export default {
  name: "Organization",
  components: { Pagination },
  directives: { permission },
  data() {
    return {
      rules: {
        name: [{ required: true, message: "请输入名称", trigger: "blur" }],
        code: [{ required: true, message: "请输入编号", trigger: "blur" }],
        sort: [{ required: true, message: "请输入序号", trigger: "blur" }]
      },
      defaultProps: { children: "children", label: "name", isLeaf: "leaf" },
      form: {},
      orgName: "",
      list: null,
      orgDatas: null,
      totalCount: 0,
      listLoading: true,
      formLoading: false,
      listQuery: {
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
      const params = {}
      if (typeof node !== "object") {
        if (node) {
          params["name"] = node;
        }
      } else if (node.level !== 0) {
        params["pid"] = node.data.id;
      }
      this.$axios
        .gets("/api/business/orgs/all", this.listQuery)
        .then(response => {
          this.list = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
    },
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * 10;
      this.$axios
        .gets("/api/business/orgs/all", this.listQuery)
        .then(response => {
          this.list = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
    },
    fetchData(id) {
      this.$axios.gets("/api/business/orgs/" + id).then(response => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    save() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.formLoading = true;
          if (this.isEdit) {
            this.$axios
              .puts("/api/business/orgs/" + this.form.id, this.form)
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
              .posts("/api/business/orgs", this.form)
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
      this.formTitle = "新增机构";
      this.isEdit = false;
      this.form = {};
      this.dialogFormVisible = true;
    },
    handleDelete(row) {
      var params = [];
      let alert = "";
      if (row) {
        params.push(row.id);
        alert = row.name;
      } else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: "未选择",
            type: "warning"
          });
          return;
        }
        this.multipleSelection.forEach(element => {
          let id = element.id;
          params.push(id);
        });
        alert = "选中项";
      }
      this.$confirm("是否删除" + alert + "?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.$axios
            .posts("/api​/business​/orgs​/delete", params)
            .then(response => {
              const index = this.list.indexOf(row);
              this.$notify({
                title: "成功",
                message: "删除成功",
                type: "success",
                duration: 2000
              });
              this.getList();
            });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除"
          });
        });
    },
    handleUpdate(row) {
      this.formTitle = "修改机构";
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
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    handleRowClick(row, column, event) {
      this.$refs.multipleTable.clearSelection();
      this.$refs.multipleTable.toggleRowSelection(row);
      this.$refs.dictDetail.listQuery.Pid = row.id;
      this.$refs.dictDetail.getList();
    },
    cancel() {
      this.dialogFormVisible = false;
      this.$refs.form.clearValidate();
    },
    handleNodeClick() {}
  }
};
</script>