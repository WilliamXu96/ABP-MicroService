<template>
  <div class="app-container">
    <div class="filter-container">
      <div class="filter-left">
        <el-input
          v-model="listQuery.Filter"
          placeholder="搜索..."
          style="width: 200px;"
          class="filter-item"
          @keyup.enter.native="handleFilter"
        />
        <el-button
          v-waves
          class="filter-item"
          type="primary"
          icon="el-icon-search"
          @click="handleFilter"
        >搜索</el-button>
      </div>
      <div class="filter-right">
        <router-link :to="'/tool/flowCreate/'">
          <el-button
            class="filter-item"
            style="margin-left: 10px;"
            type="primary"
            icon="el-icon-plus"
          >添加</el-button>
        </router-link>
        <el-button
          class="filter-item"
          style="margin-left: 10px;"
          type="success"
          icon="el-icon-edit"
          @click="handleUpdate()"
        >编辑</el-button>
        <el-button
          class="filter-item"
          type="danger"
          icon="el-icon-delete"
          style="margin-left: 10px;"
          @click="handleDelete()"
        >删除</el-button>
      </div>
    </div>
    <el-table
      ref="multipleTable"
      v-loading="listLoading"
      :data="list"
      border
      style="width: 100%;"
      height="500"
      @sort-change="sortChange"
      @selection-change="handleSelectionChange"
      @row-click="handleRowClick"
    >
      <el-table-column type="selection" width="40px" />
      <el-table-column label="编号" prop="Code" sortable="custom" align="center" width="150px">
        <template slot-scope="scope">
          <span>{{ scope.row.code }}</span>
        </template>
      </el-table-column>
      <el-table-column label="名称" prop="Name" sortable="custom" align="center" width="150px">
        <template slot-scope="{row}">
          <span class="link-type" @click="handleUpdate(row)">{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column label="备注" align="center" width="200px">
        <template slot-scope="scope">
          <span>{{ scope.row.notes }}</span>
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
  </div>
</template>

<script>
import waves from "@/directive/waves";
import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";

export default {
  name: "FlowDesign",
  components: { Pagination },
  directives: { waves, permission },
  data() {
    return {
      list: null,
      totalCount: 0,
      listLoading: true,
      listQuery: {
        Filter: "",
        Sorting: "",
        SkipCount: 0,
        MaxResultCount: 20
      },
      page: 1,
      multipleSelection: []
    };
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {},
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleUpdate(row) {
      if (row) {
        this.$router.push({ path: "/tool/flowEdit/" + row.id });
      } else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: "编辑必须选择单行",
            type: "warning"
          });
          return;
        } else {
          this.$router.push({
            path: "/tool/flowEdit/" + this.multipleSelection[0].id
          });
        }
      }
    },
    handleDelete() {
      if (this.multipleSelection.length < 1) {
        this.$message({
          message: "删除至少选择一行数据",
          type: "warning"
        });
        return;
      } else {
        this.$confirm("是否删除?", "提示", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning"
        })
          .then(() => {
            var params = [];
            this.multipleSelection.forEach(element => {
              const id = element.id;
              params.push(id);
            });
            this.$axios
              .posts("/api/basicData/material/delete", params)
              .then(response => {
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
    }
  }
};
</script>
