import type { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'Home',
    component: () => import('@/modules/home/Home.vue'),
  },
  {
    path: '/MyProfile/:option?',
    name: 'MyProfile',
    component: () => import('@/modules/auth/MyProfile.vue'),
    meta: {
      requiresAuth: true,
    }
  },
  {
    path: '/UserManager',
    name: 'UserManager',
    component: () => import('@/modules/users/UserManager.vue'),
    meta: {
      requiresAuth: true,
      role: 'Admin',
      permissions: 'Users.UserManager'
    }
  },
  {
    path: '/UserPermissions/:id',
    name: 'UserPermissions',
    component: () => import('@/modules/users/UserPermissions.vue'),
    meta: {
      requiresAuth: true,
      role: 'Admin',
      permissions: ['Users.UserManager', 'Employees.UserManager']
    }
  },
  {
    path: '/Logout',
    name: 'Logout',
    component: () => import('@/modules/auth/Logout.vue'),
    meta: {
      requiresAuth: true,
    }
  },
  {
    path: '/SignUp',
    name: 'SignUp',
    component: () => import('@/modules/auth/SignUp.vue'),
  },
  {
    path: '/SignIn',
    name: 'SignIn',
    component: () => import('@/modules/auth/SignIn.vue'),
  },
  {
    path: '/ConfirmEmail',
    name: 'ConfirmEmail',
    component: () => import('@/modules/auth/ConfirmEmail.vue'),
  },
  {
    path: '/RemindPassword',
    name: 'ReminPassword',
    component: () => import('@/modules/auth/RemindPassword.vue'),
  },
  {
    path: '/Error500',
    name: 'Error500',
    component: () => import('@/modules/errors/Error500.vue')
  },
  {
    path: '/Error503',
    name: 'Error503',
    component: () => import('@/modules/errors/Error503.vue')
  },
  {
    path: '/Error403',
    name: 'Error403',
    component: () => import('@/modules/errors/Error403.vue'),
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'Error404',
    component: () => import('@/modules/errors/Error404.vue')
  },
];

export default routes;
