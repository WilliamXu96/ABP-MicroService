<template>
  <el-form ref="basicInfoForm" :model="info" :rules="rules" label-width="150px">
    <el-row>
      <el-col :span="12">
        <el-form-item label="表单名" prop="formName">
          <el-input v-model="info.formName" disabled />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="实体名称" prop="entityName">
          <el-input placeholder="请输入实体名称" v-model="info.entityName" />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="表名称" prop="tableName">
          <el-input placeholder="请输入表名称" v-model="info.tableName" />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="表描述" prop="remark">
          <el-input placeholder="请输入表描述" v-model="info.remark" />
        </el-form-item>
      </el-col>
    </el-row>
  </el-form>
</template>
<script>
export default {
  name: "BasicInfoForm",
  props: {
    info: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      rules: {
        tableName: [
          { required: true, message: "请输入表名称", trigger: "blur" }
        ],
        entityName: [
          { required: true, message: "请输入实体类名称", trigger: "blur" }
        ]
      }
    };
  }
};
</script>
