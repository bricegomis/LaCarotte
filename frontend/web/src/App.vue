<template>
  <router-view />
  <q-ajax-bar />
</template>

<script setup lang="ts">
import { useCarotteStore } from 'src/stores/carotte-store';
import { onMounted } from 'vue';
import { useCarotteStore } from './stores/carotte-store';

const carotteStore = useCarotteStore();
const carotteStore = useCarotteStore();

defineOptions({
  name: 'App',
});

const loop = async () => {
  try {
    await carotteStore.fetchProfile();
    await carotteStore.fetchCarottes();
    await carotteStore.fetchCarottes();
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
