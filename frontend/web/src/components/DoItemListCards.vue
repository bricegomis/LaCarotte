<template>
  <div class="row justify-around">
    <div
      v-for="doItem in doItems"
      :key="doItem.id ?? ''"
      class="col-12 col-md-4 q-pa-xs"
    >
      <q-card flat bordered>
        <q-card-section horizontal class="justify-around items-center">
          <q-img
            :src="doItem.image ?? ''"
            class="bg-white col-2 q-ma-sm"
            width="50px"
          />
          <div class="col-9">{{ doItem.title }}</div>
        </q-card-section>
        <q-separator />

        <q-card-section horizontal class="row justify-between items-center">
          <div class="">
            <q-btn
              flat
              icon="edit"
              @click="doItemStore.startEditingDoItem(doItem)"
              >Edit</q-btn
            >
          </div>
          <div>
            <q-rating
              class="q-px-sm"
              readonly
              v-if="doItem.historyBonus && doItem"
              :color="doItem.schedule == 'Daily' ? 'primary' : 'grey'"
              icon="thumb_up"
              v-model="doItem.historyBonus"
              :disabled="doItem.historyBonus ?? 0 > 0"
            />
            <q-btn
              color="positive"
              square
              class="q-ma-none score-container text-gray"
              :disabled="doItem.isFinished"
              @click="doItemStore.finishDoItem(doItem)"
            >
              <div
                class="text-bold score-text text-lowercase"
                v-if="haveBonus(doItem)"
              >
                +{{ bonusPoint(doItem) }}pts |
              </div>
              <div :class="{ pointBonus: haveBonus(doItem) }">
                {{ doItem.points }} pts
              </div>
            </q-btn>
          </div>
        </q-card-section>
      </q-card>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useDoItemStore } from 'src/stores/doItem-store';
import { computed } from 'vue';
import { DoItem } from './models';

const doItemStore = useDoItemStore();
const doItems = computed(() => {
  return doItemStore.doItems.slice().sort((a, b) => {
    if (a.isFinished && !b.isFinished) {
      return 1;
    } else if (!a.isFinished && b.isFinished) {
      return -1;
    } else {
      return (b.points ?? 0) - (a.points ?? 0);
    }
  });
});
const haveBonus = (doItem: DoItem) => {
  return doItem.historyBonus && doItem.historyBonus > 0;
};
const bonusPoint = (doItem: DoItem) => {
  if (!doItem || !doItem.historyBonus) return;
  return (doItem.historyBonus ?? 0) * 10;
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
