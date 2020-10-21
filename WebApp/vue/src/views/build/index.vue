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
          type="primary"
          icon="el-icon-download"
          size="mini"
          @click="handleGen"
        >配置表单</el-button>
        <el-button
          type="primary"
          icon="el-icon-download"
          size="mini"
          @click="handleGen"
        >生成代码</el-button>
      </div>
    </div>
    <el-table
      ref="multipleTable"
      v-loading="listLoading"
      :data="list"
      size="small"
      style="width: 90%"
      @sort-change="sortChange"
      @selection-change="handleSelectionChange"
      @row-click="handleRowClick"
    >
      <el-table-column type="selection" width="44px"></el-table-column>
      <el-table-column label="表单名称" prop="formName" align="center" />
      <el-table-column label="实体名称" prop="entityName" align="center" />
      <el-table-column label="表名称" prop="tabelName" align="center" />
      <el-table-column label="表描述" prop="remark" align="center" />
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
import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";

const defaultForm = {
  id: null,
  name: null,
  description: null,
  sort: 999,
  disabled: true,
};
export default {
  name: "Build",
  components: { Pagination },
  directives: { permission },
  filters: {
    displayStatus(status) {
      const statusMap = {
        true: "是",
        false: "否",
      };
      return statusMap[status];
    },
  },
  data() {
    return {
      rules: {
        name: [{ required: true, message: "请输入岗位名", trigger: "blur" }],
      },
      form: Object.assign({}, defaultForm),
      list: null,
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
      this.listQuery.SkipCount = (this.page - 1) * 10;
      this.$axios
        .gets("/api/business/form", this.listQuery)
        .then((response) => {
          this.list = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
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
            .posts("/api/business/form/delete", params)
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
    handleCreate() {
      this.$router.push({ path: "/tool/formCreate" });
    },
    handleGen(){
      if (this.multipleSelection.length != 1) {
          this.$message({
            message: "代码生成必须选择单行",
            type: "warning",
          });
          return;
        } else {
          this.$router.push({
            path: "/tool/buildEdit/" + this.multipleSelection[0].id,
          });
        }
    },
    handleUpdate(row) {
      if (row) {
        this.$router.push({ path: "/tool/formEdit/" + row.id });
      } else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: "编辑必须选择单行",
            type: "warning",
          });
          return;
        } else {
          this.$router.push({
            path: "/tool/formEdit/" + this.multipleSelection[0].id,
          });
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
  },
};
</script>