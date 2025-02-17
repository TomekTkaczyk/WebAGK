import { createRouter, createWebHistory } from 'vue-router'
import routes from '@/router/routes'
import { useAuthStore } from '@/stores/AuthStore';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore();
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!authStore.isAuthenticated) {
      if (to.name === 'SignUp') {
        return next({ name: 'SignUp' });
      }
      return next({ name: 'SignIn' });
    }
  }

  const requiredRole = to.meta.role as string | undefined;
  let requiredPermissions: string[] = [];
  if (typeof to.meta.permissions === 'string') {
    requiredPermissions = [to.meta.permissions];
  } else if (Array.isArray(to.meta.permissions)) {
    requiredPermissions = to.meta.permissions;
  }

  const hasRole = requiredRole ? requiredRole === authStore.role : true;
  const hasPermissions = requiredPermissions.length === 0 || requiredPermissions.some(perm => authStore.flatPermissions.has(perm));

  if (!(hasRole || hasPermissions)) {
    return next({ name: 'Error403' });
  }

  next();
});

export default router;
