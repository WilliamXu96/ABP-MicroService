<template>
  <div class="app-container">
    <el-dialog
      :visible.sync="dialogFormVisible"
      :close-on-click-modal="false"
      :title="formTitle"
      width="500px"
    >
      <el-form ref="form" :model="form" :rules="rules" size="small" label-width="80px">
        <el-form-item label="字典名称" prop="name">
          <el-input v-model="form.name" style="width: 370px;" />
        </el-form-item>
        <el-form-item label="描述">
          <el-input v-model="form.description" style="width: 370px;" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button type="text" @click="cancel">取消</el-button>
        <el-button :loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>

    <el-row :gutter="10">
      <el-col :xs="24" :sm="24" :md="10" :lg="11" :xl="11" style="margin-bottom: 10px">
        <el-card class="box-card">
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
            <div class="opts">
              <el-button
                class="filter-item"
                size="mini"
                type="primary"
                icon="el-icon-plus"
                @click="handleCreate"
                v-permission="['BaseService.DataDictionary.Create']"
              >新增</el-button>
              <el-button
                class="filter-item"
                size="mini"
                type="success"
                icon="el-icon-edit"
                v-permission="['BaseService.DataDictionary.Update']"
                @click="handleUpdate()"
              >修改</el-button>
              <el-button
                slot="reference"
                class="filter-item"
                type="danger"
                icon="el-icon-delete"
                size="mini"
                v-permission="['BaseService.DataDictionary.Delete']"
                @click="handleDelete()"
              >删除</el-button>
            </div>
          </div>
          <el-table
            ref="multipleTable"
            v-loading="listLoading"
            :data="list"
            size="small"
            style="width: 100%;"
            @selection-change="handleSelectionChange"
            @row-click="handleRowClick"
          >
            <el-table-column type="selection" width="55" />
            <el-table-column :show-overflow-tooltip="true" prop="name" label="名称" />
            <el-table-column :show-overflow-tooltip="true" prop="description" label="描述" />
            <el-table-column label="操作" align="center" width="125">
              <template slot-scope="{row}">
                <el-button
                  type="primary"
                  size="mini"
                  @click="handleUpdate(row)"
                  v-permission="['BaseService.DataDictionary.Update']"
                  icon="el-icon-edit"
                />
                <el-button
                  type="danger"
                  size="mini"
                  @click="handleDelete(row)"
                  v-permission="['BaseService.DataDictionary.Delete']"
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
        </el-card>
      </el-col>
      <el-col :xs="24" :sm="24" :md="14" :lg="13" :xl="13">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <span>字典详情</span>
            <el-button
              class="filter-item"
              size="mini"
              style="float: right;padding: 4px 10px"
              type="primary"
              icon="el-icon-plus"
              :disabled="multipleSelection.length != 1"
              @click="handleCreateDetail"
              v-permission="['BaseService.DataDictionary.Create']"
            >新增</el-button>
          </div>
          <dictDetail ref="dictDetail" />
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { isvalidPhone } from "@/utils/validate";
import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";
import dictDetail from './dictDetail'

export default {
  name: "Dictionary",
  components: { Pagination, dictDetail },
  directives: { permission },
  data() {
    return {
      rules: {
        name: [{ required: true, message: "请输入名称", trigger: "blur" }]
      },
      form: {},
      list: null,
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
      isEdit: false,
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
        .gets("/api/base/dict/all", this.listQuery)
        .then(response => {
          this.list = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
    },
    fetchData(id) {
      this.$axios.gets("/api/base/dict/" + id).then(response => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    resetQuery() {},
    save() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.formLoading = true;
          if (this.isEdit) {
            this.$axios
              .puts("/api/base/dict/" + this.form.id, this.form)
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
              .posts("/api/base/dict", this.form)
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
      this.formTitle = "新增字典";
      this.isEdit = false;
      this.form = {};
      this.dialogFormVisible = true;
    },
    handleDelete(row) {
      var params = [];
      let alert=""
      if (row) {
        params.push(row.id);
        alert=row.name
      } else {
        if (this.multipleSelection.length === 0) {
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
        alert="选中项"
      }
      this.$confirm("是否删除" + alert + "?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.$axios.posts("/api/base/dict/delete", params).then(response => {
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
      this.formTitle = "修改字典";
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
      this.$refs.dictDetail.listQuery.Pid = row.id
      this.$refs.dictDetail.getList()
    },
    handleCreateDetail(){
      this.$refs.dictDetail.dialogFormVisible=true
      this.$refs.dictDetail.isEdit=false
      this.$refs.dictDetail.form={}
      this.$refs.dictDetail.formTitle="新增字典详情"
    },
    cancel(){
      this.dialogFormVisible=false
      this.$refs.form.clearValidate()
    }
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