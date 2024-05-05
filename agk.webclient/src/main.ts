import './assets/main.css'

import { createApp } from 'vue'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

// Pinia
import { createPinia } from 'pinia';

// Router
import router from './router'

// Components
import App from './App.vue'

const vuetify = createVuetify({
    components,
    directives,
})

const pinia = createPinia();

createApp(App)
    .use(vuetify)
    .use(pinia)
    .use(router)
    .mount('#app')
