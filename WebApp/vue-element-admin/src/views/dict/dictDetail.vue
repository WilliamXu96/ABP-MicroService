<template>
  <div>
    <el-dialog
      :visible.sync="dialogFormVisible"
      :close-on-click-modal="false"
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
        <el-form-item label="排序" prop="dictSort">
          <el-input-number
            v-model.number="form.dictSort"
            :min="0"
            :max="999"
            controls-position="right"
            style="width: 370px;"
          />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button type="text" @click="dialogFormVisible = false">取消</el-button>
        <el-button :loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>
    <el-table
      ref="multipleTable"
      v-loading="listLoading"
      :data="list"
      size="small"
      style="width: 100%;"
      @row-click="handleRowClick"
    >
      <el-table-column prop="label" label="字典标签" />
      <el-table-column prop="value" label="字典值" />
      <el-table-column prop="dictSort" label="排序" />
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
  </div>
</template>
<script>
import permission from "@/directive/permission/index.js";
import Pagination from "@/components/Pagination";

export default {
  components: { Pagination },
  directives: { permission },
  data() {
    return {
      rules: {
        label: [{ required: true, message: "请输入字典标签", trigger: "blur" }],
        value: [{ required: true, message: "请输入字典值", trigger: "blur" }],
        dictSort: [
          {
            required: true,
            message: "请输入序号",
            trigger: "blur",
            type: "number"
          }
        ]
      },
      form: {},
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
        .gets("/api/business/dictDetails/all", this.listQuery)
        .then(response => {
          this.list = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
    },
    save() {},
    handleRowClick(row, column, event) {}
  }
};
</script>