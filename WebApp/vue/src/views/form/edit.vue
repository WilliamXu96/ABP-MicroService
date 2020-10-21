<template>
  <form-detail :is-edit="true" />
</template>

<script>
import FormDetail from "./components/FormDetail";

export default {
  name: "FormEdit",
  components: { FormDetail }
};
</script>