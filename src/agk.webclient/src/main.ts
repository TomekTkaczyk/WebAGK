import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.min.css'
import 'bootstrap'

import '@/style.css';

import { createApp } from 'vue';
import App from '@/AGK.vue';

import { createPinia } from 'pinia';

import router from '@/router';

const pinia = createPinia();

const agk = createApp(App);

// setupRouter(router);

agk.use(pinia)
   .use(router)
   .mount("#agk");
