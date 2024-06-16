<template>
  <q-page title="Carottes" style="background-color: #f5f5f8">
    <q-toolbar class="q-pa-sm row" style="background-color: #fffddf">
      <q-select
        class="col"
        v-model="selectedTag"
        :options="tags"
        label="Catégorie"
        outlined
        dense
      />
      <q-select
        class="col"
        v-model="selectedOrder"
        :options="orderOptions"
        label="Ordre"
        outlined
        dense
      />
    </q-toolbar>
    <div class="row justify-around">
      <div
        v-for="carotte in carottes"
        :key="carotte.id ?? ''"
        class="col-12 col-md-4 q-pa-sm"
      >
        <carotte-card :carotte="carotte" />
      </div>
    </div>
  </q-page>
</template>

<script setup lang="ts">
import CarotteCard from 'components/CarotteCard.vue';
import { useCarotteStore } from 'src/stores/carotte-store';
import { computed, ref } from 'vue';

const carotteStore = useCarotteStore();
const selectedOrder = ref({ label: 'Points', value: 'asc' });
const selectedTag = ref<string>('All');

const tags = ['All'];

const distinct = <T>(array: T[]): T[] => {
  return array.filter((item, index) => array.indexOf(item) === index);
};

let allTags = carotteStore.carottes
  .filter((carotte) => carotte.tags != null)
  .map((carotte) => carotte.tags)
  .flat()
  .filter((tag) => tag != null && tag != undefined);
allTags = distinct(allTags);
allTags.forEach((tag) => {
  if (tag != null && tag != undefined) tags.push(tag);
});

const orderOptions = ref([
  { label: 'Points', value: 'asc' },
  { label: 'Descendant', value: 'desc' },
]);
const carottes = computed(() => {
  let result = carotteStore.carottes.slice();

  if (selectedTag.value != 'All') {
    result = result.filter(
      (carotte) => carotte.tags && carotte.tags.indexOf(selectedTag.value) >= 0
    );
  }

  result.sort((a, b) => {
    // console.log(Number(b.isReward))
    return Number(b.isReward) - Number(a.isReward);
  });

  // result.sort((a, b) => {
  //   const comparison = (a.points ?? 0) - (b.points ?? 0);
  //   return selectedOrder.value.value === 'asc' ? comparison : -comparison;
  // });

  return result;
});

defineOptions({
  name: 'CarottesPage',
});
</script>
