<!-- eslint-disable vue/valid-v-model -->
<template>
  <div class="row justify-around">
    <div
      v-for="carotte in carottes"
      :key="carotte.id ?? ''"
      class="col-12 col-md-4 q-pa-xs"
    >
      <q-card flat bordered v-if="carotte">
        <q-card-section horizontal class="justify-around items-center">
          <q-img
            :src="carotte.image ?? ''"
            class="bg-white col-2 q-ma-sm"
            width="50px"
          />
          <div class="col-9">{{ carotte.title }}</div>
        </q-card-section>
        <q-separator />

        <q-card-section horizontal class="row justify-between items-center">
          <div class="">
            <q-btn
              flat
              icon="edit"
              @click="carotteStore.startEditingCarotte(carotte)"
              >Edit</q-btn
            >
          </div>
          <div>
            <span class="text-caption text-grey">
              {{ carotte.history?.length ?? 0 }}x
            </span>
            <q-btn
              :color="carotte.isReward ? 'positive' : 'negative'"
              square
              :disabled="isDisabled(carotte)"
              class="q-ma-none score-container text-gray"
              @click="carotteStore.finishCarotte(carotte)"
            >
              <div>{{ carotte.points }} pts</div>
            </q-btn>
          </div>
        </q-card-section>
      </q-card>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useCarotteStore } from 'src/stores/carotte-store';
import { computed } from 'vue';
import { Carotte } from './models';

const carotteStore = useCarotteStore();

const carottes = computed(() => {
  return carotteStore.carottes
    .slice()
    .sort((a, b) => (a.points ?? 0) - (b.points ?? 0));
});

// const haveHistory = (item: Carotte) => {
//   return item.history?.length ?? 0 > 0;
// };

const isDisabled = (item: Carotte) => {
  return carotteStore.profile.scoreWeek <= (item.points ?? 0);
};
</script>

<style scoped></style>
