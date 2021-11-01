<template>
  <flow-design-detail :is-edit="false" />
</template>

<script>
import FlowDesignDetail from './components/FlowDesignDetail'

export default {
  name: 'FlowDesignCreate',
  components: { FlowDesignDetail }
}
</script>
