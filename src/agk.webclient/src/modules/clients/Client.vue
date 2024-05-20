<script setup lang="ts">
    const props = defineProps<{
        id: string;
    }>();

    console.log(props.id);
</script>

<template>
    Client details with id: {{id}}
</template>