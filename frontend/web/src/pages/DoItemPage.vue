<template>
  <q-page title="Carottes" style="background-color: #8ec8e4">
    <do-item-list-cards />
    <do-item-update-card></do-item-update-card>
  </q-page>
</template>

<script setup lang="ts">
import CarotteListCards from 'components/CarotteListCards.vue';
import CarotteUpdateCard from 'components/CarotteUpdateCard.vue';

defineOptions({
  name: 'CarottePage',
});
</script>
