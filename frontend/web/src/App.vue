<template>
  <router-view />
  <q-ajax-bar />
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { useCarotteStore } from 'src/stores/carotte-store';

const carotteStore = useCarotteStore();

defineOptions({
  name: 'App',
});

const loop = async () => {
  try {
    await carotteStore.fetchProfile();
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
