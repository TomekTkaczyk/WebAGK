
<!-- ***************************************************  -->
<!-- * Script section                                  *  -->
<!-- ***************************************************  -->

<script setup lang='ts'>

defineProps({
  statusCode: String,
  message: String,
});

</script>

<!-- ***************************************************  -->
<!-- * Template section                                *  -->
<!-- ***************************************************  -->

<template>
  <div class="errorCode">
      Error {{statusCode}}
  </div>
  <div class="message">
    Upss.... {{message}}.
  </div>
</template>

<!-- ***************************************************  -->
<!-- * Style section                                   *  -->
<!-- ***************************************************  -->

<style scoped>
.errorCode {
  color: red;
  font-size: 30px;
  font-weight: bold;
}

.message {
  color: blue;
  font-size: 20px;
  font-weight: bold;
}
</style>
