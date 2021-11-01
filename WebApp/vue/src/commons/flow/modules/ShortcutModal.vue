<template>
  <a-modal
    title="快捷键大全"
    width="60%"
    :visible="modalVisible"
    ok-text="确认"
    cancel-text="取消"
    @ok="saveSetting"
    @cancel="cancel"
  >
    <a-table
      row-key="code"
      :columns="columns"
      :data-source="dataSource"
    />
  </a-modal>
</template>

<script>
import { flowConfig } from '../config/args-config.js'

export default {
  data() {
    return {
      modalVisible: false,
      columns: [
        {
          title: '功能',
          align: 'center',
          key: 'shortcutName',
          dataIndex: 'shortcutName',
          width: '50%'
        },
        {
          title: '快捷键',
          align: 'center',
          key: 'codeName',
          dataIndex: 'codeName',
          width: '50%'
        }
      ],
      dataSource: []
    }
  },
  methods: {
    open() {
      const that = this

      that.modalVisible = true
      const obj = Object.assign({}, flowConfig.shortcut)
      for (const k in obj) {
        that.dataSource.push(obj[k])
      }
    },
    close() {
      this.dataSource = []
      this.modalVisible = false
    },
    saveSetting() {
      this.close()
    },
    cancel() {
      this.close()
    }
  }
}
</script>

<style>
</style>
