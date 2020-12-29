<template>
  <el-card>
    <el-tabs v-model="activeName">
      <el-tab-pane label="基本信息" name="basic">
        <basic-info-form ref="basicInfo" :info="form" />
      </el-tab-pane>
      <el-tab-pane label="字段信息" name="cloum">
        <el-table ref="dragTable" :data="form.fields" row-key="columnId" :max-height="tableHeight">
          <el-table-column
            label="字段名"
            prop="fieldName"
            min-width="10%"
            :show-overflow-tooltip="true"
          />
          <el-table-column label="字段描述" min-width="10%">
            <template slot-scope="scope">
              <el-input size="mini" style="width:150px" v-model="scope.row.label"></el-input>
            </template>
          </el-table-column>
          <el-table-column
            label="物理类型"
            prop="columnType"
            min-width="10%"
            :show-overflow-tooltip="true"
          />
          <el-table-column label="只读" min-width="5%">
            <template slot-scope="scope">
              <el-checkbox true-label="true" v-model="scope.row.isReadonly"></el-checkbox>
            </template>
          </el-table-column>
          <el-table-column label="必填" min-width="5%">
            <template slot-scope="scope">
              <el-checkbox true-label="true" v-model="scope.row.isRequired"></el-checkbox>
            </template>
          </el-table-column>
          
          <!-- <el-table-column label="显示类型" min-width="12%">
            <template slot-scope="scope">
              <el-select v-model="scope.row.htmlType">
                <el-option label="文本框" value="input" />
                <el-option label="文本域" value="textarea" />
                <el-option label="下拉框" value="select" />
                <el-option label="单选框" value="radio" />
                <el-option label="复选框" value="checkbox" />
                <el-option label="日期控件" value="datetime" />
              </el-select>
            </template>
          </el-table-column> -->
          <el-table-column label="字典类型" min-width="12%">
            <template slot-scope="scope">
              <el-select size="mini" v-model="scope.row.dictType" clearable filterable placeholder="请选择">
                <el-option
                  v-for="dict in dictOptions"
                  :key="dict.dictType"
                  :label="dict.dictName"
                  :value="dict.dictType">
                  <span style="float: left">{{ dict.dictName }}</span>
                  <span style="float: right; color: #8492a6; font-size: 13px">{{ dict.dictType }}</span>
              </el-option>
              </el-select>
            </template>
          </el-table-column>
        </el-table>
      </el-tab-pane>
    </el-tabs>
    <el-form label-width="100px">
      <el-form-item style="text-align: center;margin-left:-100px;margin-top:10px;">
        <el-button type="primary"  v-loading="formLoading" @click="submitForm()">提交</el-button>
        <el-button @click="close()">返回</el-button>
      </el-form-item>
    </el-form>
  </el-card>
</template>
<script>
import basicInfoForm from "./basicInfoForm";
import Sortable from 'sortablejs'

export default {
  name: "BuildEdit",
  components: {
    basicInfoForm
  },
  data() {
    return {
      // 选中选项卡的 name
      activeName: "cloum",
      // 表格的高度
      tableHeight: document.documentElement.scrollHeight - 245 + "px",
      // 表列信息
      cloumns: [],
      // 字典信息
      dictOptions: [],
      // 菜单信息
      menus: [],
      // 表详细信息
      info: {},
      form:{},
      formLoading: false,
    };
  },
  created() {
    const id = this.$route.params && this.$route.params.id;
    if (id) {
      this.fetchData(id)
    }
  },
  methods: {
    fetchData(id) {
      this.$axios.gets("/api/business/build/" + id).then(response => {
        this.form = response;
      });
    },
    /** 提交按钮 */
    submitForm() {
      const basicForm = this.$refs.basicInfo.$refs.basicInfoForm;
      Promise.all([basicForm].map(this.getFormPromise)).then(res => {
        const validateResult = res.every(item => !!item);
        if (validateResult) {
          const form = Object.assign({}, basicForm.model);
          form.fields=this.form.fields
          this.$axios
              .puts("/api/business/build/" + this.form.id, form)
              .then(response => {
                this.formLoading = false;
                this.$notify({
                  title: "成功",
                  message: "更新成功",
                  type: "success",
                  duration: 2000
                });
                this.close()
              })
              .catch(() => {
                this.formLoading = false;
              });
        } else {
          this.$message({
            message: "表单校验未通过，请重新检查提交内容",
            type: "warning",
          });
        }
      });
    },
    getFormPromise(form) {
      return new Promise(resolve => {
        form.validate(res => {
          resolve(res);
        });
      });
    },
    /** 关闭按钮 */
    close() {
      this.$store.dispatch("tagsView/delView", this.$route);
      this.$router.replace({ path: "/tool/build" })
    },
  },
  mounted() {
    const el = this.$refs.dragTable.$el.querySelectorAll(".el-table__body-wrapper > table > tbody")[0];
    const sortable = Sortable.create(el, {
      handle: ".allowDrag",
      onEnd: evt => {
        const targetRow = this.cloumns.splice(evt.oldIndex, 1)[0];
        this.cloumns.splice(evt.newIndex, 0, targetRow);
        for (let index in this.cloumns) {
          this.cloumns[index].sort = parseInt(index) + 1;
        }
      }
    });
  }
};
</script>
