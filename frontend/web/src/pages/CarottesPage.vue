<template>
  <q-page title="Carottes" style="background-color: #2b2a33">
    <q-tabs v-model="tag" inline-label class="text-white shadow-2">
      <q-tab
        v-for="tag in tags"
        :key="tag.name"
        :name="tag.name"
        :icon="tag.icon"
        :label="tag.label"
        :class="tag.classes"
      />
    </q-tabs>
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
// const selectedOrder = ref({ label: 'Points', value: 'asc' });

const tags = [
  {
    name: 'all',
    icon: 'reorder',
    label: 'All',
    classes: 'bg-primary',
  },
  {
    name: 'sport',
    icon: 'fitness_center',
    label: 'Sports',
    classes: 'bg-positive',
  },
  {
    name: 'home',
    icon: 'home',
    label: 'Maison',
    classes: 'bg-positive',
  },
  {
    name: 'menage',
    icon: 'cleaning_services',
    label: 'Ménage',
    classes: 'bg-positive',
  },
  {
    name: 'food',
    icon: 'restaurant',
    label: 'Food',
    classes: 'bg-negative',
  },
  {
    name: 'fiesta',
    icon: 'celebration',
    label: 'Fiesta',
    classes: 'bg-negative',
  },
];
const tag = ref(tags[0].name);

// const distinct = <T>(array: T[]): T[] => {
//   return array.filter((item, index) => array.indexOf(item) === index);
// };

// let allTags = carotteStore.carottes
//   .filter((carotte) => carotte.tags != null)
//   .map((carotte) => carotte.tags)
//   .flat()
//   .filter((tag) => tag != null && tag != undefined);
// allTags = distinct(allTags);
// allTags.forEach((tag) => {
//   if (tag != null && tag != undefined) tags.push(tag);
// });

// const orderOptions = ref([
//   { label: 'Points', value: 'asc' },
//   { label: 'Descendant', value: 'desc' },
// ]);
const carottes = computed(() => {
  let result = carotteStore.carottes.slice();

  if (tag.value != 'all') {
    result = result.filter(
      (carotte) => carotte.tags && carotte.tags.indexOf(tag.value) >= 0
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
