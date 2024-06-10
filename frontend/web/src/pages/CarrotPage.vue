<template>
  <q-page title="Carotte Items" style="background-color: #8ec8e4">
    <carotte-item-list-cards />
  </q-page>
</template>

<script setup lang="ts">
import CarotteListCards from 'components/CarotteListCards.vue';

defineOptions({
  name: 'CarottePage',
});
</script>
