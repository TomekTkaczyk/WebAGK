import { type RouteRecordRaw, type Router, createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '@/store/auth';

const authGuard = (to: any, from: any, next: any) => {
    const authStore = useAuthStore();

    if (to.matched.some((record: any) => record.meta.requiresAuth) && !authStore.isAuthenticated) {
        next('/login');
    }

    switch (to.name) {
        case 'Agents': {
            if (!authStore.hasClaim('agents')) {
                next('/Error403');
            }
            break;
        }
    }

    next();
}

const routes: Array<RouteRecordRaw> = [
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
        name: 'Clients',
        component: () => import('@/modules/clients/Clients.vue'),
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
        component: () => import('@/modules/settlements/Banks.vue'),
        meta: {
            requiresAuth: true,
            requiresClaim: 'banks',
        },
    },
    {
        path: '/commisions',
        name: 'Commisions',
        component: () => import('@/modules/settlements/Commissions.vue'),
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
]



const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
export const setupRouter = (router: Router) => {
    router.beforeEach(authGuard);
}
