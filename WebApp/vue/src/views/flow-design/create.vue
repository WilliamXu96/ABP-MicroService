<template>
  <easy-flow :is-edit="false" />
</template>

<script>
import EasyFlow from '../flow/panel' 

export default {
  name: 'FlowDesignCreate',
  components: { EasyFlow }
}
</script>
