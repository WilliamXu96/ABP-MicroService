<template>
  <flow-design-detail :is-edit="true" />
</template>

<script>
import FlowDesignDetail from './components/FlowDesignDetail'

export default {
  name: "FlowDesignEdit",
  components: { FlowDesignDetail }
};
</script>