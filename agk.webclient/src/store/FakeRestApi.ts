import { defineStore } from 'pinia';
import { type IPageInfo } from '@/types/IPageInfo';

export default defineStore({
    id: 'fakeRestApi',
    actions: {
        getClients(pageNumber: number, pageSize: number, textSearch: string) {
            return new Promise<IPageInfo>((resolve, reject) => {
            });
        },
    },
});
