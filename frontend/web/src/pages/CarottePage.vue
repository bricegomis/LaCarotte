<template>
  <q-page title="Carotte">
    <q-card class="q-pa-md">
      <q-card-section>
        <q-avatar> <q-img :src="carotte.image ?? ''" /></q-avatar>
        <q-input v-model="carotte.title" label="Titre" />
        <q-input v-model="carotte.desc" label="Description" />
        <q-input v-model.number="carotte.points" type="number" />
        <!-- <q-knob
          :step="10"
          label="Points"
          v-model="points"
          show-value
          size="90px"
          :thickness="0.22"
          color="lime"
          track-color="lime-3"
          class="text-lime q-ma-md"
        />-->
        <q-input v-model="carotte.image" label="Image" />
      </q-card-section>
      <q-card-actions align="right">
        <q-btn
          label="Cancel"
          color="negative"
          @click="goBack"
        />
        <q-btn
          label="Save"
          color="primary"
          @click="carotteStore.SaveEditingCarotte"
        />
      </q-card-actions>
    </q-card>
  </q-page>
</template>

<script setup lang="ts">
import { useCarotteStore } from 'src/stores/carotte-store';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';
import { Carotte } from 'src/models/Carotte';
import { useRouter } from 'vue-router';

const carotteStore = useCarotteStore();
const route = useRoute();
const router = useRouter();

const carotteId = route.params.id as string;
console.log('carotteId', carotteId);

const carotte = ref<Carotte>({});
console.log('ref ok');

const goBack = () => {
  router.back();
};

// const points = computed({
//   // getter
//   get() {
//     return carotte.points ?? 0;
//   },
//   // setter
//   set(newValue) {
//     carotte.points = newValue;
//   },
// });

defineOptions({
  name: 'CarottePage',
});

onMounted(async () => {
  console.log('mounted');
  await carotteStore.getCarotte(carotteId);
  carotte.value = carotteStore.editingCarotte;
  console.log('carotte', carotte.value);
});
</script>
