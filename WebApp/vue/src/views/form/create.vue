<template>
  <form-detail :is-edit="false" />
</template>

<script>
import FormDetail from "./components/FormDetail";

export default {
  name: "FormCreate",
  components: { FormDetail }
};
</script>