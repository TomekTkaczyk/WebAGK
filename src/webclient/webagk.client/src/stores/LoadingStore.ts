import { defineStore } from 'pinia';

interface ILoadingState {
  isLoading: boolean;
}

export const useLoadingStore = defineStore("loading",
{
  state: (): ILoadingState => ({
    isLoading: false,
  }),
  actions: {
    startLoading() {
      this.isLoading = true;
    },
    stopLoading() {
      this.isLoading = false;
    },
  }
});
