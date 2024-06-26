import { defineStore } from 'pinia';

export const useAuthStore = defineStore({
    id: 'auth',
    state: () => ({
        isAuthenticated: false,
        claims: [] as string[],
    }),
    actions: {
        login() {
            return new Promise<void>((resolve, reject) => {
            // logika logowania użytkownika
                this.isAuthenticated = true;
                this.claims.push('clients');
                this.claims.push('agents');
                this.claims.push('settlement');
                this.claims.push('banks');
                this.claims.push('commisions');
                this.claims.push('admin');
                resolve();
            });
        },
        logout() {
            return new Promise<void>((resolve, reject) => {
                // logika wylogowania użytkownika
                this.isAuthenticated = false;
                this.claims = [];
                resolve();
            });
        },
        hasClaim(claim: string) {
            return this.isAuthenticated && this.claims.includes(claim);
        }
    },
});
