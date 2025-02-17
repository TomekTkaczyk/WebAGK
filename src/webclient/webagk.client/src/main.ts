import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.min.css'
import 'bootstrap'

import '@/assets/main.css';

import { createApp } from 'vue';
import App from './WebAGK.vue';
import { createPinia } from 'pinia';
import { useAuthStore } from '@/stores/AuthStore';
import { setRouter } from './infrastructure/httpApiClient';

import router from "@/router";

const pinia = createPinia();

const webagk = createApp(App);

setRouter(router);

webagk
    .use(pinia)
    .use(router);

const authStore = useAuthStore();
authStore.initialize();

webagk.mount('#webagk');


