<!-- ***************************************************  -->
<!-- * Script section                                  *  -->
<!-- ***************************************************  -->

<script setup lang='ts'>
import { onMounted, reactive, ref } from 'vue';
import { useRouter } from 'vue-router';
import { FormErrors } from '@/types/FormErrors';
import { useUserStore } from '@/stores/UserStore';
import ErrorHandler from '@/stores/ErrorHandler';
import UserItem from './UserItem.vue';

const userStore = useUserStore();
const router = useRouter()

const errors = new FormErrors();
const users = ref<any[]>([]);
const isLoading = ref(false);
const errorHandle = ErrorHandler;

const fetchUsers = async () => {
  isLoading.value = true;
  errors.ClearAll();

  try{
    const response = await userStore.getUsers();
    users.value = response?.data || [];
  } catch (error) {
    errorHandle(error);
  } finally {
    isLoading.value = false;
  }
};

const userPermissions = (id: string) => {
  router.push({name: 'UserPermissions', params: {id: id}});
}

onMounted(fetchUsers);

</script>

<!-- ***************************************************  -->
<!-- * Template section                                *  -->
<!-- ***************************************************  -->

<template>
  <div class="user-manager">
    <h1>Lista użytkowników</h1>
    <div v-if="isLoading" class="loading">
      Ładowanie danych...
    </div>
    <div v-else-if="errors.Count !== 0" class="error">
      {{ errors.messages }}
    </div>
    <div v-else class="user-list">
      <li v-for="user in users" :key="user.id" @click="userPermissions(user.id)">
        <UserItem :user="user" />
      </li>
    </div>
  </div>
</template>

<!-- ***************************************************  -->
<!-- * Style section                                   *  -->
<!-- ***************************************************  -->

<style scoped>
.user-manager {
  max-width: 500px;
  margin: 0 auto;
  padding: 20px;
}

.loading {
  font-size: 18px;
  color: #007bff;
}

.error {
  color: #ff4d4d;
  font-size: 16px;
}

.user-list {
  list-style: none;
  padding: 0;
}

.user-item {
  border: 1px solid #ddd;
  padding: 10px;
  margin-bottom: 10px;
  border-radius: 5px;
  cursor: pointer;
}
</style>
