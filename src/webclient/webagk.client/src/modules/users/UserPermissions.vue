<!-- ***************************************************  -->
<!-- * Script section                                  *  -->
<!-- ***************************************************  -->

<script setup lang='ts'>

import { onMounted, ref, watchEffect } from 'vue';
import type IUpdatePermissionsCommand from './requests/updatepermissions-command.ts';
import ComboBox from '@/components/ComboBox.vue';
import { FormErrors } from '@/types/FormErrors.ts';
import { useRoute, useRouter } from 'vue-router';
import { useUserStore } from '@/stores/UserStore.ts';
import ErrorHandler from '@/stores/ErrorHandler.ts';

const userStore = useUserStore();
const route = useRoute();
const router = useRouter();
const errorHandle = ErrorHandler;

const props = ref(route.params.id);
const isFormValid = ref(false);

const touchedFields = ref({
  role: false,
  isActive: false,
});

const formData = ref<IUpdatePermissionsCommand & {name: string, permissions: Record<string,string[]>}>({
  id: "",
  name: "",
  role: "",
  isActive: false,
  permissions: {},
});

const allPermissions = ref<Record<string, string[]>>({});

const errors = new FormErrors();

const updatePermissions = async (data: IUpdatePermissionsCommand) => {
  try{
    if(isFormValid){
      await userStore.updatePermissions(data);
      router.push("/UserManager");
    }
  } catch (error) {
    errorHandle(error);
  }
}

const roleOptions = [
  { label: 'Administrator', value: 'Admin' },
  { label: 'Użytkownik', value: 'User' },
];

const isActiveOptions = [
  { label: 'Tak', value: true },
  { label: 'Nie', value: false },
];

watchEffect(() => {
  isFormValid.value = errors.Count === 0 &&
    (touchedFields.value.role || touchedFields.value.isActive);
});

const fetchUser = async () => {
  try{
    const response = await userStore.getUser(props.value as string);
    formData.value.id = response?.data.id;
    formData.value.name = response?.data.name;
    formData.value.role = response?.data.role;
    formData.value.isActive = response?.data.isActive;
    formData.value.permissions = response?.data.permissions || {};


    const permissions = await userStore.getAllPermissions();
    allPermissions.value = permissions?.data || {};
  } catch (error) {
    errorHandle(error);
  }
}

const categoryTranslations: Record<string, string> = {
  "Users": "Moduł: Użytkownik",
  "Employees": "Moduł: Pracownik"
};

const translatePermissionsCategory = (category: string): string => {
  return categoryTranslations[category] || category;
};

const onPermissionsChange = (category: string, claim: string, event: Event) => {
  const checked = (event.target as HTMLInputElement).checked;

  if (!formData.value.permissions[category]) {
    formData.value.permissions[category] = [];
  }

  if (checked) {
    if (!formData.value.permissions[category].includes(claim)) {
      formData.value.permissions[category].push(claim);
    }
  } else {
    formData.value.permissions[category] = formData.value.permissions[category].filter(c => c !== claim);

    if (formData.value.permissions[category].length === 0) {
      delete formData.value.permissions[category];
    }
  }
};

onMounted(fetchUser);

</script>

<!-- ***************************************************  -->
<!-- * Template section                                *  -->
<!-- ***************************************************  -->

<template>
  <div class="updateuser-form">
    <a href="javascript:history.back()" class="back-link">&#8592; Powrót</a>
    <br/>
    <br/>
    <h4>Edycja użytkownika</h4>
    <div class="user-name">{{formData.name}}</div>
    <form @submit.prevent="updatePermissions(formData)">
      <div class="form-group">
        <ComboBox v-model="formData.role"
          label="Rola w systemie"
          :options="roleOptions"
          :messages="errors.Get('Role')"
        />
      </div>
      <div class="form-group">
        <ComboBox v-model="formData.isActive"
          label="Aktywny"
          :options="isActiveOptions"
          :messages="errors.Get('IsActive')"
        />
      </div>

      <div class="permissions-section">
        <h5>Uprawnienia</h5>
        <div v-for="(permissions, category) in allPermissions" :key="category" class="permissions-group">
          <h6 class="permissions-category">{{ translatePermissionsCategory(category) }}</h6>
          <div v-for="permission in permissions" :key="permission" class="permissions-item">
            <label>
              <input type="checkbox"
                    :value="permission"
                    :checked="formData.permissions[category]?.includes(permission)"
                    @change="onPermissionsChange(category, permission, $event)" />
              {{ permission }}
            </label>
          </div>
        </div>
      </div>

      <button v-if="true" type="submit">Zapisz</button>
    </form>
  </div>
  </template>

<!-- ***************************************************  -->
<!-- * Style section                                   *  -->
<!-- ***************************************************  -->

<style scoped>
.updateuser-form {
  max-width: 400px;
  margin: 0 auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.form-group {
  margin-bottom: 15px;
}

.user-name {
  font-size: 25px;
  color: blue;
}

.permissions-section {
  margin-top: 20px;
}

.permissions-group {
  margin-bottom: 15px;
}

.permissions-category {
  text-align: center;
  font-weight: bold;
  margin-bottom: 10px;
}

.permissions-item label {
  display: flex;
  align-items: center;
  gap: 10px;
}

input[type="checkbox"] {
  margin: 0;
}
</style>
