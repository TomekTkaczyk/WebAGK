import pluginVue from 'eslint-plugin-vue'
import vueTsEslintConfig from '@vue/eslint-config-typescript'

export default [
  {
    name: 'app/files-to-lint',
    files: ['**/*.{ts,mts,tsx,vue}'],
  },

  {
    name: 'app/files-to-ignore',
    ignores: ['**/dist/**', '**/dist-ssr/**', '**/coverage/**'],
  },
  {
    "vue/multi-word-component-names": ["error", {
      "ignores": [
        "Error403"
    ]}]
  },
  ...pluginVue.configs['flat/essential'],
  ...vueTsEslintConfig(),
]
