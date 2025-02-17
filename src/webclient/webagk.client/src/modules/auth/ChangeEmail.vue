<!-- ***************************************************  -->
<!-- * Script section                                  *  -->
<!-- ***************************************************  -->

<script setup lang="ts">
  import { computed, reactive, ref } from 'vue';
  import TextInput from "@/components/TextInput.vue";
  import HintList from '@/components/HintList.vue';
  import { useAuthStore } from '@/stores/AuthStore';
  import { isValidEmail } from '@/helpers/email-validator'
  import { FormErrors } from '@/types/FormErrors';

  const authStore = useAuthStore();

  const touchedFields = ref({
    email: false,
  });

  const formData = ref(authStore.email as string);

  const errors = new FormErrors();

  const emailLabel = computed(() => {return authStore.isConfirmed ? "Email" : "Email (nie potwierdzony)"});

  async function changeEmail(email: string) {
    touchedFields.value.email = false;
    try {
      if(isValidEmail(formData.value)) {
        await authStore.changeEmail(email);
        touchedFields.value.email = false;
      }
    } catch (error) {
      await errors.CatchApiError("ChangeEmail", error);
    }
  };

  const onChangeEmail = (value: string) => {
    formData.value = value;
    touchedFields.value.email = true;
    emailValidate(value);
  }

  const emailValidate = (value: string): boolean => {
    errors.Clear("Email");
    errors.messages.value.length = 0;
    if((touchedFields.value.email) && !isValidEmail(value)) {
      errors.Add("Email","Wymagany prawidłowy adres email.");
    }
    return errors.Get("Email").length === 0;
  };

  const buttonVisible = computed(() => {
    return isValidEmail(formData.value) && (!authStore.isConfirmed || authStore.email !== formData.value);
  });

</script>

<!-- ***************************************************  -->
<!-- * Template section                                *  -->
<!-- ***************************************************  -->

<template>
  <div>
      <h4>Zmiana adresu email</h4>
      <form @submit.prevent="changeEmail(formData)">
        <div class="form-group">
              <TextInput v-model="formData"
                type="text"
                id="email"
                :label="emailLabel"
                :messages="errors.Get('Email')"
                @input="onChangeEmail"/>
          </div>
          <button v-if="buttonVisible" type="submit">Wyślij</button>
          <HintList :messages="errors.messages.value"/>
        </form>
  </div>
</template>

<!-- ***************************************************  -->
<!-- * Style section                                   *  -->
<!-- ***************************************************  -->

<style scoped>
    .changeemail-form {
        max-width: 400px;
        margin: 0 auto;
        padding: 20px;
        border: 1px solid #ccc;
        border-radius: 5px;
    }

  .form-group {
      margin-bottom: 1px;
      margin-top: 5px;
  }

  label {
      display: block;
      margin-bottom: 5px;
  }

  input {
      width: 100%;
      padding: 8px;
      border: 1px solid #ccc;
      border-radius: 5px;
  }

  button {
      padding: 10px 20px;
      background-color: #007bff;
      color: #fff;
      border: none;
      border-radius: 5px;
      cursor: pointer;
      margin-top: 10px;
      margin-bottom: 5px;
  }

  .small-text {
    font-size: 0.85em;
    margin-top: 2px;
  }

</style>
