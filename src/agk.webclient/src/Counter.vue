<script setup lang="ts">
    import { ref, onMounted } from 'vue';

    import fetchCount from './fetchCount'


    const count = ref<number | null>(null);

    const cb = (initialValue: number) => {
        count.value = initialValue;
    }

    function addCount(num: number) {
        if (count.value !== null) {
            count.value += num;
        }
    }

    onMounted(() => {
        fetchCount(cb);
    });
</script>

<template>
    {{count}}
    <button @click="addCount(2)">Add</button>
</template>