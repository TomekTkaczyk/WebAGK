<script setup lang="ts">
    import Navbar from './components/navbar.vue'
</script>

<template>
    <Navbar />
    <main>
        <router-view />
    </main>
</template>

<style scoped>
    #app {
        font-family: Roboto, Helvetica, Arial, sans-serif;
    }
</style>
