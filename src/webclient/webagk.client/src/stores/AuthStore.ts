import { defineStore } from 'pinia';

import router from '@/router'

import { httpApiClient } from '@/infrastructure/httpApiClient';

import type ISignInCommand from '@/modules/auth/requests/signin-command';
import type ISignUpCommand from '@/modules/auth/requests/signup-command';
import type IChangePasswordCommand from '@/modules/auth/requests/changepassword-command';
import type IUpdateNameCommand from '@/modules/auth/requests/updatename-command';
import { AlerMessage } from '@/infrastructure/AlertMessage';
import type IAuthState from '@/types/IAuthState';
import ErrorHandler from './ErrorHandler';

const errorHandle = ErrorHandler;

export const useAuthStore = defineStore('auth', {

  state: (): IAuthState => ({
    accessToken: null,
    refreshToken: null,
    isAuthenticated: false,
    isRefreshing: false,
    permissions: {}
  }),

  actions: {
    async logout() {
      try {
        this.isAuthenticated = false;
        this.accessToken = null;
        this.refreshToken = null;
        sessionStorage.removeItem('auth');
        await httpApiClient.post('/users-module/Account/logout');
        router.push('/signin');
      } catch (error) {
        errorHandle(error);
      }
    },

    async signInUser(command: ISignInCommand) {
      try{
        await httpApiClient.post('/users-module/Account/sign-in', command);
        this.isAuthenticated = true;
        await this.getUser();
        router.push('/');
      } catch (error) {
        errorHandle(error);
      }
    },

    async signUpUser(command: ISignUpCommand) {
      try {
        await httpApiClient.post('/users-module/Account/sign-up', command,
          {headers:
            {
              'X-Confirmemail-Url': window.location.origin+"/ConfirmEmail"
            }
          }
        );
        router.push('/signin');
        const alertMessage = new AlerMessage();
        alertMessage.Show('Wysłaliśmy aktywację na twój adres email. Zaloguj się po potwierdzeniu adresu email.')
      } catch (error) {
        errorHandle(error);
      }
    },

    async remindPassword(email: string) {
      try {
        await httpApiClient.post('/users-module/Account/remind-password', null, { params: { email: email}});
        router.push('/signin');
        const alertMessage = new AlerMessage();
        alertMessage.Show('Wysłaliśmy link do zmiany hasła na twój adres email.\nZaloguj się do aplikacji.')
      } catch (error) {
        errorHandle(error);
      }
    },

    async changePassword(command: IChangePasswordCommand) {
      try{
        await httpApiClient.post('/users-module/Account/change-password', command);
        const alertMessage = new AlerMessage();
        alertMessage.Show('Hasło zostało zmienione.');
      } catch (error) {
        errorHandle(error);
      }
    },

    async changeEmail(email: string) {
      try{
        await httpApiClient.post('/users-module/Account/change-email', null,
          {
            params: {email: email},
            headers: {'X-Confirmemail-Url': window.location.origin+"/ConfirmEmail"}
          }
        );
        const alertMessage = new AlerMessage();
        alertMessage.Show('Na podany adres został wysłany link aktywacyjny.');
      } catch (error) {
        errorHandle(error);
      }
    },

    async confirmEmail(token: string) {
      try{
        await httpApiClient.get('users-module/Account/confirm-email?token='+token);
        router.push("/signin");
        const alertMessage = new AlerMessage();
        alertMessage.Show('Adres email został potwierdzony.\nZaloguj się do aplikacji.');
      } catch (error) {
        errorHandle(error);
      }
    },

    async resendConfirmEmail(email: string) {
      try{
        await httpApiClient.post('users-module/Account/resend-confirm-email', email)
      } catch (error) {
        errorHandle(error);
      }
    },

    async updateName(command: IUpdateNameCommand) {
      try{
        await httpApiClient.put('/users-module/Account/update-name', command);
        await this.getUser();
      } catch (error) {
        errorHandle(error);
      }
    },

    async getUser() {
      const userResponse = await httpApiClient.get('/users-module/Account');
      this.id = userResponse.data.id;
      this.name = userResponse.data.name;
      this.email = userResponse.data.email
        ? (userResponse.data.email as string)
        : "";
      this.firstName = userResponse.data.firstName || "";
      this.lastName = userResponse.data.lastName || "";
      this.role = userResponse.data.role ? (userResponse.data.role as string) : "";
      this.permissions = userResponse.data.permissions;

      this.isConfirmed = userResponse.data.isConfirmed;

      sessionStorage.setItem('auth', JSON.stringify({
        accessToken: this.accessToken,
        refreshToken: this.refreshToken,
        isAuthenticated: this.isAuthenticated
      }))
    },

    async getUsers() {
      try{
        return await httpApiClient.get('/users-module');
      } catch (error) {
        errorHandle(error);
      }
    },

    async initialize() {
      const authData = sessionStorage.getItem('auth');
      if(authData) {
        const {accessToken, refreshToken, isAuthenticated} = JSON.parse(authData);
        this.accessToken = accessToken;
        this.refreshToken = refreshToken;
        this.isAuthenticated = isAuthenticated;
        await this.getUser();
      };
    },

    setTokens(accessToken: string, refreshToken: string) {
      this.accessToken = accessToken;
      this.refreshToken = refreshToken;
      this.isAuthenticated = !!accessToken;
    }
  },

  getters: {
    flatPermissions(): Set<string> {
      return new Set(
        Object.entries(this.permissions ?? {}).flatMap(([group, perms]) =>
          (Array.isArray(perms) ? perms : []).map(perm => `${group}.${perm}`)
        )
      );
    }
  },
});

