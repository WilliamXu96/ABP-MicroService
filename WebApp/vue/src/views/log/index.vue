<template>
  <div class="app-container">
    <div class="filter-container">
        <el-input
          v-model="listQuery.UserName"
          placeholder="用户名"
          style="width: 150px;"
          size="small"
          class="filter-item"
          @keyup.enter.native="handleFilter"
        />
        <el-select class="filter-item" size="small" style="width: 130px" v-model="listQuery.HttpMethod" placeholder="Http方法">
            <el-option label="GET" value="GET"></el-option>
            <el-option label="POST" value="POST"></el-option>
            <el-option label="PUT" value="PUT"></el-option>
            <el-option label="DELETE" value="DELETE"></el-option>
        </el-select>
        <el-button class="filter-item" size="mini" type="success" icon="el-icon-search" @click="handleFilter">搜索</el-button>
    </div>

    <el-table ref="table" v-loading="listLoading" :data="list" @sort-change="sortChange" style="width: 100%;">
      <el-table-column type="expand">
        <template slot-scope="scope">
          <el-form label-position="left" inline class="table-expand" v-for="row in scope.row.actions" :key="row.id">
            <el-form-item label="服务名称">
              <span>{{ row.serviceName }}</span>
            </el-form-item>
            <el-form-item label="方法名称">
              <span>{{ row.methodName }}</span>
            </el-form-item>
            <el-form-item label="执行耗时">
              <span>{{ row.executionDuration }}ms</span>
            </el-form-item>
          </el-form>
        </template>
      </el-table-column>
      <el-table-column prop="userName" sortable="custom" label="用户名" />
      <el-table-column prop="clientIpAddress" label="IP" width="130px" />
      <el-table-column prop="clientId" label="clientId" />
      <el-table-column :show-overflow-tooltip="true" prop="browserInfo" label="浏览器" />
      <el-table-column :show-overflow-tooltip="true" prop="url" label="url" />
      <el-table-column prop="httpMethod" label="Http方法" align="center">
        <template slot-scope="scope">
          <el-tag v-if="scope.row.httpMethod == 'POST'" type="success">{{ scope.row.httpMethod }}</el-tag>
          <el-tag v-else-if="scope.row.httpMethod == 'GET'">{{ scope.row.httpMethod }}</el-tag>
          <el-tag v-else-if="scope.row.httpMethod == 'PUT'" type="warning">{{ scope.row.httpMethod }}</el-tag>
          <el-tag v-else-if="scope.row.httpMethod == 'DELETE'" type="danger">{{ scope.row.httpMethod }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="httpStatusCode" label="Http状态" align="center">
        <template slot-scope="scope">
          <el-tag v-if="scope.row.httpStatusCode < '300'" type="success">{{ scope.row.httpStatusCode }}</el-tag>
          <el-tag v-else-if="scope.row.httpStatusCode < '500'" type="warning">{{ scope.row.httpStatusCode }}</el-tag>
          <el-tag v-else-if="scope.row.httpStatusCode <= '600'" type="danger">{{ scope.row.httpStatusCode }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="executionTime" label="创建日期" width="180px">
        <template slot-scope="scope">
          <span>{{ scope.row.executionTime | formatDateTime }}</span>
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
import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";

export default {
  name: "Log",
  components: { Pagination },
  data() {
    return {
      list: null,
      totalCount: 0,
      listLoading: true,
      listQuery: {
        Url: null,
        UserName:null,
        ApplicationName:null,
        CorrelationId:null,
        HttpMethod:null,
        HttpStatusCode:null,
        MaxExecutionDuration:null,
        MinExecutionDuration:null,
        HasException:null,
        Sorting: null,
        SkipCount: 0,
        MaxResultCount: 10,
      },
      page: 1,
    };
  },
  created() {
    this.getList();
  },
  methods:{
      getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios
        .gets("/api/base/auditLogging/all", this.listQuery)
        .then(response => {
          response.items.forEach(element => {
            if (!element.lastModificationTime) {
              element.lastModificationTime = element.creationTime;
            }
          });
          this.list = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
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
  }
};
</script>

<style>
.table-expand {
  font-size: 0;
}
.table-expand label {
  width: 70px;
  color: #99a9bf;
}
.table-expand .el-form-item {
  margin-right: 0;
  margin-bottom: 0;
  width: 100%;
}
.table-expand .el-form-item__content {
  font-size: 12px;
}
</style>