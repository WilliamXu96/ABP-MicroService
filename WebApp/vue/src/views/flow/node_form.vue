<template>
  <div>
    <div class="ef-node-form">
      <div class="ef-node-form-header">编辑</div>
      <div class="ef-node-form-body">
        <el-form
          :model="node"
          ref="dataForm"
          label-width="80px"
          v-show="type === 'node'"
        >
          <el-form-item label="类型">
            <el-input v-model="node.type" :disabled="true"></el-input>
          </el-form-item>
          <el-form-item label="名称">
            <el-input v-model="node.name"></el-input>
          </el-form-item>
          <el-form-item label="left坐标">
            <el-input v-model="node.left" :disabled="true"></el-input>
          </el-form-item>
          <el-form-item label="top坐标">
            <el-input v-model="node.top" :disabled="true"></el-input>
          </el-form-item>
          <el-form-item label="ico图标">
            <el-input v-model="node.ico"></el-input>
          </el-form-item>
          <el-form-item label="状态">
            <el-select v-model="node.state" placeholder="请选择" style="width: 100%">
              <el-option
                v-for="item in stateList"
                :key="item.state"
                :label="item.label"
                :value="item.state"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="执行权限">
            <el-select v-model="executor" placeholder="所有人" clearable  @change="handleRowClick()" style="width: 100%">
              <el-option
                v-for="execut in executOption"
                :key="execut.value"
                :label="execut.label"
                :value="execut.value"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item
            label="选择用户"
            v-if="executor == 'users'"
          >
            <el-select v-model="node.users" multiple style="width: 100%" placeholder="请选择">
            <el-option
              v-for="item in userList"
              :key="item.userName"
              :label="item.userName"
              :value="item.userName"
            ></el-option>
          </el-select>
          </el-form-item>
          <el-form-item
            label="选择角色"
            v-if="executor == 'roles'"
          >
            <el-select v-model="node.roles" multiple style="width: 100%" placeholder="请选择">
            <el-option
              v-for="item in roleList"
              :key="item.name"
              :label="item.name"
              :value="item.name"
            ></el-option>
          </el-select>
          </el-form-item>
          <el-form-item>
            <el-button icon="el-icon-close" size="mini">重置</el-button>
            <el-button type="primary" icon="el-icon-check" size="mini" @click="save"
              >暂存</el-button
            >
          </el-form-item>
        </el-form>

        <el-form
          :model="line"
          ref="dataForm"
          label-width="80px"
          v-show="type === 'line'"
        >
          <el-form-item label="源节点" size="small">
            <el-input :value="line.from" disabled />
          </el-form-item>
          <el-form-item label="目标节点" size="small">
            <el-input :value="line.to" disabled />
          </el-form-item>
          <el-form-item label="名称">
            <el-input v-model="line.label"></el-input>
          </el-form-item>
          <el-form-item label="字段条件" size="small">
            <div align="left" v-for="(item,index) in tempFormField" :key="index">
              <el-select v-model="item.fieldId" placeholder="请选择" size="mini" style="width:60%">
                <el-option
                  v-for="field in fieldList"
                  :key="field.id"
                  :label="field.label"
                  :value="field.id"
                ></el-option>
              </el-select>
              <el-select
                v-model="item.condition"
                placeholder="请选择"
                size="mini"
                style="width:35%"
              >
                <el-option
                  v-for="condition in conditionOption"
                  :key="condition.id"
                  :label="condition.label"
                  :value="condition.value"
                ></el-option>
              </el-select>
              <el-input size="mini" style="width:80%" v-model="item.content"></el-input>
              <el-button circle icon="el-icon-delete" size="mini" @click="removeRow(index)"></el-button>
            </div>
            
          </el-form-item>
          <el-form-item>
            <el-button type="primary" icon="el-icon-plus" size="mini" @click="addRow">添加</el-button>
            <el-button type="primary" size="mini" icon="el-icon-check" @click="saveLine"
              >暂存</el-button
            >
          </el-form-item>
        </el-form>
      </div>
      <!--            <div class="el-node-form-tag"></div>-->
    </div>
  </div>
</template>

<script>
import { cloneDeep } from "lodash";

export default {
  data() {
    return {
      visible: true,
      // node 或 line
      type: "node",
      node: {},
      line: {},
      data: {},
      roleList: [],
      userList:[],
      fieldList:[],
      executor:'',
      executOption: [
        { value: "users", label: "指定用户" },
        { value: "roles", label: "指定角色" }
      ],
      fieldOption: [
        { value: "name", label: "姓名" },
        { value: "age", label: "年龄" },
        { value: "sex", label: "性别" }
      ],
      conditionOption: [
        { value: ">", label: ">" },
        { value: ">=", label: ">=" },
        { value: "<", label: "<" },
        { value: "<=", label: "<=" },
        { value: "=", label: "=" },
        { value: "!=", label: "!=" }
      ],
      tempFormField:[],
      stateList: [
        {
          state: "success",
          label: "成功",
        },
        {
          state: "warning",
          label: "警告",
        },
        {
          state: "error",
          label: "错误",
        },
        {
          state: "running",
          label: "运行中",
        },
      ],
    };
  },
  methods: {
    getAllRoles() {
      this.$axios.gets("/api/identity/roles/all").then(response => {
        this.roleList=response.items
      });
    },
    //修改用户接口
    getAllUsers() {
      this.$axios.gets("/api/base/user").then(response => {
        this.userList=response.items
      });
    },
    getAllFields(id) {
      this.$axios.gets("/api/business/form/" + id).then((response) => {
        this.fieldList=response.fields
      });
    },
    getAllConditions(){
      this.$axios.gets("/api/base/dictDetails/list",{name:'condition'}).then((response) => {
        this.conditionOption=response.items
      });
    },
    nodeInit(data, id) {
      this.type = "node";
      this.data = data;
      this.getAllUsers()
      this.getAllRoles()
      data.nodeList.filter((node) => {
        if (node.id === id) {
          this.node = cloneDeep(node);
          this.executor=node.executor
        }
      });
    },
    lineInit(line,data,formId) {
      this.type = "line";
      this.line = line;
      this.getAllFields(formId)
      this.getAllConditions()
      data.lineList.filter((i) => {
        if (i.from == line.from && i.to == line.to) {
          this.tempFormField = !i.formField ? [{fieldId:'',condition:'',content:''}] : cloneDeep(i.formField);
        }
      });
    },
    // 修改连线
    saveLine() {
      this.$emit("setLineLabel", this.line.from, this.line.to, this.line.label, this.tempFormField );
    },
    addRow(){
      if(this.tempFormField.length>0){
        this.$alert("暂不支持多字段条件", "提示", {
          confirmButtonText: "确定",
        });
        return
      }
      
      var row= {fieldId:'',condition:'',content:''}
      this.tempFormField.push(row)
    },
    removeRow(index){
      this.tempFormField.splice(index,1)
    },
    handleRowClick(){
    },
    save() {
      this.data.nodeList.filter((node) => {
        if (node.id === this.node.id) {
          node.name = this.node.name;
          node.left = this.node.left;
          node.top = this.node.top;
          node.ico = this.node.ico;
          node.state = this.node.state;
          node.executor=this.executor
          if(this.executor==='users'){
              node.users=this.node.users
          }
          if(this.executor==='roles'){
              node.roles=this.node.roles
          }
          this.$emit("repaintEverything");
        }
      });
    },
  },
};
</script>

<style>
.el-node-form-tag {
  position: absolute;
  top: 50%;
  margin-left: -15px;
  height: 40px;
  width: 15px;
  background-color: #fbfbfb;
  border: 1px solid rgb(220, 227, 232);
  border-right: none;
  z-index: 0;
}
</style>
