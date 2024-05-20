<script setup lang="ts">
    import Navbar from '@/components/Navbar.vue'
    import Footbar from '@/components/Footbar.vue'
    import router from './router'

    router.push("/clients");
</script>

<template>
    <div class="d-flex flex-column overflow-hidden min-vh-100 vh-100">
        <header>
            <Navbar />
        </header>
        <main role="main" class="flex-grow-1 overflow-auto">
            <div class="router-view">
                <RouterView />
            </div>
        </main>
        <footer>
            <Footbar />
        </footer>
    </div>
</template>

<style>
    #app {
        font-family: Roboto, Helvetica, Arial, sans-serif;
        color: #2c3e50;
    }

    .router-view {
        margin: 20px;
        bottom: 20px;
    }
</style>
