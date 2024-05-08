import { defineStore } from 'pinia';

export const useAuthStore = defineStore({
    id: 'auth',
    state: () => ({
        isAuthenticated: false,
        claims: [] as string[],
    }),
    actions: {
        login() {
            // logika logowania u¿ytkownika
            this.isAuthenticated = true;
            this.claims.push('agents');
        },
        logout() {
            // logika wylogowania u¿ytkownika
            this.isAuthenticated = false;
            this.claims = [];
        },
        hasClaim(claim: string) {
            return this.isAuthenticated && this.claims.includes(claim);
        }
    },
});
