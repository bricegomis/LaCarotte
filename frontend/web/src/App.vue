<template>
  <router-view />
  <q-ajax-bar />
</template>

<script setup lang="ts">
import { useDoItemStore } from 'src/stores/doItem-store';
import { onMounted } from 'vue';
import { useCarrotItemStore } from './stores/carrotItem-store';

const doItemStore = useDoItemStore();
const carrotItemStore = useCarrotItemStore();

defineOptions({
  name: 'App',
});

const loop = async () => {
  try {
    await doItemStore.fetchProfile();
    await doItemStore.fetchDoItems();
    await carrotItemStore.fetchCarrotItems();
  } catch (error) {
    // console.error('Error fetching items:', error);
  } finally {
    setTimeout(loop, 5000);
  }
};

onMounted(() => {
  loop();
});
</script>
