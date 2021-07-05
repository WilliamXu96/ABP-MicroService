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

    <el-table 
    :data="tableData"
    height="400px" 
    fit
    >
    <el-table-column
        v-for="(item, index) in drawingList"
        :key="index" 
        :prop="item.fieldName" 
        :label="item.label">
    </el-table-column>
</el-table>
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
      tableData: [
			{id: 1, date: '2018-07-24', name: 23.34, mobile: 137597.76, const: 102203.71, profit: 35394.05},
			{id: 2, date: '2018-07-24', name: 23.34, mobile: 137597.76, const: 102203.71, profit: 35394.05},
		],
    };
  },
  created() {
    this.drawingList=drawingDefalut
  },
};
</script>