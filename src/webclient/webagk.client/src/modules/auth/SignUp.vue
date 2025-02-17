<!-- ***************************************************  -->
<!-- * Script section                                  *  -->
<!-- ***************************************************  -->

<script setup lang="ts">
  import { computed, reactive, ref } from 'vue';
  import type ISignUpCommand from './requests/signup-command';
  import TextInput from "@/components/TextInput.vue";
  import HintList from '@/components/HintList.vue';
  import { useAuthStore } from '@/stores/AuthStore';
  import { FormErrors } from '@/types/FormErrors';
  import { isValidEmail } from '@/helpers/email-validator';

  const authStore = useAuthStore();

  const touchedFields = ref({
    userName: false,
    email: false,
    password: false,
    retypePassword: false
  });

  const formData = ref<ISignUpCommand & {retypePassword: string}>({
    userName: '',
    email: '',
    password: '',
    retypePassword: '',
  });

  const errors = new FormErrors();

  const signUpUser = async (data: ISignUpCommand) => {
    touchedFields.value.userName = false;
    touchedFields.value.email = false;
    touchedFields.value.password = false;
    touchedFields.value.retypePassword = false;
    try {
      if(isFormValid){
        await authStore.signUpUser(data);
      }
    } catch (error: any) {
      await errors.CatchApiError("SignUp", error.response?.data);
    };
  };

  const onChangeUsername = (value: string) => {
    formData.value.userName = value;
    touchedFields.value.userName = true;
    userNameValidate(value);
  };

  const onChangeEmail = (value: string) => {
    formData.value.email = value;
    touchedFields.value.email = true;
    emailValidate(value);
  };

  const onChangePassword = (value: string) => {
    formData.value.password = value;
    touchedFields.value.password = true;
    passwordValidate(value);
  };

  const onChangeRetypePassword = (value: string) => {
      formData.value.retypePassword = value;
      touchedFields.value.retypePassword = true;
      retypePasswordValidate(value);
    };

  const userNameValidate = (value: string): boolean => {
    errors.Clear("UserName");
    errors.messages.value.length = 0;
    const invalidCharsRegex = /[^a-zA-Z0-9_-]/;
    if (touchedFields.value.userName && !value) errors.Add("UserName", "Nazwa użytkownika jest wymagana.");
    if (touchedFields.value.userName && (value.length < 3)) errors.Add("UserName", "Nazwa użytkownika musi mieć co najmniej 3 znaki.");
    if (touchedFields.value.userName && (value.length > 20)) errors.Add("UserName", "Nazwa użytkownika może mieć maksymalnie 20 znaków.");
    if (touchedFields.value.userName && (invalidCharsRegex.test(value))) errors.Add("UserName", "Nazwa użytkownika może zawierać tylko litery, cyfry, myślniki i podkreślniki.");

    return errors.Get("UserName").length === 0;
  }

  const emailValidate = (value: string): boolean => {
    errors.Clear("Email");
    errors.messages.value.length = 0;
    if((touchedFields.value.email) && !isValidEmail(value)) {
      errors.Add("Email","Wymagany prawidłowy adres email.");
    }
    return errors.Get("Email").length === 0;
  }

  const passwordValidate = (value: string): boolean => {
    errors.Clear("Password");
    errors.Clear("RetypePassword");
    errors.messages.value.length = 0;
    if(value !== formData.value.retypePassword) {
      errors.Add("RetypePassword","Powtórzone hasło musi być identyczne.");
    }
    return errors.Get("Password").length === 0;
  }

  const retypePasswordValidate = (value: string): boolean => {
    errors.Clear("Password");
    errors.Clear("RetypePassword");
    errors.messages.value.length = 0;
    if(value !== formData.value.password) {
      errors.Add("RetypePassword","Powtórzone hasło musi być identyczne.");
    }
    return errors.Get("RetypePassword").length === 0;
  }

  const isFormValid = computed(() => {
    return errors.Count === 0 &&
    (touchedFields.value.userName || formData.value.userName.length > 0) &&
    (touchedFields.value.email || formData.value.email.length > 0);
  });
</script>

<!-- ***************************************************  -->
<!-- * Template section                                *  -->
<!-- ***************************************************  -->

<template>
  <div class="signup-form">
      <h2>Rejestracja użytkownika</h2>
      <form @submit.prevent="signUpUser(formData)">
         <div class="form-group">
              <TextInput v-model="formData.userName"
              type="text"
              id="userName"
              label="Nazwa użytkownika"
              :messages="errors.Get('UserName')"
              @input="onChangeUsername"/>
          </div>
          <div class="form-group">
              <TextInput v-model="formData.email"
              type="text"
              id="email"
              label="Adres email"
              :messages="errors.Get('Email')"
              @input="onChangeEmail"/>
          </div>
          <div class="form-group">
              <TextInput v-model="formData.password"
              type="password"
              id="password"
              label="Hasło"
              :messages="errors.Get('Password')"
              @input="onChangePassword"/>
          </div>
          <div class="form-group">
              <TextInput v-model="formData.retypePassword"
              type="password"
              id="retypepassword"
              label="Powtórz hasło"
              :messages="errors.Get('RetypePassword')"
              @input="onChangeRetypePassword"/>
          </div>
          <button v-if="isFormValid" type="submit">Zarejestruj</button>
          <p></p>
          <div><RouterLink to="signin">Chcę się zalogować</RouterLink></div>
          <HintList style="margin-top: 10px;" :messages="errors.messages.value"/>
      </form>
  </div>
</template>

<!-- ***************************************************  -->
<!-- * Style section                                   *  -->
<!-- ***************************************************  -->

<style scoped>
    .signup-form {
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
</style>
