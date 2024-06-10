<template>
  <div class="row justify-around">
    <div
      v-for="carotte in carottes"
      :key="carotte.id ?? ''"
      class="col-12 col-md-4 q-pa-xs"
    >
      <q-card flat bordered>
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
            <q-rating
              class="q-px-sm"
              readonly
              v-if="carotte.historyBonus && carotte"
              :color="carotte.schedule == 'Daily' ? 'primary' : 'grey'"
              icon="thumb_up"
              v-model="carotte.historyBonus"
              :disabled="carotte.historyBonus ?? 0 > 0"
            />
            <q-btn
              color="positive"
              square
              class="q-ma-none score-container text-gray"
              :disabled="carotte.isFinished"
              @click="carotteStore.finishCarotte(carotte)"
            >
              <div
                class="text-bold score-text text-lowercase"
                v-if="haveBonus(carotte)"
              >
                +{{ bonusPoint(carotte) }}pts |
              </div>
              <div :class="{ pointBonus: haveBonus(carotte) }">
                {{ carotte.points }} pts
              </div>
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
  return carotteStore.carottes.slice().sort((a, b) => {
    if (a.isFinished && !b.isFinished) {
      return 1;
    } else if (!a.isFinished && b.isFinished) {
      return -1;
    } else {
      return (b.points ?? 0) - (a.points ?? 0);
    }
  });
});
const haveBonus = (carotte: Carotte) => {
  return carotte.historyBonus && carotte.historyBonus > 0;
};
const bonusPoint = (carotte: Carotte) => {
  if (!carotte || !carotte.historyBonus) return;
  return (carotte.historyBonus ?? 0) * 10;
};
</script>

<style scoped>
.score-container {
  position: relative;
}

.score-text {
  color: #fbff00;
  margin-right: 10px;
  font-size: 14px;
}
.pointBonus {
  font-size: 16px;
}
</style>
