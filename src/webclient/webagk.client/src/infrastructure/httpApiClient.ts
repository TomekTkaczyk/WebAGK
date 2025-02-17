import { useAuthStore } from '@/stores/AuthStore';
import { useLoadingStore } from '@/stores/LoadingStore';
import axios, { type AxiosRequestConfig } from 'axios';
import { type Router } from 'vue-router';


let router: Router | null = null;
const setRouter = (r: Router) => { router = r; };

const httpApiClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true,
});


interface FailedQueueItem {
  resolve: (value?: any) => void;
  reject: (reason?: any) => void;
}

let failedQueue: FailedQueueItem[] = [];

const processQueue = (error: any, token: string | null = null): void => {
  failedQueue.forEach(prom => {
    if (error) {
      prom.reject(error);
    } else {
      prom.resolve(token);
    }
  });
  failedQueue = [];
};


httpApiClient.interceptors.request.use(
  config => {
    const loadingStore = useLoadingStore();
    loadingStore.startLoading();
    return config;
  },
  error => {
    const LoadingStore = useLoadingStore();
    LoadingStore.stopLoading();
    return Promise.reject(error);
  }
);

httpApiClient.interceptors.response.use(
  response => {
    const LoadingStore = useLoadingStore();
    LoadingStore.stopLoading();
    return response;
  },
  async error => {
    const LoadingStore = useLoadingStore();
    LoadingStore.stopLoading();
    const originalRequest = error.config as AxiosRequestConfig;

    if(error.response){
      const status = error.response.status;

      if (status === 401 && !originalRequest._retry) {
        const authStore = useAuthStore();
        if(authStore.isAuthenticated) {
          originalRequest._retry = true;
          if (authStore.isRefreshing) {
            return new Promise<any>((resolve, reject) => {
              failedQueue.push({ resolve, reject });
            }).then(() => httpApiClient(originalRequest));
          }
          authStore.isRefreshing = true;
          try {
            await httpApiClient.post('/users-module/Account/refresh-token');
            processQueue(null);
            return httpApiClient(originalRequest);
          } catch (refreshError) {
            processQueue(refreshError);
            await authStore.logout();
            return Promise.reject(refreshError);
          } finally {
            authStore.isRefreshing = false;
          }
        }
      }

      if(status === 403) {
        if (router) {
          router.push("/Error403");
        } else {
          console.error("Router is not defined.");
        }
        return new Promise(() => {});
      }
    }

    return Promise.reject(error);
  });

export { httpApiClient, setRouter };
