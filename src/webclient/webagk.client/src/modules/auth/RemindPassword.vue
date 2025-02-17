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

    const formData = ref('');

    const errors = new FormErrors();

    async function remindPassword(email: string) {
      try {
        if(isFormValid) {
          await authStore.remindPassword(email);
        }
      } catch (error) {
          await errors.CatchApiError("RemindPassword", error);
      }
    };

    const onChangeEmail = (value: string) => {
      formData.value = value;
      emailValidate(value);
    }

    const emailValidate = (value: string): boolean => {
      errors.Clear("Email");
      errors.messages.value.length = 0;
      if(!isValidEmail(value)){
        errors.Add("Email", "Wymagany prawidłowy adres eamil.");
      };
      return errors.Get("Email").length === 0;
    }

    const isFormValid = computed(() => {
        return errors.Count === 0 &&
          (formData.value.length > 0);
    });

</script>

<!-- ***************************************************  -->
<!-- * Template section                                *  -->
<!-- ***************************************************  -->

<template>
  <div class="remindpassword-form">
      <h2>Reset hasła</h2>
      <form @submit.prevent="remindPassword(formData)">
        <div class="form-group">
              <TextInput v-model="formData"
                type="text"
                id="email"
                label="Email"
                :messages="errors.Get('Email')"
                @input="onChangeEmail"/>
            </div>
          <button v-if="isFormValid" type="submit">Wyślij</button>
          <br/>
          <div><RouterLink to="signin">Chcę się zalogować</RouterLink></div>
          <div><RouterLink to="signup">Chcę się zarejestrować</RouterLink></div>
          <HintList :messages="errors.messages.value"/>
      </form>
  </div>
</template>

<!-- ***************************************************  -->
<!-- * Style section                                   *  -->
<!-- ***************************************************  -->

<style scoped>
    .remindpassword-form {
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
        margin-top: 10px;
        margin-bottom: 5px;
    }
</style>
