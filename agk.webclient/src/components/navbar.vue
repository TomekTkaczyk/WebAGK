<template>
    <MDBNavbar expand="sm" dark bg="dark" container>
        <MDBNavbarToggler target="#navbarAgk" @click="collapse = !collapse"></MDBNavbarToggler>
        <MDBCollapse v-model="collapse" id="navbarAgk">
            <MDBNavbarNav class="mb-lg-0">
                <MDBNavbarItem to="/" active>
                    AGK
                </MDBNavbarItem>
                <MDBNavbarItem to="clients">
                    Klienci
                </MDBNavbarItem>
                <MDBDropdown v-if="authStore.hasClaim('agents')" class="nav-item" v-model="agents">
                    <MDBDropdownToggle tag="a"
                                       class="nav-link"
                                       @click="agents=!agents">Agenci</MDBDropdownToggle>
                    <MDBDropdownMenu>
                        <MDBDropdownItem to="/agents">Kartoteka</MDBDropdownItem>
                        <MDBDropdownItem to="/structures">Struktura</MDBDropdownItem>
                    </MDBDropdownMenu>
                </MDBDropdown>
                <MDBNavbarItem to="contracts">
                    Polisy
                </MDBNavbarItem>
                <MDBNavbarItem to="cash">
                    Gotówka
                </MDBNavbarItem>
                <MDBDropdown v-if="authStore.hasClaim('settlements')" class="nav-item" v-model="settlements">
                    <MDBDropdownToggle tag="a"
                                       class="nav-link"
                                       @click="settlements=!settlements">Rozliczenia</MDBDropdownToggle>
                    <MDBDropdownMenu>
                        <MDBDropdownItem to="/banks">Banki</MDBDropdownItem>
                        <MDBDropdownItem to="/commisions">Prowizje</MDBDropdownItem>
                    </MDBDropdownMenu>
                </MDBDropdown>
                <MDBNavbarItem to="users">
                    Użytkownicy
                </MDBNavbarItem>
                <MDBNavbarItem to="settings">
                    Ustawienia
                </MDBNavbarItem>
                <MDBNavbarItem to="profile">
                    Profil
                </MDBNavbarItem>
            </MDBNavbarNav>
        </MDBCollapse>
    </MDBNavbar>
</template>

<script setup lang="ts">
    import {
        MDBNavbar,
        MDBNavbarToggler,
        MDBNavbarBrand,
        MDBNavbarNav,
        MDBNavbarItem,
        MDBCollapse,
        MDBDropdown,
        MDBDropdownToggle,
        MDBDropdownMenu,
        MDBDropdownItem
    } from 'mdb-vue-ui-kit';

    import { ref } from 'vue';

    import { useAuthStore } from '@/store/auth';

    const authStore = useAuthStore();
    const agents = ref(false);


    const collapse = ref(false);
    const dropdown7 = ref(false);

    const expandDropdown = (dropdown: string) => {
        switch (dropdown) {
            case 'dropdown7':
                dropdown7.value = true;
                break;
            default:
                break;
        }
    };

    authStore.login();

</script>

<style>
    a[class*=dropdown-item] {
        cursor: pointer;
    }
</style>