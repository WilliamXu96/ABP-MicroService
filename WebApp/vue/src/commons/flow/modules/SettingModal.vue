<template>
  <div>
    <a-drawer
      title="设置"
      placement="left"
      :width="600"
      :visible="settingVisible"
      @close="close"
    >

      <a-form
        :form="settingForm"
        layout="horizontal"
      >

        <a-divider orientation="left">画布</a-divider>
        <a-form-item label="缩小比例" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
          <a-slider
            v-decorator="['containerOnceNarrow', {}]"
            :min="0.05"
            :max="0.5"
            :step="0.05"
            :tip-formatter="formatterContainerOnceNarrow"
            @afterChange="setContainerOnceNarrow"
          />
        </a-form-item>
        <a-form-item label="放大比例" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
          <a-slider
            v-decorator="['containerOnceEnlarge', {}]"
            :min="0.05"
            :max="0.5"
            :step="0.05"
            :tip-formatter="formatterContainerOnceEnlarge"
            @afterChange="setContainerOnceEnlarge"
          />
        </a-form-item>

        <a-divider orientation="left">连线</a-divider>
        <a-form-item label="类型" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
          <a-select v-decorator="['linkType', {}]" @change="setFlowType">
            <a-select-option value="Bezier">贝塞尔曲线</a-select-option>
            <a-select-option value="Straight">直线</a-select-option>
            <a-select-option value="Flowchart">流程图线</a-select-option>
            <a-select-option value="StateMachine">状态线</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="颜色" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
          <colorPicker v-model="linkColor" @change="setLinkColor" />
        </a-form-item>
        <a-form-item label="粗细" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
          <a-slider
            v-decorator="['linkThickness', {}]"
            :min="1"
            :max="10"
            @afterChange="setStrokeWidth"
          />
        </a-form-item>

        <a-divider orientation="left">默认样式</a-divider>
        <a-form-item label="辅助线" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
          <a-switch
            v-decorator="['isOpenAuxiliaryLine', {}]"
            :checked="isOpenAuxiliaryLine"
            checked-children="开"
            un-checked-children="关"
            @change="toggleOpenAuxiliaryLine"
          />
        </a-form-item>
        <a-form-item label="自动对齐水平间距" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
          <a-slider
            v-decorator="['alignLevelDistance', {}]"
            :min="10"
            :max="800"
            :step="5"
            @afterChange="setAlignLevelDistance"
          />
        </a-form-item>
        <a-form-item label="自动对齐垂直间距" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
          <a-slider
            v-decorator="['alignVerticalDistance', {}]"
            :min="10"
            :max="800"
            :step="5"
            @afterChange="setAlignVerticalDistance"
          />
        </a-form-item>
        <a-form-item label="微移距离" :label-col="formItemLayout.labelCol" :wrapper-col="formItemLayout.wrapperCol">
          <a-slider
            v-decorator="['movePx', {}]"
            :min="1"
            @afterChange="setMovePx"
          />
        </a-form-item>
      </a-form>
    </a-drawer>
  </div>
</template>

<script>
import { flowConfig } from '../config/args-config.js'

export default {
  data() {
    return {
      settingVisible: false,
      formItemLayout: {
        labelCol: { span: 6 },
        wrapperCol: { span: 15 }
      },
      initFlag: false,
      settingForm: this.$form.createForm(this),
      isOpenAuxiliaryLine: flowConfig.defaultStyle.isOpenAuxiliaryLine,
      linkColor: flowConfig.jsPlumbInsConfig.PaintStyle.stroke
    }
  },
  methods: {
    init() {
      const that = this
      that.$nextTick(() => {
        that.settingForm.setFieldsValue({
          movePx: flowConfig.defaultStyle.movePx,
          linkType: flowConfig.jsPlumbInsConfig.Connector[0],
          linkThickness: flowConfig.jsPlumbInsConfig.PaintStyle.strokeWidth,
          alignLevelDistance: flowConfig.defaultStyle.alignSpacing.level,
          alignVerticalDistance: flowConfig.defaultStyle.alignSpacing.vertical,
          containerOnceNarrow: flowConfig.defaultStyle.containerScale.onceNarrow,
          containerOnceEnlarge: flowConfig.defaultStyle.containerScale.onceEnlarge
        })
      })
    },
    open() {
      this.settingVisible = true
      if (!this.initFlag) {
        this.init()
        this.initFlag = true
      }
    },
    close() {
      this.settingVisible = false
    },
    setFlowType(v) {
      flowConfig.jsPlumbInsConfig.Connector[0] = v
    },
    toggleOpenAuxiliaryLine(flag) {
      this.isOpenAuxiliaryLine = flag
      flowConfig.defaultStyle.isOpenAuxiliaryLine = flag
    },
    setMovePx(v) {
      flowConfig.defaultStyle.movePx = v
    },
    setLinkColor(v) {
      this.linkColor = v
      flowConfig.jsPlumbInsConfig.PaintStyle.stroke = v
    },
    setStrokeWidth(v) {
      flowConfig.jsPlumbInsConfig.PaintStyle.strokeWidth = v
    },
    setAlignLevelDistance(v) {
      flowConfig.defaultStyle.alignSpacing.level = v
    },
    setAlignVerticalDistance(v) {
      flowConfig.defaultStyle.alignSpacing.vertical = v
    },
    formatterContainerOnceNarrow(v) {
      return `${v * 100}%`
    },
    setContainerOnceNarrow(v) {
      flowConfig.defaultStyle.containerScale.onceNarrow = v
    },
    formatterContainerOnceEnlarge(v) {
      return `${v * 100}%`
    },
    setContainerOnceEnlarge(v) {
      flowConfig.defaultStyle.containerScale.onceEnlarge = v
    }
  }
}
</script>

<style>
	.m-colorPicker .box {
		z-index: 9999 !important;
		width: 220px !important;
	}

	.ant-divider-horizontal.ant-divider-with-text, .ant-divider-horizontal.ant-divider-with-text-left, .ant-divider-horizontal.ant-divider-with-text-right {
		font-weight: 800;
		margin: 24px 0 4px;
	}
</style>
