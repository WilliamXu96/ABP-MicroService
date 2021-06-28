<template>
  <div class="app-container">
    <el-form
      ref="form"
        :inline="true"
        size="small"
        label-width="66px"
    >
      <div v-for="(item,index) in drawingList" :key="index">
        <el-form-item :label="item.label">
          <el-input :type="item.fieldType" v-model="item.value" />
        </el-form-item>
      </div>
    </el-form>
  </div>
</template>

<script>
import {
  inputComponents,
  selectComponents,
  layoutComponents,
  importComponents,
  formConf,
} from "@/utils/generator/config";
import drawingDefalut from "@/utils/generator/drawingDefalut";

export default {
  name: "Dynamic",
  data() {
    return {
      formConf: Object.assign({}, formConf),
      inputComponents,
      drawingList: [],
    };
  },
  created() {
    this.drawingList=drawingDefalut
  },
};
</script>