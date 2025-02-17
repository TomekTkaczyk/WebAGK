import { defineStore } from 'pinia'

interface IAlert {
    message: string;
    type: string;
}

interface IAlertState {
    alert: IAlert | null;
}

export const useAlertStore = defineStore('alert', {
    state: (): IAlertState => ({
        alert: null,
    }),
    actions: {
        success(message: string) {
            this.alert = { message, type: 'alert-success' };
        },
        error(message: string) {
            this.alert = { message, type: 'alert-success' };
        },
        clear() {
            this.alert = null;
        },
    }
});
