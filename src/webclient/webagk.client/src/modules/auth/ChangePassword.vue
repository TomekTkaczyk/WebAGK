<!-- ***************************************************  -->
<!-- * Script section                                  *  -->
<!-- ***************************************************  -->

<script setup lang="ts">
  import { computed, reactive, ref } from 'vue';
  import type IChangePasswordCommand from './requests/changepassword-command';
  import TextInput from '@/components/TextInput.vue';
  import HintList from '@/components/HintList.vue';
  import { useAuthStore } from '@/stores/AuthStore';
  import { FormErrors } from '@/types/FormErrors';

  const authStore = useAuthStore();

  const touchedFields = ref({
    oldPassword: false,
    newPassword: false,
    retypePassword: false,
  });

  const formData = ref<IChangePasswordCommand & {retypePassword: string}>({
    currentPassword: '',
    newPassword: '',
    retypePassword: '',
  });

  const errors = new FormErrors();

  async function changePassword(data: IChangePasswordCommand) {
    touchedFields.value.oldPassword = false;
    touchedFields.value.newPassword = false;
    touchedFields.value.retypePassword = false;
    try {
      if(isFormValid) {
        await authStore.changePassword(data);
      }
    } catch (error: any) {
      errors.CatchApiError("ChangePassword", error);
    }
  }

  const onChangeCurrentPassword = (value:string ) => {
    formData.value.currentPassword = value;
    touchedFields.value.oldPassword = true;
    oldPasswordValidate(value);
  }

  const onChangeNewPassword = (value: string) => {
    formData.value.newPassword = value;
    touchedFields.value.newPassword = true;
    newPasswordValidate(value);
  };

  const onChangeRetypePassword = (value: string) => {
    formData.value.retypePassword = value;
    touchedFields.value.retypePassword = true;
    retypePasswordValidate(value);
  };

  const oldPasswordValidate = (value: string): boolean => {
    errors.Clear("OldPassword");
    errors.messages.value.length = 0;
    return errors.Get("OldPassword").length === 0;
  }

  const newPasswordValidate = (value: string): boolean => {
    errors.Clear("NewPassword");
    errors.Clear("RetypePassword");
    errors.messages.value.length = 0;
    if(value !== formData.value.retypePassword) {
      errors.Add("RetypePassword", "Powtórzone hasło musi być identyczne.");
    }
    return errors.Get("NewPassword").length === 0;
  }

  const retypePasswordValidate = (value: string): boolean => {
    errors.Clear("RetypePassword");
    errors.messages.value.length = 0;
    if(value !== formData.value.newPassword) {
      errors.Add("RetypePassword", "Powtórzone hasło musi być identyczne.");
    }
    return errors.Get("RetypePassword").length === 0;
  }

  const isFormValid = computed(() => {
    return errors.Count === 0;
  });
</script>


<!-- ***************************************************  -->
<!-- * Template section                                *  -->
<!-- ***************************************************  -->

<template>
  <div>
      <h4>Zmiana hasła</h4>
      <form @submit.prevent="changePassword(formData)">
        <div class="form-group">
          <TextInput v-model="formData.currentPassword"
            type="password"
            id="currentpassword"
            label="Aktualne hasło"
            :messages="errors.Get('OldPassword')"
            @input="onChangeCurrentPassword"/>
        </div>
        <div class="form-group">
          <TextInput v-model="formData.newPassword"
            type="password"
            id="newpassword"
            label="Nowe hasło"
            :messages="errors.Get('NewPassword')"
            @input="onChangeNewPassword"/>
        </div>
        <div class="form-group">
            <TextInput v-model="formData.retypePassword"
            type="password"
            id="retypepassword"
            label="Powtórz hasło"
            :messages="errors.Get('RetypePassword')"
            @input="onChangeRetypePassword"/>
        </div>
        <button v-if="isFormValid" type="submit">Zmień hasło</button>
        <br/>
        <HintList :messages="errors.messages.value"/>
      </form>
  </div>
</template>

<!-- ***************************************************  -->
<!-- * Style section                                   *  -->
<!-- ***************************************************  -->

<style scoped>
    .changepassword-form {
        max-width: 400px;
        margin: 0 auto;
        padding: 20px;
        border: 1px solid #ccc;
        border-radius: 5px;
    }

    .form-group {
        margin-bottom: 15px;
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
