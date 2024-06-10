<template>
  <q-page title="Carrot Items" style="background-color: #8ec8e4">
    <carrot-item-list-cards />
  </q-page>
</template>

<script setup lang="ts">
import CarrotItemListCards from 'components/CarrotItemListCards.vue';

defineOptions({
  name: 'CarrotPage',
});
</script>
