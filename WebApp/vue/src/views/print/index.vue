<template>
  <div class="app-container">
    <div class="head-container">
      <!-- 搜索 -->
      <el-input v-model="listQuery.Filter" clearable size="mini" placeholder="搜索..." style="width: 200px"
        class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button class="filter-item" size="mini" type="success" icon="el-icon-search" @click="handleFilter">搜索
      </el-button>
      <div style="padding: 6px 0">
        <!-- <el-button class="filter-item" size="mini" type="primary" icon="el-icon-printer" v-print>打印
        </el-button> -->
        <el-button class="filter-item" size="mini" type="primary" icon="el-icon-plus" @click="handleCreate">新增
        </el-button>
        <el-button class="filter-item" size="mini" type="success" icon="el-icon-edit" @click="handleUpdate()">修改
        </el-button>
        <el-button slot="reference" class="filter-item" type="danger" icon="el-icon-delete" size="mini"
          @click="handleDelete()">删除</el-button>
      </div>
    </div>
    <el-dialog :visible.sync="dialogFormVisible" :close-on-click-modal="false" @close="cancel()" :title="formTitle"
      width="800px">
      <el-form ref="form" :inline="true" :model="form" :rules="rules" size="medium" label-width="80px">
        <el-row>
          <el-col :md="12"><el-form-item label="模板名称" prop="name">
              <el-input v-model="form.name" placeholder="请输入模板名称" clearable></el-input> </el-form-item></el-col>
          <el-col :md="12"><el-form-item label="模板类型" prop="tempType">
              <el-select v-model="form.tempType" placeholder="选择模板类型" :disabled="isEdit">
                <el-option label="设计模板" :value="0"></el-option>
                <el-option label="指令模板" :value="1"></el-option>
                <el-option label="HTML模板" :value="2"></el-option>
              </el-select> </el-form-item></el-col>
          <el-col :md="12" v-if="form.tempType === 2"><el-form-item label="打印方向" prop="orientation">
              <el-select v-model="form.orientation" placeholder="选择打印方向">
                <el-option label="横向打印" :value="0"></el-option>
                <el-option label="纵向打印" :value="1"></el-option>
              </el-select> </el-form-item></el-col>
          <el-col :md="12" v-if="form.tempType === 2">
            <el-form-item label="纸张类型" prop="paperKind">
              <el-select v-model="form.paperKind" placeholder="选择纸张类型">
                <el-option label="自定义" :value="0" />
                <el-option label="A3" :value="8" />
                <el-option label="A4" :value="9" />
                <el-option label="A5" :value="11" />
                <el-option label="B4" :value="12" />
                <el-option label="B5" :value="13" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :md="12" v-if="form.tempType === 2 && form.paperKind===0">
            <el-form-item label="纸张宽度" prop="paperWidth">
              <el-input type="number" v-model="form.paperWidth" placeholder="请输入纸张宽度" clearable></el-input> </el-form-item>
          </el-col>
          <el-col :md="12" v-if="form.tempType === 2 && form.paperKind===0">
            <el-form-item label="纸张高度" prop="paperHeight">
              <el-input type="number" v-model="form.paperHeight" placeholder="请输入纸张高度" clearable></el-input> </el-form-item>
          </el-col>
          <el-col :md="12">
            <el-form-item label="状态" prop="status">
              <el-select v-model="form.status" placeholder="选择模板状态">
                <el-option label="启用" :value="1"></el-option>
                <el-option label="停用" :value="0"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :md="12">
            <el-form-item label="默认" prop="isDefault">
              <el-switch v-model="form.isDefault" active-color="#13ce66" style="width: 154px">
              </el-switch> </el-form-item></el-col>
          <el-col :md="12"><el-form-item label="排序" prop="sort">
              <el-input-number v-model="form.sort" placeholder="排序"></el-input-number> </el-form-item></el-col>
          <el-col :md="12"><el-form-item label="备注" prop="remark">
              <el-input v-model="form.remark" placeholder="请输入备注" clearable></el-input> </el-form-item></el-col>
          <el-col :md="24">
            <el-form-item v-if="form.tempType === 0" label="模板内容" prop="content">
              <el-link type="primary" @click="openDesign(0)">设计模板</el-link>&nbsp;&nbsp;&nbsp;
              <el-link type="info" @click="openDesign(1)">预览模板</el-link>
            </el-form-item>
            <el-form-item label="模板内容" prop="content" v-if="form.tempType > 0">
              <el-input type="textarea" rows="8" v-model="form.content" placeholder="请输入模板内容"
                style="width: 600px"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <div slot="footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button v-if="form.tempType === 2" size="small" type="text" @click="dialogPreviewVisible = true">预览</el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>
    <el-dialog title="模板预览" width="900px" top="18vh" :visible.sync="dialogPreviewVisible">
      <div v-html="form.content"></div>
    </el-dialog>
    <el-table id="printMe" ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="width: 90%"
      @sort-change="sortChange" @selection-change="handleSelectionChange" @row-click="handleRowClick">
      <el-table-column type="selection" width="44px"></el-table-column>
      <el-table-column label="模板名称" prop="name" align="center" />
      <el-table-column label="模板类型" prop="tempType" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.tempType | displayTempType }}</span>
        </template>
      </el-table-column>
      <el-table-column label="默认" prop="isDefault" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.isDefault | displayIsDefault }}</span>
        </template>
      </el-table-column>
      <el-table-column label="状态" prop="status" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.status | displayStatus }}</span>
        </template>
      </el-table-column>
      <el-table-column label="排序" prop="sort" align="center" />
      <el-table-column label="备注" prop="remark" align="center" />
      <el-table-column label="操作" align="center">
        <template slot-scope="{ row }">
          <!-- <el-button type="text" size="mini" @click="handlePrint(row)" icon="el-icon-printer">打印</el-button> -->
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
import Print from "vue-print-nb";
import { getLodop, loadTemp, previewTemp } from "@/utils/LodopFuncs";

const defaultForm = {
  id: null,
  name: null,
  tempType: undefined,
  isDefault: false,
  status: 1,
  sort: 0,
  content: null,
  remark: null,
  paperKind:9,
  orientation:0
};
export default {
  name: "print",
  components: {
    Pagination,
  },
  directives: {
    permission,
    Print,
  },
  filters: {
    displayStatus(status) {
      const statusMap = {
        0: "停用",
        1: "启用",
      };
      return statusMap[status];
    },
    displayIsDefault(status) {
      const statusMap = {
        true: "是",
        false: "否",
      };
      return statusMap[status];
    },
    displayTempType(status) {
      const statusMap = {
        0: "设计模板",
        1: "指令模板",
        2: "HTML模板"
      };
      return statusMap[status];
    },
  },
  props: [],
  data() {
    return {
      rules: {
        name: [
          {
            required: true,
            message: "请输入模板名称",
            trigger: "blur",
          },
        ],
        tempType: [
          {
            required: true,
            message: "模板类型",
            trigger: "blur",
          },
        ],
        isDefault: [
          {
            required: true,
            message: "默认",
            trigger: "blur",
          },
        ],
        status: [
          {
            required: true,
            message: "状态",
            trigger: "blur",
          },
        ],
        sort: [
          {
            required: true,
            message: "排序",
            trigger: "blur",
          },
        ],
        content: [],
        remark: [],
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
      dialogPreviewVisible: false,
      multipleSelection: [],
      formTitle: "",
      isEdit: false,
    };
  },
  computed: {},
  watch: {},
  created() {
    this.getList();
  },
  mounted() { },
  methods: {
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount =
        (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios
        .gets("/api/business/print-template", this.listQuery)
        .then((response) => {
          this.list = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
    },
    fetchData(id) {
      this.$axios
        .gets("/api/business/print-template/" + id)
        .then((response) => {
          this.form = response;
        });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleCreate() {
      this.formTitle = "新增打印模板";
      this.isEdit = false;
      this.dialogFormVisible = true;
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
            .posts("/api/business/print-template/delete", params)
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
    handleUpdate(row) {
      this.formTitle = "修改打印模板";
      this.isEdit = true;
      if (row) {
        this.fetchData(row.id);
        this.dialogFormVisible = true;
      } else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: "编辑必须选择单行",
            type: "warning",
          });
          return;
        } else {
          this.fetchData(this.multipleSelection[0].id);
          this.dialogFormVisible = true;
        }
      }
    },
    save() {
      this.$refs.form.validate((valid) => {
        if (valid) {
          this.formLoading = true;
          this.form.roleNames = this.checkedRole;
          if (this.isEdit) {
            this.$axios
              .posts("/api/business/print-template/data-post", this.form)
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
              .posts("/api/business/print-template/data-post", this.form)
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
        }
      });
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
    openDesign(opt) {
      let _self = this;
      let LODOP = getLodop();
      if (_self.form.content) {
        loadTemp(LODOP, _self.form.content);
      }
      if (opt === 0) {
        const tid = LODOP.PRINT_DESIGN();
        LODOP.On_Return = function (taskID, value) {
          _self.form.content = value;
        };
      }
      if (opt === 1) {
        LODOP.PREVIEW();
      }
    },
    handlePrint(row) {
      if (row.tempType == 2) {
        this.$axios
          .downLoad("/api/business/print-template/pdf/" + row.id)
          .then((response) => {
            const blob = new Blob([response.data], { type: "application/pdf" });
            // 创建新的URL并指向File对象或者Blob对象的地址
            const blobURL = window.URL.createObjectURL(blob);
            window.open(blobURL)
          });
      }
    }
  },
};
</script>
<style>

</style>
