<template>
    <nav class="navbar navbar-dark bg-dark navbar-expand-md" role="navigation">
        <div class="container-fluid">
            <RouterLink class="navbar-brand" to="/">AGK</RouterLink>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav me-auto mb-lg-0">
                    <li class="nav-item">
                        <RouterLink class="nav-link" :to="{name:'Clients', query: { pageNumber: 1, pageSize: 15, textSearch: '' }}">Klienci</RouterLink>
                    </li>
                    <li class="dropdown nav-item">
                        <a class="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            Agenci
                        </a>
                        <ul class="dropdown-menu">
                            <li><RouterLink class="dropdown-item" :to="{name:'Agents'}">Kartoteka</RouterLink></li>
                            <li><RouterLink class="dropdown-item" :to="{name:'Structures'}">Struktura</RouterLink></li>
                        </ul>
                    </li>
                    <li class="nav-item">
                        <RouterLink class="nav-link" to="/contracts">Polisy</RouterLink>
                    </li>
                    <li class="nav-item">
                        <RouterLink to="/cash" class="nav-link" exact="false">Gotówka</RouterLink>
                    </li>
                    <li class="dropdown nav-item">
                        <a class="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            Rozliczenia
                        </a>
                        <ul class="dropdown-menu">
                            <li><RouterLink class="dropdown-item" to="/banks">Banki</RouterLink></li>
                            <li><RouterLink class="dropdown-item" to="/commisions">Prowizje</RouterLink></li>
                        </ul>
                    </li>
                    <li class="nav-item">
                        <RouterLink class="nav-link" to="users">Użytkownicy</RouterLink>
                    </li>
                    <li class="nav-item">
                        <RouterLink class="nav-link" to="settings">Ustawienia</RouterLink>
                    </li>
                </ul>
                <ul class="navbar-nav ms-auto mb-lg-0">
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            Witaj User
                        </a>
                        <ul class="dropdown-menu">
                            <li><RouterLink class="dropdown-item" to="/profile">Mój profil</RouterLink></li>
                            <li><RouterLink class="dropdown-item" to="/logout">Wyloguj</RouterLink></li>
                        </ul>
                    </li>
                </ul>
                <ul class="navbar-nav ms-auto mb-lg-0">
                    <li class="nav-item">
                        <RouterLink class="nav-link" to="login">Zaloguj</RouterLink>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script setup lang="ts">
</script>

<style>
    a[class*=dropdown-item] {
        cursor: pointer;
    }
</style>