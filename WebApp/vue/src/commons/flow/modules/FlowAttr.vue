<template>
  <div>
    <el-tabs type="border-card" v-model="activeKey" style="height:536px;overflow: auto;">
      <el-tab-pane name="flow-attr" disabled>
        <span slot="label">
          <svg-icon icon-class="task" />流程属性
        </span>
        <el-form>
          <el-form-item label="流程id">
            <el-input :value="flowData.attr.id" disabled />
          </el-form-item>
        </el-form>
      </el-tab-pane>
      <el-tab-pane name="node-attr" disabled>
        <span slot="label">
          <i class="el-icon-setting"></i> 节点属性
        </span>
        <template v-if="currentSelect.type == 'start'">
          <el-form>
            <el-form-item label="类型">
              <el-tag size="small">{{ currentSelect.type }}</el-tag>
            </el-form-item>
            <el-form-item label="id">
              <el-input :value="currentSelect.id" disabled />
            </el-form-item>
            <el-form-item label="名称">
              <el-input
                placeholder="请输入节点名称"
                :value="currentSelect.nodeName"
                @input="nodeNameChange"
              />
            </el-form-item>
            <el-form-item label="执行权限" placeholder="请选择">
              <el-select v-model="executor.start" style="width:100%">
                <el-option
                  v-for="execut in executOption"
                  :key="execut.value"
                  :label="execut.label"
                  :value="execut.value"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item
              label="指定用户"
              v-if="currentSelect.type == 'start' && executor.start=='users'"
            >
              <el-input :value="currentSelect.users" readonly></el-input>
            </el-form-item>
            <el-form-item
              label="指定角色"
              v-if="currentSelect.type == 'start' && executor.start=='roles'"
            >
              <el-input :value="currentSelect.roles" readonly></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-if="currentSelect.type == 'end'">
          <el-form>
            <el-form-item label="类型">
              <el-tag size="small">{{ currentSelect.type }}</el-tag>
            </el-form-item>
            <el-form-item label="id">
              <el-input :value="currentSelect.id" disabled />
            </el-form-item>
            <el-form-item label="名称">
              <el-input
                placeholder="请输入节点名称"
                :value="currentSelect.nodeName"
                @input="nodeNameChange"
              />
            </el-form-item>
            <el-form-item label="执行权限" placeholder="请选择">
              <el-select v-model="executor.end" style="width:100%">
                <el-option
                  v-for="execut in executOption"
                  :key="execut.value"
                  :label="execut.label"
                  :value="execut.value"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="指定用户" v-if="currentSelect.type == 'end' && executor.end=='users'">
              <el-input :value="currentSelect.users" readonly></el-input>
            </el-form-item>
            <el-form-item label="指定角色" v-if="currentSelect.type == 'end' && executor.end=='roles'">
              <el-input :value="currentSelect.roles" readonly></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-if="currentSelect.type == 'task'">
          <el-form>
            <el-form-item label="类型">
              <el-tag size="small">{{ currentSelect.type }}</el-tag>
            </el-form-item>
            <el-form-item label="id">
              <el-input :value="currentSelect.id" disabled />
            </el-form-item>
            <el-form-item label="名称">
              <el-input
                placeholder="请输入节点名称"
                :value="currentSelect.nodeName"
                @input="nodeNameChange"
              />
            </el-form-item>
            <el-form-item label="执行权限" placeholder="请选择">
              <el-select v-model="executor.task" style="width:100%">
                <el-option
                  v-for="execut in executOption"
                  :key="execut.value"
                  :label="execut.label"
                  :value="execut.value"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item
              label="指定用户"
              v-if="currentSelect.type == 'task' && executor.task=='users'"
            >
              <el-input :value="currentSelect.users" readonly></el-input>
            </el-form-item>
            <el-form-item
              label="指定角色"
              v-if="currentSelect.type == 'task' && executor.task=='roles'"
            >
              <el-input :value="currentSelect.roles" readonly></el-input>
            </el-form-item>
          </el-form>
        </template>

        <template v-if="currentSelect.type == 'checkinStart'">
          <el-form>
            <el-form-item label="类型">
              <el-tag size="small">{{ currentSelect.type }}</el-tag>
            </el-form-item>
            <el-form-item label="id">
              <el-input :value="currentSelect.id" disabled />
            </el-form-item>
            <el-form-item label="名称">
              <el-input
                placeholder="请输入节点名称"
                :value="currentSelect.nodeName"
                @input="nodeNameChange"
              />
            </el-form-item>
            <el-form-item label="执行权限" placeholder="请选择">
              <el-select v-model="executor.checkinStart" style="width:100%">
                <el-option
                  v-for="execut in executOption"
                  :key="execut.value"
                  :label="execut.label"
                  :value="execut.value"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item
              label="指定用户"
              v-if="currentSelect.type == 'checkinStart' && executor.checkinStart=='users'"
            >
              <el-input :value="currentSelect.users" readonly></el-input>
            </el-form-item>
            <el-form-item
              label="指定角色"
              v-if="currentSelect.type == 'checkinStart' && executor.checkinStart=='roles'"
            >
              <el-input :value="currentSelect.roles" readonly></el-input>
            </el-form-item>
          </el-form>
        </template>

        <template v-if="currentSelect.type == 'checkinEnd'">
          <el-form>
            <el-form-item label="类型">
              <el-tag size="small">{{ currentSelect.type }}</el-tag>
            </el-form-item>
            <el-form-item label="id">
              <el-input :value="currentSelect.id" disabled />
            </el-form-item>
            <el-form-item label="名称">
              <el-input
                placeholder="请输入节点名称"
                :value="currentSelect.nodeName"
                @input="nodeNameChange"
              />
            </el-form-item>
            <el-form-item label="执行权限" placeholder="请选择">
              <el-select v-model="executor.checkinEnd" style="width:100%">
                <el-option
                  v-for="execut in executOption"
                  :key="execut.value"
                  :label="execut.label"
                  :value="execut.value"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item
              label="指定用户"
              v-if="currentSelect.type == 'checkinEnd' && executor.checkinEnd=='users'"
            >
              <el-input :value="currentSelect.users" readonly></el-input>
            </el-form-item>
            <el-form-item
              label="指定角色"
              v-if="currentSelect.type == 'checkinEnd' && executor.checkinEnd=='roles'"
            >
              <el-input :value="currentSelect.roles" readonly></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-else-if="currentSelect.type == 'x-lane' || currentSelect.type == 'y-lane'">
          <el-form>
            <el-form-item label="类型">
              <el-tag size="small">{{ currentSelect.type }}</el-tag>
            </el-form-item>
            <el-form-item label="id">
              <el-input :value="currentSelect.id" disabled />
            </el-form-item>
            <el-form-item label="名称">
              <el-input
                placeholder="请输入节点名称"
                :value="currentSelect.nodeName"
                @input="nodeNameChange"
              />
            </el-form-item>
            <el-form-item label="执行权限" placeholder="请选择">
              <el-select v-model="executor" style="width:100%">
                <el-option
                  v-for="execut in executOption"
                  :key="execut.value"
                  :label="execut.label"
                  :value="execut.value"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="指定用户" v-if="currentSelect.type == 'start' && executor=='users'">
              <el-input :value="currentSelect.users" readonly></el-input>
            </el-form-item>
            <el-form-item label="指定角色" v-if="currentSelect.type == 'start' && executor=='roles'">
              <el-input :value="currentSelect.roles" readonly></el-input>
            </el-form-item>
          </el-form>
        </template>
      </el-tab-pane>
      <el-tab-pane name="link-attr" disabled>
        <span slot="label">
          <i class="el-icon-share"></i> 连线属性
        </span>
        <el-form label-position="left">
          <el-form-item label="id" size="small">
            <el-input :value="currentSelect.id" disabled />
          </el-form-item>
          <el-form-item label="源节点" size="small">
            <el-input :value="currentSelect.sourceId" disabled />
          </el-form-item>
          <el-form-item label="目标节点" size="small">
            <el-input :value="currentSelect.targetId" disabled />
          </el-form-item>
          <el-form-item label="文本" size="small">
            <el-input :value="currentSelect.label" @input="linkLabelChange" />
          </el-form-item>

          <el-form-item label="表单字段条件" size="small">
            <br />
            <div align="left" v-for="(item,index) in tempFieldForm" :key="index">
              <el-select v-model="item.fieldName" placeholder="请选择" size="mini" style="width:90px">
                <el-option
                  v-for="field in fieldOption"
                  :key="field.value"
                  :label="field.label"
                  :value="field.value"
                ></el-option>
              </el-select>
              <el-select
                v-model="item.condition"
                placeholder="请选择"
                size="mini"
                style="width:70px"
              >
                <el-option
                  v-for="condition in conditionOption"
                  :key="condition.value"
                  :label="condition.label"
                  :value="condition.value"
                ></el-option>
              </el-select>
              <el-input size="mini" style="width:90px" v-model="item.content"></el-input>
              <el-button type="danger" icon="el-icon-delete" size="mini" @click="removeRow(index)"></el-button>
            </div>
            <el-button type="primary" icon="el-icon-plus" size="mini" @click="addRow">新增</el-button>
              <el-button type="primary" icon="el-icon-plus" size="mini" @click="tempSave">暂存</el-button>
          </el-form-item>
        </el-form>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script>
import jsplumb from "jsplumb";
import { flowConfig } from "../config/args-config.js";

export default {
  components: {
    jsplumb
  },
  props: ["plumb", "flowData", "select"],
  data() {
    return {
      currentSelect: this.select,
      formItemLayout: {
        labelCol: { span: 6 },
        wrapperCol: { span: 16 }
      },
      activeKey: "flow-attr",
      executOption: [
        { value: "users", label: "指定用户" },
        { value: "everyone", label: "所有人" },
        { value: "roles", label: "指定角色" }
      ],
      executor: {
        start: "",
        end: "",
        task: "",
        checkinStart: "",
        chechinEnd: ""
      },
      fieldOption: [
        { value: "name", label: "姓名" },
        { value: "age", label: "年龄" },
        { value: "sex", label: "性别" }
      ],
      fieldSelected: "",
      conditionOption: [
        { value: ">", label: ">" },
        { value: ">=", label: ">=" },
        { value: "<", label: "<" },
        { value: "<=", label: "<=" },
        { value: "=", label: "=" },
        { value: "!=", label: "!=" }
      ],
      conditionSelected: "",
      dynamicForm:{
        field:[{value:''}],
        condition:[{value:''}],
        text:[{value:''}]
      },
      tempFieldForm:[],
      fieldForm:[]
    };
  },
  watch: {
    select(val) {
      this.currentSelect = val;
      if (this.currentSelect.type == "link") {
        this.activeKey = "link-attr";
      } else if (!this.currentSelect.type) {
        this.activeKey = "flow-attr";
      } else {
        this.activeKey = "node-attr";
      }
    },
    currentSelect: {
      handler(val) {
        this.$emit("update:select", val);
      },
      deep: true
    }
  },
  methods: {
    nodeNameChange(value) {
      this.currentSelect.nodeName = value;
    },
    linkLabelChange(value) {
      const that = this;
      const label = value;

      that.currentSelect.label = label;
      const conn = that.plumb.getConnections({
        source: that.currentSelect.sourceId,
        target: that.currentSelect.targetId
      })[0];
      if (label != "") {
        conn.setLabel({
          label: label,
          cssClass: "linkLabel"
        });
      } else {
        const labelOverlay = conn.getLabelOverlay();
        if (labelOverlay) conn.removeOverlay(labelOverlay.id);
      }
    },
    addRow(){
      var row=new Object();
      row.linkId=this.currentSelect.id
      row.fieldName='name'
      row.condition='>'
      row.content='123'
      this.tempFieldForm.push(row)
    },
    removeRow(index){
      this.tempFieldForm.splice(index,1)
    },
    tempSave(){
      console.log(this.tempFieldForm)
    }
  }
};
</script>

<style lang="scss">
@import "../style/flow-attr.scss";
</style>
