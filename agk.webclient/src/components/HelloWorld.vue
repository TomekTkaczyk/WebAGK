<template>
    <div class="weather-component">
        <h1>Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>

        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
        </div>

        <div v-if="!serverReady">
            Waiting for the server to be ready...
        </div>

        <div v-if="post" class="content">
            <table>
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="forecast in post" :key="forecast.date">
                        <td>{{ forecast.date }}</td>
                        <td>{{ forecast.temperatureC }}</td>
                        <td>{{ forecast.temperatureF }}</td>
                        <td>{{ forecast.summary }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent, ref } from 'vue';

    type Forecast = {
        date: string,
        temperatureC: string,
        temperatureF: string,
        summary: string
    };

    //interface Data {
    //    loading: boolean,
    //    post: null | Forecast
    //}

    export default defineComponent({
        setup() {
            const loading = ref(true);
            const serverReady = ref(false);
            const post = ref<Forecast[] | null>(null);

            const fetchData = async () => {
                try {
                    const response = await fetch('weatherforecast');
                    const data = await response.json();
                    post.value = data;
                } catch (error) {
                    console.error('Error fetching data:', error);
                } finally {
                    loading.value = false;
                }
            };

            // Function to ping the server
            const checkServerReady = async () => {
                try {
                    const response = await fetch('ping'); // Adjust the ping URL as needed
                    if (response.ok) {
                        serverReady.value = true;
                    }
                } catch (error) {
                    console.error('Error pinging server:', error);
                }
            };

            // Call checkServerReady every second
            setInterval(checkServerReady, 1000);

            fetchData(); // Fetch data when the component is mounted

            return { loading, serverReady, post };

        },
    })

    //export default _defineComponent({
    //    data(): Data {
    //        return {
    //            loading: false,
    //            post: null
    //        };
    //    },
    //    created() {
    //        // fetch the data when the view is created and the data is
    //        // already being observed
    //        this.fetchData();
    //    },
    //    watch: {
    //        // call again the method if the route changes
    //        '$route': 'fetchData'
    //    },
    //    methods: {
    //        fetchData(): void {
    //            this.post = null;
    //            this.loading = true;

    //            fetch('weatherforecast')
    //                .then(r => r.json())
    //                .then(json => {
    //                    this.post = json as Forecasts;
    //                    this.loading = false;
    //                    return;
    //                });
    //        }
    //    },
    //});
</script>

<style scoped>
th {
    font-weight: bold;
}
tr:nth-child(even) {
    background: #F2F2F2;
}

tr:nth-child(odd) {
    background: #FFF;
}

th, td {
    padding-left: .5rem;
    padding-right: .5rem;
}

.weather-component {
    text-align: center;
}

table {
    margin-left: auto;
    margin-right: auto;
}
</style>