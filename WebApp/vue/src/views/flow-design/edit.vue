<template>
  <easy-flow :is-edit="true" />
</template>

<script>
import EasyFlow from '../flow/panel'

export default {
  name: "FlowDesignEdit",
  components: { EasyFlow }
};
</script>