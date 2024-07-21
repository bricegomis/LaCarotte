<template>
  <q-layout view="lHh Lpr lFf" style="background-color: #2b2a33">
    <q-header elevated>
      <q-toolbar class="">
        <!--
          <q-btn
          flat
          dense
          round
          icon="menu"
          aria-label="Menu"
          @click="toggleLeftDrawer"
        />
        -->

        <q-toolbar-title class="text-accent">
          La Carotte
          <q-icon
            color="red"
            size="ml"
            name="wifi"
            v-if="!carotteStore.isOnline"
        /></q-toolbar-title>

        <div class="row text-h6 text-black justify-around items-center">
          <div>{{ carotteStore.profile?.scoreDay }}</div>
          <div>
            <q-avatar size="24px">
              <q-img src="/img/LaCarotte.jpg" ratio="1" />
            </q-avatar>
          </div>
        </div>
      </q-toolbar>
    </q-header>

    <q-page-container>
      <router-view v-slot="{ Component }">
        <!-- Use a custom transition or fallback to `fade` -->
        <transition name="slide-right">
          <component :is="Component" />
        </transition>
      </router-view>
    </q-page-container>

    <q-footer class="row q-pa-md q-gutter-sm items-center justify-around">
      <q-btn
        round
        @click="changeTab('carotte')"
        class="q-pa-sm bg-white"
        dense
        size="xl"
      >
        <q-avatar size="80px">
          <q-img src="/img/LaCarotte.jpg" ratio="1" />
        </q-avatar>
      </q-btn>

      <q-btn dense round class="" @click="changeTab('carotte')">
        <q-avatar size="80px">
          <q-img src="/img/LaClope.jpg" ratio="1" />
        </q-avatar>
      </q-btn>
    </q-footer>
  </q-layout>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useCarotteStore } from 'src/stores/carotte-store';

const carotteStore = useCarotteStore();
const router = useRouter();

const activeTab = ref('');

const changeTab = (tabName: string) => {
  if (tabName !== activeTab.value) {
    activeTab.value = tabName;
    router.push({ name: tabName });
  }
};
</script>

<style scoped>
/* Add your custom styles here */
</style>
