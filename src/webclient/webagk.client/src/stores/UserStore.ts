import { defineStore } from 'pinia';
import { httpApiClient } from '@/infrastructure/httpApiClient';
import ErrorHandler from './ErrorHandler';

import { AlerMessage } from '@/infrastructure/AlertMessage';
import type IUpdatePermissionsCommand from '@/modules/users/requests/updatepermissions-command';

const errorHandle = ErrorHandler;

export const useUserStore = defineStore('users',{
  actions: {
    async getUsers() {
      try{
        return await httpApiClient.get('/users-module');
      } catch (error) {
        errorHandle(error);
      }
    },

    async getUser(id: string) {
      try{
        return await httpApiClient.get(`/users-module/${id}`);
      } catch (error) {
        errorHandle(error);
      }
    },

    async getAllPermissions() {
      try {
        return await httpApiClient.get(`/permissions`);
      } catch (error) {
        errorHandle(error);
      }
    },

    async updatePermissions(command: IUpdatePermissionsCommand) {
      try{
        await httpApiClient.post('/users-module/update-permissions', command);
        const alertMessage = new AlerMessage();
        alertMessage.Show('Uprawnienia zostały zaktualizowane.');
      } catch (error) {
        errorHandle(error);
      }
    },
  }
});
