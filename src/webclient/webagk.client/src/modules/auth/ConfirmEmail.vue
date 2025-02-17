<!-- ***************************************************  -->
<!-- * Script section                                  *  -->
<!-- ***************************************************  -->

<script setup lang='ts'>
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import { useAuthStore } from '@/stores/AuthStore';
import type { ApiError } from '@/infrastructure/errors/ApiError';

const route = useRoute();
const confirmToken = route.query.token as string;
const authStore = useAuthStore();

const isLoading = ref(false);
const isConfirmed = ref(false);
const errorMessage = ref<string | null>(null);
const buttonVisible = ref<boolean>(false);

const clickFunction = ref<() => Promise<void>>(() => Promise.resolve());

const confirmEmail = async () => {
  if (!confirmToken) {
    errorMessage.value = 'Brak wymaganych danych do potwierdzenia e-maila.';
    return;
  }
  isLoading.value = true;
  errorMessage.value = null;
  try{
    await authStore.confirmEmail( confirmToken );
    isConfirmed.value = true;
  } catch (error) {
    switch((error as ApiError).code){
      case "email_already_confirmed": {
        errorMessage.value = 'Adres email jest już potwierdzony.';
        break;
      }
      case "invalid_email_token": {
        errorMessage.value = 'Żądanie straciło ważność.\nZaloguj się do aplikacji ustaw ponownie adres email.\nŻądanie zostanie wysłane ponownie.';
        break;
      }
      default:{
        errorMessage.value = 'Wystąpił błąd podczas potwierdzania e-maila.';
      }
    }
  } finally {
    isLoading.value = false;
  }
};

clickFunction.value = confirmEmail;

confirmEmail();

</script>

<!-- ***************************************************  -->
<!-- * Template section                                *  -->
<!-- ***************************************************  -->

<template>
  <div class="confirm-form" >
    <div class="confirm-email">
      <div v-if="isLoading">Potwierdzanie e-maila...</div>
      <div v-else-if="isConfirmed">E-mail został pomyślnie potwierdzony!</div>
      <div v-else-if="errorMessage">
        <div class="error">
          {{ errorMessage }}
        </div>
        <br/>
        <button v-if="buttonVisible" v-on:click="clickFunction">Wyślij ponownie</button>
      </div>
      <div v-else>Oczekiwanie na potwierdzenie e-maila...</div>
    </div>
  </div>
</template>

<!-- ***************************************************  -->
<!-- * Style section                                   *  -->
<!-- ***************************************************  -->

<style scoped>
.confirm-form {
  max-width: 400px;
    margin: 0 auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
}
.confirm-email {
  text-align: center;
  margin: 20px;
}

.error {
  color: red;
  font-weight: bold;
  white-space: pre-line;
}

.button {
        padding: 10px 20px;
        background-color: #007bff;
        color: #fff;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        margin-top: 10px;
        margin-bottom: 5px;
    }
</style>
