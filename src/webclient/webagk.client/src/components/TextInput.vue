<!-- ***************************************************  -->
<!-- * Script section                                  *  -->
<!-- ***************************************************  -->

<script setup lang='ts'>

  import HintList from '@/components/HintList.vue';

  interface Props {
    modelValue: string | boolean,
    id: string,
    label?: string,
    placeholder?: string,
    messages?: string[],
    readonly?: boolean,
    type: 'text' | 'password'
  }

  const props = withDefaults(defineProps<Props>(), {
    modelValue: '',
    label: '',
    messages: () => [],
    placeholder: '',
    readonly: false,
    type: 'text'
  });

  const emit = defineEmits(['input']);
</script>

<!-- ***************************************************  -->
<!-- * Template section                                *  -->
<!-- ***************************************************  -->

<template>
  <label :for="props.id">{{props.label}}</label>
  <input :type="props.type"
    :id="props.id"
    :value="props.modelValue"
    :placeholder="props.placeholder"
    @input="$emit('input',($event.target as HTMLInputElement).value)"/>
  <HintList :messages="props.messages"/>
</template>

<!-- ***************************************************  -->
<!-- * Style section                                   *  -->
<!-- ***************************************************  -->

<style scoped>

    input {
        width: 100%;
        padding: 8px;
        border: 1px solid #ccc;
        border-radius: 5px;
        margin-bottom: 1px;
    }
</style>
