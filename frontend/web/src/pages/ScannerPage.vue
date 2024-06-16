<template>
  <q-page title="Scanner" class="row">
    <div style="display: none">
      <scanner-card />
      <div class="row">
      <div
        v-for="test in tests"
        :key="test"
        class="col-4 q-pa-md text-center"
        :class="`bg-${test}`"
      >
        {{ test }}
      </div>
      <div
        v-for="test in tests"
        :key="test"
        class="col-4 q-pa-md text-center"
        :class="`text-${test}`"
      >
        {{ test }}
      </div>
    </div>
    </div>
    
  </q-page>
</template>

<script setup lang="ts">
import ScannerCard from 'components/ScannerCard.vue'

const tests = [
  'primary',
  'secondary',
  'accent',
  // 'dark',
  // 'dark-page',
  'positive',
  'negative',
  'info',
  'warning',
];

defineOptions({
  name: 'ScannerPage',
});
</script>

<style scoped></style>
