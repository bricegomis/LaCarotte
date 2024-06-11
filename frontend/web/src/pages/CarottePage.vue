<template>
  <q-page title="Carottes" style="background-color: #8ec8e4">
    <carotte-card />
  </q-page>
</template>

<script setup lang="ts">
import CarotteCard from 'components/CarotteCard.vue';

defineOptions({
  name: 'CarottePage',
});
</script>
