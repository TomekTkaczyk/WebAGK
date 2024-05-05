<template>
    <div class="navbar">
        <div class="menu">
            <ul>
                <li v-for="item in menuItems" :key="item.id" @mouseover="highlightItem(item.id)" @mouseleave="clearHighlight">
                    <a :href="item.url">{{ item.label }}</a>
                    <ul v-if="item.children && item.children.length">
                        <li v-for="child in item.children" :key="child.id">
                            <a class="submenu-item" :href="child.url">{{ child.label }}</a>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</template>

<script>
    import { computed, ref } from 'vue';

    export default {
        setup() {
            const isMenuOpen = ref(false);
            const highlightedItemId = ref(null);

            const menuItems = computed(() => {
                return [
                    {
                        id: 100,
                        label: 'Home',
                        url: '/',
                    },
                    {
                        id: 200,
                        label: 'About',
                        url: '/about'
                    },
                    {
                        id: 300,
                        label: 'Services',
                        children: [
                            { id: 310, label: 'Submenu 1', url: '/submenu1' },
                            { id: 320, label: 'Submenu 2', url: '/submenu2' }
                        ]
                    },
                    {
                        id: 400,
                        label: 'Contact',
                        children: [
                            { id: 410, label: 'Submenu 1', url: '/submenu1' },
                            { id: 420, label: 'Submenu 2', url: '/submenu2' }
                        ]
                    }
                ]
                    ;
            });

            const toggleMenu = () => {
                isMenuOpen.value = !isMenuOpen.value;
            };

            const highlightItem = () => {
                highlightedItemId.value = null;
            };

            const clearHighlight = () => {
                highlightedItemId.value = null;
            };

            return {
                isMenuOpen,
                highlightedItemId,
                menuItems,
                toggleMenu,
                highlightItem,
                clearHighlight
            };
        }
    };
</script>

<style scoped>
    .navbar {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        background-color: #000;
        color: #fff;
        padding: 10px 20px;
        z-index: 1000;
    }

    .menu {
        display: flex;
    }

    .menu ul {
        list-style-type: none;
        padding: 5px;
        margin: 0px;
        display: flex;
    }

    .menu ul li {
        margin-right: 0px;
        padding: 5px;
        position: relative;
    }

    .menu ul li a {
        text-decoration: none;
        color: #ffff;
        padding: 0px;
    }

    .menu ul li:hover > a {
       color: #ffd800;
    }

    .menu ul li ul {
        position: absolute;
        top: 100%;
        left: 0;
        background-color: #000;
        display: none;
    }

    .menu ul li:hover > ul {
        display: block;
    }

    .menu ul li ul li {
        margin-top: 5px;
    }

    .submenu-item {
        width: auto; /* Ustawiamy szerokoœæ na auto, aby submenu by³o dostosowane do d³ugoœci tekstu */
        white-space: nowrap; /* Zapobiegamy ³amaniu tekstu na dwie linie */
        margin-right: 10px; /* Dostosuj margines miêdzy poszczególnymi pozycjami submenu */
    }
</style>
