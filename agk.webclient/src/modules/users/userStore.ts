import { defineStore } from "pinia"

function apiLogin(login: string, password: string) {
    return Promise.resolve({ isAdmin: true });
}

export const useUserStore = defineStore({
    id: "user",

    state: () => ({
        name: "Admin",
        isAdmin: true,
    }),

    actions: {
        logout() {
            this.$patch({
                name: "",
                isAdmin: false,
            })
        },
        async login(login: string, password: string) {
            const userDate = await apiLogin(login, password);

            this.$patch({
                name: login,
                ...userDate,
            })
        },
    },
});

