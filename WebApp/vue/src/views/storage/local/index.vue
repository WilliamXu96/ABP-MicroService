<template>
  <div class="app-container" style="padding: 8px;">
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
          icon="el-icon-upload"
          @click="handleUpload"
        >上传</el-button>
        <el-button
          slot="reference"
          class="filter-item"
          type="danger"
          icon="el-icon-delete"
          size="mini"
          @click="handleDelete()"
        >删除</el-button>
      </div>
    </div>
    <el-dialog
      :visible.sync="dialogFormVisible"
      :close-on-click-modal="false"
      :title="formTitle"
      @close="cancel()"
      width="500px"
    >
      <el-form
        ref="form"
        :inline="true"
        :model="form"
        :rules="rules"
        size="small"
        label-width="66px"
      >
        <el-form-item label="文件名" prop="name">
          <el-input v-model="form.name" style="width: 370px;" />
        </el-form-item>
        <el-form-item label="上传">
          <el-upload
            ref="upload"
            :limit="1"
            :before-upload="beforeUpload"
            :auto-upload="false"
            :on-success="handleSuccess"
            :on-error="handleError"
            :action="storageApi + '/upload?name=' + form.name"
          >
            <div class="upload">
              <i class="el-icon-upload" /> 添加文件
            </div>
            <div slot="tip" class="el-upload__tip">上传文件不超过100M</div>
          </el-upload>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="upload">确认</el-button>
      </div>
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
      <el-table-column label="文件名" prop="name" sortable="custom" align="center" width="150px">
        <template slot-scope="scope">
          <el-popover
            :content="storageApi + scope.row.url"
            placement="top-start"
            title="路径"
            width="200"
            trigger="hover"
          >
            <a
              slot="reference"
              :href="storageApi + scope.row.url"
              class="el-link--primary"
              style="word-break:keep-all;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;color: #1890ff;font-size: 13px;"
              target="_blank"
            >{{ scope.row.name }}</a>
          </el-popover>
        </template>
      </el-table-column>
      <el-table-column label="预览图" prop="url" align="center">
        <template slot-scope="{row}">
          <el-image
            :src="storageApi + row.url"
            :preview-src-list="[storageApi + row.url]"
            fit="contain"
            lazy
            class="el-avatar"
          >
            <div slot="error">
              <i class="el-icon-document" />
            </div>
          </el-image>
        </template>
      </el-table-column>
      <el-table-column label="文件格式" prop="suffix" align="center" />
      <el-table-column label="类别" prop="type" align="center">
        <template slot-scope="scope">
          <span>{{scope.row.type | displayType}}</span>
        </template>
      </el-table-column>
      <el-table-column label="大小" prop="size" align="center" />
      <el-table-column label="创建日期" prop="creationTime" align="center">
        <template slot-scope="scope">
          <span>{{scope.row.creationTime | formatDate}}</span>
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
import config from "../../../../static/config";

const defaultForm = {
  id: null,
  name: null,
};
export default {
  name: "Storage",
  components: { Pagination },
  directives: { permission },
  filters: {
    displayType(typeId) {
      const typeMap = {
        0: "图片",
        1: "文件",
      };
      return typeMap[typeId];
    },
  },
  data() {
    return {
      storageApi: config.storage.ip,
      rules: {
        name: [{ required: true, message: "请输入文件名", trigger: "blur" }],
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
    // 上传文件
    upload() {
      this.$refs.upload.submit();
    },
    beforeUpload(file) {
      let isLt2M = true;
      isLt2M = file.size / 1024 / 1024 < 100;
      if (!isLt2M) {
        this.loading = false;
        this.$message.error("上传文件大小不能超过 100MB!");
      }
      this.form.name = file.name;
      return isLt2M;
    },
    handleSuccess(response, file, fileList) {
      this.$notify({
        title: "成功",
        message: "更新成功",
        type: "success",
        duration: 2000,
      });
      this.dialogFormVisible = false;
      this.getList();
    },
    // 监听上传失败
    handleError(e, file, fileList) {
      this.$notify({
        title: e,
        type: "error",
        duration: 4000,
      });
      this.loading = false;
    },
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios
        .gets(config.storage.ip + "/api/app/file", this.listQuery)
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
    handleUpload() {
      this.formTitle = "文件上传";
      this.isEdit = false;
      this.dialogFormVisible = true;
    },
    handleDelete(row) {
      this.$message({
        message: "敬请期待！",
        type: "warning",
      });
      return;
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
  },
};
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.upload {
  border: 1px dashed #c0ccda;
  border-radius: 5px;
  height: 45px;
  line-height: 45px;
  width: 368px;
}
</style>