<script setup lang="ts">
    import { defineProps } from 'vue';
    import router from '@/router'
    import useRestApi from '@/store/FakeRestApi';

    import type { IClient } from '@/types/IClient';
    import type { IPageInfo } from '@/types/IPageInfo';

    import SearchInput from '@/components/SearchInput.vue'
    import ClientItem from '@/modules/clients/ClientListItem.vue';
    import PageNavigator from '@/components/PageNavigator.vue'

    const props = defineProps<{
        pageNumber: {
            type: number;
            required: false;
            default: 1;
        },
        pageSize: {
            type: number;
            required: false;
            default: 15;
        },
        textSearch: {
            type: string;
            required: false;
            default: '';
        },
    }>();

    const restApiStore = useRestApi();

    const loadPage = async () => {
        try {
            const pageNumber: number = props.pageNumber !== undefined ? Number(props.pageNumber) : 1;
            const pageSize: number = props.pageSize !== undefined ? Number(props.pageSize) : 15;
            const textSearch: string = props.textSearch !== undefined ? String(props.textSearch) : '';

            const pageInfo: IPageInfo = await restApiStore.getClients(
                pageNumber,
                pageSize,
                textSearch
            );

            return pageInfo;
        } catch (error) {
            console.error('Error loading page:', error);
            throw error;
        }
    };

    loadPage().then((result: IPageInfo) => {
        const clients: IClient[] = result.collection as IClient[];
        console.log(clients);
    }).catch((error) => {
        console.error('Error loading page:', error);
    });



    //const clients: IClient[] = [
    //    {
    //        id: 1,
    //        name: 'Kowalski',
    //        pesel: '987654321',
    //        nip: null,
    //        company: false,
    //    },
    //    {
    //        id: 2,
    //        name: 'Nowak',
    //        pesel: null,
    //        nip: '1234567890',
    //        company: true,
    //    },
    //    {
    //        id: 3,
    //        name: 'Kwiatkowski',
    //        pesel: '987654321',
    //        nip: null,
    //        company: false,
    //    },
    //];

    function clickRow(client: IClient) {
        console.log(`route to ${client.name} /clients/${client.id}`);
        router.push(`/clients/${client.id}`);
    }

</script>

<template>
    <h1>Lista klientów</h1>
    <div class="row">
        <div class="col">
            <SearchInput />
        </div>
        <div class="col" align="right">
            <PageNavigator />
        </div>
    </div>
    <div class="table-responsive">
        <table class="table table-sm table-hover">
            <thead class="table-dark">
                <tr>
                    <th>Nazwisko</th>
                    <th>PESEL/NIP</th>
                </tr>
            </thead>
            <tbody>
                <ClientItem v-for="client of clients"
                            :key="client.id"
                            :client="client"
                            :clickRow="clickRow" />
            </tbody>
        </table>
    </div>
</template>

<style scoped>
    .clients {
    }
</style>