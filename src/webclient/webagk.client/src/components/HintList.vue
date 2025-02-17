<template>
  <ul class="message-list" v-if="messages.length > 0">
    <li class="message-item" v-for="(hint,index) in messages" :key="index">
      {{ hint }}
    </li>
  </ul>
</template>

<script setup lang="ts">

  defineProps({
  messages: {
    type: Array as () => string[],
    required: true
  }
});

</script>

<style scoped>
  .message-list {
    margin: 0;
    padding: 0;
    list-style-type: none;
    font-size: 0.875rem; /* Mała czcionka */
    color: #d32f2f; /* Kolor czerwony na błędy, można zmienić */
  }

  .message-item {
    position: relative;
    padding-left: 1.5em; /* Odstęp na kropkę */
    margin-bottom: 0.25em;
  }

    .message-item::before {
      content: '\2022'; /* Unicode dla kropki */
      position: absolute;
      left: 0;
      font-size: 1rem; /* Rozmiar kropki */
      color: #d32f2f; /* Ten sam kolor co tekst */
    }
</style>
