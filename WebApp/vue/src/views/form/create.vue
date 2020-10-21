<template>
  <form-detail :is-edit="false" />
</template>

<script>
import FormDetail from "./components/FormDetail";

export default {
  name: "formCreate",
  components: { FormDetail }
};
</script>