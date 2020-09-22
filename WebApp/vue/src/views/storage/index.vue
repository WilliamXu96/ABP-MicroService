<template>
  <el-tabs v-model="activeName" style="padding-left: 8px;" @tab-click="tabClick">
    <el-tab-pane label="本地存储" name="local">
      <Local ref="local" />
    </el-tab-pane>
    <el-tab-pane label="数据库存储" name="blob">
      <Blob ref="blob" />
    </el-tab-pane>
  </el-tabs>
</template>

<script>
import Blob from './blob/index'
import Local from './local/index'
export default {
  name: 'Storage',
  components: { Blob, Local },
  data() {
    return {
      activeName: 'local'
    }
  },
  methods: {
    tabClick(name) {
      if (this.activeName === 'local') {
        this.$refs.local.getList()
      } else {
        //this.$refs.qiNiu.crud.toQuery()
      }
    }
  }
}
</script>

<style scoped>
</style>
