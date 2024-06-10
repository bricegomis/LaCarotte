<!-- eslint-disable vue/valid-v-model -->
<template>
  <div class="row justify-around">
    <div
      v-for="carrotItem in carrotItems"
      :key="carrotItem.id ?? ''"
      class="col-12 col-md-4 q-pa-xs"
    >
      <q-card flat bordered v-if="carrotItem">
        <q-card-section horizontal class="justify-around items-center">
          <q-img
            :src="carrotItem.image ?? ''"
            class="bg-white col-2 q-ma-sm"
            width="50px"
          />
          <div class="col-9">{{ carrotItem.title }}</div>
        </q-card-section>
        <q-separator />

        <q-card-section horizontal class="row justify-between items-center">
          <div class="">
            <q-btn
              flat
              icon="edit"
              @click="carrotItemStore.startEditingCarrotItem(carrotItem)"
              >Edit</q-btn
            >
          </div>
          <div>
            <q-rating
              class="q-px-sm"
              readonly
              v-if="carrotItem.history && haveHistory(carrotItem)"
              color="negative"
              icon="thumb_up"
              v-model="carrotItem.history.length"
            />
            <q-btn
              color="negative"
              square
              :disabled="isDisabled(carrotItem)"
              class="q-ma-none score-container text-gray"
              @click="carrotItemStore.finishCarrotItem(carrotItem)"
            >
              <div>{{ carrotItem.points }} pts</div>
            </q-btn>
          </div>
        </q-card-section>
      </q-card>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useCarrotItemStore } from 'src/stores/carrotItem-store';
import { computed } from 'vue';
import { CarrotItem } from './models';
import { useDoItemStore } from 'src/stores/doItem-store';

const doItemStore = useDoItemStore();

const carrotItemStore = useCarrotItemStore();
const carrotItems = computed(() => {
  return carrotItemStore.carrotItems
    .slice()
    .sort((a, b) => (a.points ?? 0) - (b.points ?? 0));
});

const haveHistory = (item: CarrotItem) => {
  return item.history?.length ?? 0 > 0;
};

const isDisabled = (item: CarrotItem) => {
  return (
    doItemStore.profile.scoreWeek <= (item.points ?? 0) ||
    (item.history?.length ?? 0) >= 5
  );
};
</script>

<style scoped>
.score-container {
  position: relative;
}

.score-text {
  /* position: absolute; */
  color: #fbff00;
  margin-right: 10px;
  /* top: 50%; */
  /* left: 50%; */
  font-size: 20px;
}
.pointBonus {
  font-size: 16px;
}
</style>
