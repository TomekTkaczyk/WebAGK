<script setup lang="ts">
    import type { IClient } from '@/types/IClient';
import type { PropType } from 'vue';

    const props = defineProps({
        client: {
            type: Object as () => IClient,
            required: true,
        },
        clickRow: {
            type: Function as PropType<(client: IClient) => void>,
            required: true,
        },
    });

    const handleClick = () => {
        props.clickRow(props.client);
    }
</script>

<template>
    <tr @click="handleClick">
        <td>
            {{client.name}}
        </td>
        <td v-if="client.company">
            NIP : {{client.nip}}
        </td>
        <td v-else>
            PESEL : {{client.pesel}}
        </td>
    </tr>
</template>

<style scoped>
    tr {
        cursor: pointer;
        margin: 5px;
        color: rgb(100, 100, 104);
    }

    tr:hover {
        color: blue;
        box-shadow: 0 2px 2px rgba(0, 0, 0, 0.1);
        border-block-color: blue;
        border: 5px;
    }
</style>