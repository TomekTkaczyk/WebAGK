<!-- <script lang="ts">
    import { useAuthStore } from '@/store/auth';
    import router from '@/router';

    export default {
        mounted() {
            logout();
        },
        methods: {
            async logout() {
                const authStore = useAuthStore();
                try {
                    await authStore.logout();
                    console.log("After authStore logout().");
                    router.push({ path: '/login' });
                } catch (error) {
                    console.error("B��d wylogowania", error);
                }
            }
        }
    }
</script> -->

<template>
    Logout
</template>