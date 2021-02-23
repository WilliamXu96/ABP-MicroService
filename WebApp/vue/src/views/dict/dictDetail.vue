<template>
  <div>
    <el-dialog
      :visible.sync="dialogFormVisible"
      :close-on-click-modal="false"
      @close="cancel()"
      :title="formTitle"
      width="500px"
    >
      <el-form ref="form" :model="form" :rules="rules" size="small" label-width="80px">
        <el-form-item label="字典标签" prop="label">
          <el-input v-model="form.label" style="width: 370px;" />
        </el-form-item>
        <el-form-item label="字典值" prop="value">
          <el-input v-model="form.value" style="width: 370px;" />
        </el-form-item>
        <el-form-item label="排序" prop="sort">
          <el-input-number v-model="form.sort" :min="0" :max="999" style="width: 370px;" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button type="text" @click="dialogFormVisible=false">取消</el-button>
        <el-button :loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>
    <el-table
      ref="multipleTable"
      v-loading="listLoading"
      :data="list"
      size="small"
      style="width: 100%;"
    >
      <el-table-column prop="label" label="字典标签" />
      <el-table-column prop="value" label="字典值" />
      <el-table-column label="排序" prop="sort" align="center">
        <template slot-scope="scope">
          <span>{{scope.row.sort}}</span>
        </template>
      </el-table-column>
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
  </div>
</template>
<script>
import permission from "@/directive/permission/index.js";
import Pagination from "@/components/Pagination";

const defaultForm = {
  id: null,
  label: null,
  value: null,
  sort: 999
};
export default {
  components: { Pagination },
  directives: { permission },
  data() {
    return {
      rules: {
        label: [{ required: true, message: "请输入字典标签", trigger: "blur" }],
        value: [{ required: true, message: "请输入字典值", trigger: "blur" }],
        sort: [
          {
            required: true,
            message: "请输入序号",
            trigger: "blur",
            type: "number"
          }
        ]
      },
      form: Object.assign({}, defaultForm),
      list: null,
      totalCount: 0,
      listLoading: false,
      formLoading: false,
      listQuery: {
        Pid: "",
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
  methods: {
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * 10;
      this.$axios
        .gets("/api/base/dictDetails/all", this.listQuery)
        .then(response => {
          this.list = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
    },
    fetchData(id) {
      this.$axios.gets("/api/base/dictDetails/" + id).then(response => {
        this.form = response;
      });
    },
    save() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.formLoading = true;
          this.form.pid = this.listQuery.Pid;
          if (this.isEdit) {
            this.$axios
              .puts("/api/base/dictDetails/" + this.form.id, this.form)
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
              .posts("/api/base/dictDetails", this.form)
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
    handleUpdate(row) {
      this.formTitle = "修改字典详情";
      this.isEdit = true;

      this.fetchData(row.id);
      this.dialogFormVisible = true;
    },
    handleDelete(row) {
      var params = [];
      let alert = "";
      if (row) {
        params.push(row.id);
        alert = row.label;
      } else {
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
            .posts("/api/base/dictDetails/Delete", params)
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
    cancel() {
      this.form = Object.assign({}, defaultForm);
      this.dialogFormVisible = false;
      this.$refs.form.clearValidate();
    }
  }
};
</script>