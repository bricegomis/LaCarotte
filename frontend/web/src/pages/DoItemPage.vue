<template>
  <q-page title="DoItems" style="background-color: #8ec8e4">
    <do-item-list-cards />
    <do-item-update-card></do-item-update-card>
  </q-page>
</template>

<script setup lang="ts">
import DoItemListCards from 'components/DoItemListCards.vue';
import DoItemUpdateCard from 'components/DoItemUpdateCard.vue';

defineOptions({
  name: 'DoItemPage',
});
</script>
