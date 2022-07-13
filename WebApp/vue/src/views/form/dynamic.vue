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
          class="filter-item"
          size="mini"
          type="primary"
          icon="el-icon-plus"
          @click="handleCreate()"
          >新增</el-button
        >
        <el-button
          class="filter-item"
          size="mini"
          type="success"
          icon="el-icon-edit"
          @click="handleUpdate()"
          >修改</el-button
        >
        <el-button
          slot="reference"
          class="filter-item"
          type="danger"
          icon="el-icon-delete"
          size="mini"
          @click="handleDelete()"
          >删除</el-button
        >
      </div>
    </div>
    <el-dialog
      :visible.sync="dialogFormVisible"
      :close-on-click-modal="false"
      :title="formTitle"
      @close="cancel()"
      width="500px"
    >
      <el-form ref="form" :inline="true" size="small" label-width="66px">
        <div v-for="(item, index) in data" :key="index">
          <el-form-item :label="item.label">
            <el-input :type="item.fieldType" v-model="item.value" />
          </el-form-item>
        </div>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button
          size="small"
          v-loading="formLoading"
          type="primary"
          @click="save"
          >确认</el-button
        >
      </div>
    </el-dialog>

    <el-table
      ref="multipleTable"
      v-loading="listLoading"
      :data="list"
      size="small"
      style="width: 100%"
      empty-text="空"
      @sort-change="sortChange"
      @selection-change="handleSelectionChange"
      @row-click="handleRowClick"
    >
      <el-table-column type="selection" width="44px"></el-table-column>
      <el-table-column
        v-for="(item, index) in form.fields"
        :key="index"
        :prop="item.fieldName"
        :label="item.label"
      >
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
import lodash from "lodash";

export default {
  name: "Dynamic",
  components: { Pagination },
  data() {
    return {
      formId: "39b45d6b-83e5-4a01-9f2b-e861b900db20",
      rules: {
        name: [{ required: true, message: "请输入岗位名", trigger: "blur" }],
      },
      form: {},
      data: [],
      list: null,
      totalCount: 0,
      listLoading: true,
      formLoading: false,
      drawingList: [],
      tableData: [
        {
          id: 1,
          name: "水浒传",
          price: 137597.76,
          const: 102203.71,
          profit: 35394.05,
        },
        {
          id: 2,
          name: "三国演义",
          price: 137597.76,
          const: 102203.71,
          profit: 35394.05,
        },
      ],
      listQuery: {
        FormId: null,
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
    this.getForm();
  },
  methods: {
    getForm() {
      this.$axios
        .gets("http://localhost:62162/api/business/form/" + this.formId)
        .then((response) => {
          this.form = response;
          this.data = response.fields
          this.getList();
        });
    },
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount =
        (this.page - 1) * this.listQuery.MaxResultCount;
      this.listQuery.formId = this.formId;
      this.$axios
        .gets("/api/business/form-data", this.listQuery)
        .then((response) => {
          this.list = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
      this.listLoading = false;
    },
    fetchData(id) {
      this.$axios.gets("/api/business/form-data/" + id).then(response => {
        this.form.fields.forEach(element=>{Object.keys(response).filter(key => {if(key===element.fieldName){element.value=response[key]}})})
        this.data=lodash.cloneDeep(this.form.fields);
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
            .posts("/api/business/form-data/delete", params)
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
      this.formTitle = "新增" + this.form.displayName;
      this.isEdit = false;
      this.dialogFormVisible = true;
    },
    handleUpdate(row) {
      this.formTitle = "修改" + this.form.displayName;
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
      //this.form = Object.assign({}, defaultForm);
      this.dialogFormVisible = false;
      //this.$refs.form.clearValidate();
    },
    save() {
      this.formLoading = true;
      if (this.isEdit) {
        this.$axios
          .puts("/api/business/form-data/" + this.multipleSelection[0].id, {
            formId: this.formId,
            data: this.data,
          })
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
          .posts("/api/business/form-data", {
            formId: this.formId,
            data: this.form.fields,
          })
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
    },
  },
};
</script>