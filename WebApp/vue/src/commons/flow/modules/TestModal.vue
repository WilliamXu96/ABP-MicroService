<template>
  <div>
    <a-drawer
      title="测试"
      placement="right"
      :width="600"
      :visible="testVisible"
      @close="onClose"
    >

      <div>当前的flowData:</div>
      <json-view
        :value="flowData"
        :expand-depth="3"
        boxed
        copyable
      />

      <div style="margin-top: 12px;">暂存:</div>
      <a-textarea :autosize="{ minRows: 10, maxRows: 100 }" :value="flowDataJson" @change="editFlowDataJson" />

      <a-divider />
      <a-button :style="{ marginRight: '8px' }" @click="tempSave">暂存</a-button>
      <a-button type="primary" @click="onLoad">加载(暂存中的json数据)</a-button>
    </a-drawer>
  </div>
</template>

<script>
import JsonView from 'vue-json-viewer'
import { flowConfig } from '../config/args-config.js'

export default {
  components: {
    JsonView
  },
  data() {
    return {
      testVisible: false,
      flowData: null,
      flowDataJson: ''
    }
  },
  methods: {
    onClose() {
      this.testVisible = false
    },
    editFlowDataJson(e) {
      this.flowDataJson = e.target.value
    },
    tempSave() {
      const tempObj = Object.assign({}, this.flowData)
      tempObj.status = flowConfig.flowStatus.SAVE
      this.flowDataJson = JSON.stringify(tempObj)
    },
    onLoad() {
      this.$emit('loadFlow', this.flowDataJson)
      this.onClose()
    }
  }
}
</script>

<style>
</style>
