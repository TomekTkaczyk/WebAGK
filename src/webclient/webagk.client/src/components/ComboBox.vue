<!-- ***************************************************  -->
<!-- * Script section                                  *  -->
<!-- ***************************************************  -->

<script setup lang='ts'>

import HintList from './HintList.vue';

interface Props {
  modelValue: string | boolean;
  options: Array<{ label: string; value: string | boolean }>;
  label?: string;
  messages?: string[],
  readonly?: boolean,
}

const props = withDefaults(defineProps<Props>(), {
  label: '',
  messages: () => [],
  readonly: false,
  modelValue: undefined
});

const emit = defineEmits(['update:modelValue']);

const onSelectChange = (event: Event) => {
  const selectedValue = (event.target as HTMLSelectElement).value;
  emit('update:modelValue', selectedValue === 'true' ? true : selectedValue === 'false' ? false : selectedValue);
};
</script>

<!-- ***************************************************  -->
<!-- * Template section                                *  -->
<!-- ***************************************************  -->

<template>
 <div class="combo-box">
    <label>{{ label }}</label>
    <select :value="modelValue" @change="onSelectChange">
      <option v-for="option in options" :key="String(option.value)" :value="option.value">
        {{ option.label }}
      </option>
    </select>
    <HintList :messages="messages"/>
  </div>
</template>

<!-- ***************************************************  -->
<!-- * Style section                                   *  -->
<!-- ***************************************************  -->

<style scoped>

.combo-box {
  display: flex;
  flex-direction: column;
  margin-bottom: 10px;
}

.combo-box select {
  padding: 5px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

</style>
