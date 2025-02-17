<!-- ***************************************************  -->
<!-- * Script section                                  *  -->
<!-- ***************************************************  -->

<script setup lang="ts">
  import { computed, reactive, ref } from 'vue';
  import type IUpdateProfileCommand from './requests/updatename-command.ts';
  import TextInput from "@/components/TextInput.vue";
  import { useAuthStore } from '@/stores/AuthStore';
  import { FormErrors } from '@/types/FormErrors.ts';

  const authStore = useAuthStore();

  const touchedFields = ref({
    firstName: false,
    lastName: false,
  });

  const formData = ref<IUpdateProfileCommand>({
      firstName: authStore.firstName as string || '',
      lastName: authStore.lastName as string || '',
  });

  const errors = new FormErrors();

  async function updateProfile(data: IUpdateProfileCommand) {
    try{
      touchedFields.value.firstName = false;
      touchedFields.value.lastName = false;
      await authStore.updateProfile(data);
      await authStore.getUser();
    } catch (error: any) {
      await errors.CatchApiError("UpdateProfile", error);
    }
  };

  const onChangeFirstname = (value: string) => {
    formData.value.firstName = value;
    touchedFields.value.firstName = true;
    firstNameValidate(value);
  }

  const onChangeLastname = (value: string) => {
    formData.value.lastName = value;
    touchedFields.value.lastName = true;
    lastNameValidate(value);
  }

  const firstNameValidate = (value: string): boolean => {
    errors.Clear("FirstName");
    errors.messages.value.length = 0;
    return errors.Get("FirstName").length === 0;;
  }

  const lastNameValidate = (value: string): boolean => {
    errors.Clear("LastName");
    errors.messages.value.length = 0;
    return errors.Get("LastName").length === 0;;
  }

  const isFormValid = computed(() => {
    return errors.Count === 0 &&
      (touchedFields.value.firstName || touchedFields.value.lastName);
  });


</script>

<!-- ***************************************************  -->
<!-- * Template section                                *  -->
<!-- ***************************************************  -->

<template>
  <div class="updateprofile-form">
    <h4>Zmiana nazwy</h4>
    <form @submit.prevent="updateProfile(formData)">
        <div class="form-group">
              <TextInput v-model="formData.firstName"
                type="text"
                id="firstName"
                label="Imię"
                :messages="errors.Get('Firstname')"
                @input="onChangeFirstname"/>
            </div>
            <div class="form-group">
              <TextInput v-model="formData.lastName"
                type="text"
                id="lastName"
                label="Nazwisko"
                :messages="errors.Get('Lastname')"
                @input="onChangeLastname"/>
            </div>
            <button v-if="isFormValid" type="submit">Wyślij</button>
      </form>
  </div>
</template>

<!-- ***************************************************  -->
<!-- * Style section                                   *  -->
<!-- ***************************************************  -->

<style scoped>
    .form-group {
        margin-bottom: 10px;
    }

    label {
        display: block;
        margin-bottom: 5px;
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
