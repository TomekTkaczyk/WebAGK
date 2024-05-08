import "mdb-vue-ui-kit/css/mdb.min.css";
import "./style.css";

import { createApp } from "vue";
import App from "@/App.vue";

import { createPinia } from 'pinia';

import router, { setupRouter } from "@/router";

const pinia = createPinia();

const app = createApp(App);

setupRouter(router);

app.use(pinia)
   .use(router)
   .mount("#app");
