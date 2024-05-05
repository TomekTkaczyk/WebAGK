<script setup lang="ts">
    import Navbar from './common/Navbar.vue'
</script>

<template>
    <Navbar />
    <main class="main-content">
        <router-view />
    </main>
</template>

<style scoped>
    .main-content {
        position: fixed;
        top: 60px;
        left: 0;
        width: 100%;
    }
</style>
