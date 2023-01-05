<template>
  <div class="app-container">
    <div class="filter-container">
      <div class="filter-left">
        <el-input v-model="listQuery.Filter" placeholder="搜索..." style="width: 200px" size="small" class="filter-item"
          @keyup.enter.native="handleFilter" />
        <el-button size="mini" class="filter-item" type="primary" icon="el-icon-search"
          @click="handleFilter">搜索</el-button>
      </div>
      <div class="filter-right">
        <router-link :to="'/tool/flowCreate/'">
          <el-button size="mini" class="filter-item" style="margin-left: 10px" type="primary"
            icon="el-icon-plus">新增</el-button>
        </router-link>
        <el-button size="mini" class="filter-item" style="margin-left: 10px" type="success" icon="el-icon-edit"
          @click="handleUpdate()">编辑</el-button>
        <el-button size="mini" class="filter-item" type="danger" icon="el-icon-delete" style="margin-left: 10px"
          @click="handleDelete()">删除</el-button>
      </div>
    </div>
    <el-table ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="width: 90%"
      @sort-change="sortChange" @selection-change="handleSelectionChange" @row-click="handleRowClick">
      <el-table-column type="selection" width="44px"></el-table-column>
      <el-table-column label="流程编号" prop="code" sortable="custom" align="center" width="150px">
        <template slot-scope="{ row }">
          <span class="link-type" @click="handleUpdate(row)">{{
              row.code
          }}</span>
        </template>
      </el-table-column>
      <el-table-column label="流程标题" prop="title" align="center" />
      <el-table-column label="关联表单" prop="formName" align="center" />
      <el-table-column label="启用日期" sortable="custom" prop="useDate" align="center" />
      <el-table-column label="级别" sortable="custom" prop="level" align="center">
        <!-- <template slot-scope="scope">
          <span>{{ scope.row.level | displayStatus }}</span>
        </template> -->
      </el-table-column>
      <el-table-column label="操作" align="center">
        <template slot-scope="{ row }">
          <el-button type="text" size="mini" @click="handleUpdate(row)" icon="el-icon-edit">修改</el-button>
          <el-button type="text" size="mini" @click="handleDelete(row)" v-permission="['BaseService.Job.Delete']"
            icon="el-icon-delete">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="totalCount > 0" :total="totalCount" :page.sync="page" :limit.sync="listQuery.MaxResultCount"
      @pagination="getList" />
  </div>
</template>

<script>
import waves from "@/directive/waves";
import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";

export default {
  name: "Flow",
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
        MaxResultCount: 20,
      },
      page: 1,
      multipleSelection: [],
    };
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount =
        (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios
        .gets("/api/business/flow", this.listQuery)
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
    handleUpdate(row) {
      if (row) {
        this.$router.push({ path: "/tool/flowEdit/" + row.id });
      } else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: "编辑必须选择单行",
            type: "warning",
          });
          return;
        } else {
          this.$router.push({
            path: "/tool/flowEdit/" + this.multipleSelection[0].id,
          });
        }
      }
    },
    handleDelete() {
      if (this.multipleSelection.length < 1) {
        this.$message({
          message: "删除至少选择一行数据",
          type: "warning",
        });
        return;
      } else {
        this.$confirm("是否删除?", "提示", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning",
        })
          .then(() => {
            var params = [];
            this.multipleSelection.forEach((element) => {
              const id = element.id;
              params.push(id);
            });
            this.$axios
              .posts("/api/business/flow/delete", params)
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
