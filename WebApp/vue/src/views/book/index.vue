<template>
  <div class="app-container">
    <div class="head-container">
      <!-- 搜索 -->
      <el-input v-model="listQuery.Filter" clearable size="small" placeholder="搜索..." style="width: 200px;"
        class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button class="filter-item" size="mini" type="success" icon="el-icon-search" @click="handleFilter">搜索
      </el-button>
      <div style="padding: 6px 0">
        <el-button class="filter-item" size="mini" type="primary" icon="el-icon-plus" @click="handleCreate">新增
        </el-button>
        <el-button class="filter-item" size="mini" type="success" icon="el-icon-edit" @click="handleUpdate()">修改
        </el-button>
        <el-button slot="reference" class="filter-item" type="danger" icon="el-icon-delete" size="mini"
          @click="handleDelete()">删除</el-button>
        <el-button class="filter-item" type="warning" icon="el-icon-s-check" size="mini"
          @click="handleCheck()">审核</el-button>
      </div>
    </div>
    <el-dialog :visible.sync="dialogFormVisible" :close-on-click-modal="false" @close="cancel()" :title="formTitle">
      <el-form ref="form" :model="form" :rules="rules" size="medium" label-width="100px">
        <el-form-item label="名称" prop="name">
          <el-input v-model="form.name" placeholder="请输入名称" clearable :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="描述" prop="description">
          <el-input v-model="form.description" placeholder="请输入描述" clearable :style="{ width: '100%' }">
          </el-input>
        </el-form-item>
        <el-form-item label="价格" prop="price">
          <el-input-number v-model="form.price" placeholder="价格"></el-input-number>
        </el-form-item>
      </el-form>
      <div slot="footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>
    <el-table ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="width: 90%;"
      @sort-change="sortChange" @selection-change="handleSelectionChange" @row-click="handleRowClick">
      <el-table-column type="selection" width="44px"></el-table-column>
      <el-table-column label="名称" prop="name" align="center" />
      <el-table-column label="描述" prop="description" align="center" />
      <el-table-column label="价格" prop="price" align="center" />
      <el-table-column label="状态" prop="status" align="center">
        <template slot-scope="{row}">
          <el-tag :type="row.status | workflowStatusFilter">{{ row.status | displayWorkflowStatus }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="流程节点" prop="nodeStatus" align="center">
        <template slot-scope="{row}">
          <span class="link-type" @click="findFlow(row)">{{ row.nodeStatus }}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作" align="center">
        <template slot-scope="{row}">
          <el-button type="text" size="mini" @click="handleUpdate(row)" icon="el-icon-edit">修改</el-button>
          <el-button type="text" size="mini" @click="handleDelete(row)" icon="el-icon-delete">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination v-show="totalCount > 0" :total="totalCount" :page.sync="page" :limit.sync="listQuery.MaxResultCount"
      @pagination="getList" />
  </div>
</template>
<script>
import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";
import { cloneDeep } from "lodash";

const defaultForm = {
  id: null,
  name: null,
  description: null,
  price: 0,
  status: 0
}
export default {
  name: 'Book',
  components: {
    Pagination
  },
  directives: {
    permission
  },
  props: [],
  data() {
    return {
      rules: {
        name: [{
          required: true,
          message: '请输入名称',
          trigger: 'blur'
        }],
        description: [{
          required: true,
          message: '请输入描述',
          trigger: 'blur'
        }],
        price: [{
          required: true,
          message: '价格',
          trigger: 'blur'
        }],
      },
      form: Object.assign({}, defaultForm),
      list: [],
      totalCount: 0,
      listLoading: true,
      formLoading: false,
      listQuery: {
        Filter: '',
        Sorting: '',
        SkipCount: 0,
        MaxResultCount: 10
      },
      page: 1,
      dialogFormVisible: false,
      multipleSelection: [],
      formTitle: '',
      isEdit: false,
    }
  },
  computed: {},
  watch: {},
  created() {
    this.getList()
  },
  mounted() { },
  methods: {
    getListStatus(items, ids) {
      this.$axios.posts('api/business/workflow/node-status', ids).then(response => {
        items.forEach(element => { response.items.filter((i) => { if (i.entityId === element.id) { element.status = i.status; element.nodeId = i.nodeId; element.nodeStatus = i.nodeName } }) })
        this.list = items
        this.listLoading = false;
      });
    },
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios.gets('/api/business/book', this.listQuery).then(response => {
        this.totalCount = response.totalCount;
        this.getListStatus(response.items, response.items.map(_ => _.id))
      });
    },
    fetchData(id) {
      this.$axios.gets('/api/business/book/' + id).then(response => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleCreate() {
      this.formTitle = '新增图书';
      this.isEdit = false;
      this.dialogFormVisible = true;
    },
    handleDelete(row) {
      var params = [];
      let alert = '';
      if (row) {
        params.push(row.id);
        alert = row.name;
      }
      else {
        if (this.multipleSelection.length === 0) {
          this.$message({
            message: '未选择',
            type: 'warning'
          });
          return;
        }
        this.multipleSelection.forEach(element => {
          let id = element.id;
          params.push(id);
        });
        alert = '选中项';
      }
      this.$confirm('是否删除' + alert + '?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        this.$axios.posts('/api/business/book/delete', params).then(response => {
          this.$notify({
            title: '成功',
            message: '删除成功',
            type: 'success',
            duration: 2000
          });
          this.getList();
        });
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        });
      });
    },
    handleCheck() {
      if (this.multipleSelection.length === 0) {
        this.$message({
          message: '未选择',
          type: 'warning'
        });
        return;
      }
      if (this.multipleSelection.length > 1) {
        this.$message({
          message: '暂不支持批量审核',
          type: 'warning'
        });
        return;
      }
      this.$confirm('是否审核所选项?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        this.$axios.puts('api/business/workflow/do/' + this.multipleSelection[0].id, { data: JSON.stringify(this.multipleSelection[0]) }).then(response => {
          this.$notify({
            title: '成功',
            message: '审核成功',
            type: 'success',
            duration: 2000
          });
          this.getList();
        });
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消审核'
        });
      });
    },
    findFlow(row) {
      this.$router.push({ path: "/tool/flowDisplay/" + row.nodeId });
    },
    handleUpdate(row) {
      this.formTitle = '修改图书';
      this.isEdit = true;
      if (row) {
        this.fetchData(row.id);
        this.dialogFormVisible = true;
      }
      else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: '编辑必须选择单行',
            type: 'warning'
          });
          return;
        }
        else {
          this.fetchData(this.multipleSelection[0].id);
          this.dialogFormVisible = true;
        }
      }
    },
    save() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.formLoading = true;
          this.form.roleNames = this.checkedRole;
          if (this.isEdit) {
            this.$axios.posts('/api/business/book/data-post', this.form).then(response => {
              this.formLoading = false;
              this.$notify({
                title: '成功',
                message: '更新成功',
                type: 'success',
                duration: 2000
              });
              this.dialogFormVisible = false;
              this.getList();
            }).catch(() => {
              this.formLoading = false;
            });
          }
          else {
            this.$axios.posts('/api/business/book/data-post', this.form).then(response => {
              this.formLoading = false;
              this.$notify({
                title: '成功',
                message: '新增成功',
                type: 'success',
                duration: 2000
              });
              this.dialogFormVisible = false;
              this.getList();
            }).catch(() => {
              this.formLoading = false;
            });
          }
        }
      });
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data;
      if (!prop || !order) {
        this.handleFilter();
        return;
      }
      this.listQuery.Sorting = prop + ' ' + order;
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
  }
}

</script>
<style>

</style>
