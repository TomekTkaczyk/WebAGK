<!-- ***************************************************  -->
<!-- * Script section                                  *  -->
<!-- ***************************************************  -->

<script setup lang="ts">
  import { computed, reactive, ref, watchEffect } from 'vue';
  import type ISignInCommand from './requests/signin-command';
  import TextInput from "@/components/TextInput.vue";
  import HintList from '@/components/HintList.vue';
  import { useAuthStore } from '@/stores/AuthStore';
  import { FormErrors } from '@/types/FormErrors';
  import { isValidEmail } from '@/helpers/email-validator';

  const authStore = useAuthStore();

  const touchedFields = ref({
    identifier: false,
    password: false,
  });

  const formData = ref<ISignInCommand>({
      identifier: '',
      password: '',
  });

  const errors = new FormErrors();

  async function signInUser(data: ISignInCommand) {
    touchedFields.value.identifier = false;
    touchedFields.value.password = false;
    try {
      if(isFormValid) {
        await authStore.signInUser(data);
      }
    } catch (error: any) {
      await errors.CatchApiError("SignIn", error);
    };
  };

  const onChangeIdentifier = (value: string) => {
    formData.value.identifier = value;
    touchedFields.value.identifier = true;
    identifierValidate(value);
  };

  const onChangePassword = (value: string) => {
    formData.value.password = value;
    touchedFields.value.password = true;
    passwordValidate(value);
  };

  const identifierValidate = (value: string): boolean => {
    errors.Clear("Identifier");
    errors.messages.value.length = 0;
    const invalidCharsRegex = /[^a-zA-Z0-9_-]/;
    const generalAtRegex = /@/;
    if (touchedFields.value.identifier && !value) {
      errors.Add("Identifier", "Wymagany identyfikator lub email użytkownika.");
    }
    if(generalAtRegex.test(value)){
      if((touchedFields.value.identifier) && !isValidEmail(value)) {
        errors.Add("Identifier","Wymagany prawidłowy adres email.");
      }
    } else {
      if (touchedFields.value.identifier && (value.length < 3)) errors.Add("Identifier", "Identyfikator użytkownika musi mieć co najmniej 3 znaki.");
      if (touchedFields.value.identifier && (value.length > 20)) errors.Add("Identifier", "Identyfikator użytkownika może mieć maksymalnie 20 znaków.");
      if (touchedFields.value.identifier && (invalidCharsRegex.test(value))) errors.Add("Identifier", "Identyfikator użytkownika może zawierać tylko litery, cyfry, myślniki i podkreślniki.");
    }

    return errors.Get("Identifier").length === 0;
  }

  const passwordValidate = (value: string): boolean => {
    errors.Clear("Password");
    errors.messages.value.length = 0;
    return errors.Get("Password").length === 0;
  }

  const isFormValid = computed(() => {
    return errors.Count === 0 &&
      (touchedFields.value.identifier || formData.value.identifier.length > 0);
  });

</script>

<!-- ***************************************************  -->
<!-- * Template section                                *  -->
<!-- ***************************************************  -->

<template>
  <div class="signin-form">
      <h2>Logowanie użytkownika</h2>
      <form @submit.prevent="signInUser(formData)">
        <div class="form-group">
          <TextInput v-model="formData.identifier"
            type="text"
            id="identifier"
            label="Nazwa użytkownika (login/email)"
            :messages="errors.Get('Identifier')"
            @input="onChangeIdentifier"/>
        </div>
        <div class="form-group">
          <TextInput v-model="formData.password"
            type="password"
            id="password"
            label="Hasło"
            :messages="errors.Get('Password')"
            @input="onChangePassword"/>
        </div>
        <button v-if="isFormValid" type="submit">Zaloguj</button>
        <p></p>
        <div><RouterLink to="signup">Chcę się zarejestrować</RouterLink></div>
        <div><RouterLink to="remindpassword">Nie pamiętam hasła</RouterLink></div>
        <HintList style="margin-top: 10px;" :messages="errors.messages.value"/>
      </form>
  </div>
</template>

<!-- ***************************************************  -->
<!-- * Style section                                   *  -->
<!-- ***************************************************  -->

<style scoped>
.signin-form {
  max-width: 400px;
    margin: 0 auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
}

.form-group {
    margin-bottom: 15px;
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
}
</style>
