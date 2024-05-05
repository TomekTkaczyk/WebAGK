import { createRouter, createWebHistory } from 'vue-router'

export default createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            component: () => import('@/modules/home/Home.vue'),
        },
        {
            path: '/about',
            component: () => import('@/modules/about/About.vue'),
        },
        //{
        //    path: '/contact',
        //    component: () => import('@/views/Contact.vue'),
        //},
    ],
})
