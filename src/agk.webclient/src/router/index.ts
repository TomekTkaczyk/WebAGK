
// import { useAuthStore } from '@/store/auth';

// const authGuard = (to: any, from: any, next: any) => {
//     const authStore = useAuthStore();

//     if (to.matched.some((record: any) => record.meta.requiresAuth) && !authStore.isAuthenticated) {
//         next('/login');
//         return;
//     }

//     switch (to.name) {
//         case 'Agents': {
//             if (!authStore.hasClaim('agents')) {
//                 next('/Error403');
//             }
//             break;
//         } 
//         case 'Structures': {
//             if (!authStore.hasClaim('agents')) {
//                 next('/Error403');
//             }
//             break;
//         }
//         case 'Banks': {
//             if (!authStore.hasClaim('banks')) {
//                 next('/Error403');
//             }
//             break;
//         }
//         case 'Commisions': {
//             if (!authStore.hasClaim('commisions')) {
//                 next('/Error403');
//             }
//             break;
//         } 
//         case 'Users': {
//             if (!authStore.hasClaim('users')) {
//                 next('/Error403');
//             }
//             break;
//         } 
//         case 'Settings': {
//             if (!authStore.hasClaim('admin')) {
//                 next('/Error403');
//             }
//             break;
//         } 
//     }

//     next();
// }

import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/Error403',
            name: 'Error403',
            component: () => import('@/modules/errors/Error403.vue'),
        },
        {
            path: '/',
            name: 'Home',
            component: () => import('@/modules/home/Home.vue'),
        },
        {
            path: '/clients',
            children: [
                {
                    path: "",
                    name: "Clients",
                    component: () => import('@/modules/clients/Clients.vue'),
                    props: (route) => ({
                        pageNumber: route.query.pageNumber ? parseInt(route.query.pageNumber as string) : 1,
                        pageSize: route.query.pageSize ? parseInt(route.query.pageSize as string) : 15,
                        textSearch: route.query.textSearch ? parseInt(route.query.textSearch as string) : '',
                    }),
                },
                {
                    path: ":id",
                    name: "Client",
                    component: () => import('@/modules/clients/Client.vue'),
                    props: true,
                },                
            ],
            meta: {
                requiresAuth: true,
            },
        },
        {
            path: '/clients/:id',
            name: 'Client',
            component: () => import('@/modules/clients/Client.vue'),
            props: true,
            meta: {
                requiresAuth: true,
            },
        },
        {
            path: '/agents',
            name: 'Agents',
            component: () => import('@/modules/agents/Agents.vue'),
            meta: {
                requiresAuth: true,
                requiresClaim: 'agents',
            },
        },
        {
            path: '/structures',
            name: 'Structures',
            component: () => import('@/modules/agents/Structures.vue'),
            meta: {
                requiresAuth: true,
                requiresClaim: 'agents',
            },
        },
       {
            path: '/contracts',
            name: 'Contracts',
            component: () => import('@/modules/contracts/Contracts.vue'),
            meta: {
                requiresAuth: true,
            },
        },
        {
            path: '/cash',
            name: 'Cash',
            component: () => import('@/modules/cash/Cash.vue'),
            meta: {
                requiresAuth: true,
            },
        },
        {
            path: '/banks',
            name: 'Banks',
            component: () => import('@/modules/settlement/Banks.vue'),
            meta: {
                requiresAuth: true,
                requiresClaim: 'banks',
            },
        },
        {
            path: '/commisions',
            name: 'Commisions',
            component: () => import('@/modules/settlement/Commissions.vue'),
            meta: {
                requiresAuth: true,
                requiresClaim: 'commisions',
            },
        },
        {
            path: '/users',
            name: 'Users',
            component: () => import('@/modules/users/Users.vue'),
            meta: {
                requiresAuth: true,
                requiresClaim: 'users',
            },
        },
        {
            path: '/settings',
            name: 'Settings',
            component: () => import('@/modules/settings/Settings.vue'),
            meta: {
                requiresAuth: true,
                requiresClaim: 'settings',
            },
        },
        {
            path: '/profile',
            name: 'Profile',
            component: () => import('@/modules/users/Profile.vue'),
            meta: {
                requiresAuth: true,
            },
        },
        {
            path: '/login',
            name: 'Login',
            component: () => import('@/modules/users/Login.vue'),
        },
        {
            path: '/logout',
            name: 'Logout',
            component: () => import('@/modules/users/Logout.vue'),
        },
    ],
});

export default router;

