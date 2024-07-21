<!-- eslint-disable vue/valid-v-model -->
<template>
  <q-card
    class="my-card q-pa-none"
    style="background-color: #6b697f; color: white"
    flat
    bordered
    v-on:click="router.push({ name: 'carotte', params: { id: carotte.id } })"
  >
    <q-card-section
      horizontal
      class="row items-center content-stretch background-gray"
    >
      <q-icon
        class="q-mx-sm"
        :name="carotte.image ?? 'win'"
        :color="carotte.isReward ? 'positive' : 'negative'"
        size="md"
      />
      <div class="col text-left">
        <div class="text-weight-bold text-capitalize">
          {{ carotte.title }}
        </div>
        <div class="text- text-italic">{{ carotte.desc }}</div>
      </div>
      <div class="col-auto row items-center self-stretch q-ml-md">
        <div class="col">
          <span class="text-caption text-grey">
            x{{ carotte.history?.length ?? 0 }}
          </span>
        </div>
        <q-btn
          :color="carotte.isReward ? 'positive' : 'negative'"
          :disabled="isDisabled(carotte)"
          class="q-ml-sm self-stretch text-gray"
          @click="carotteStore.finishCarotte(carotte)"
          :style="{ borderRadius: '0 10px 10px 0' }"
        >
          <div>{{ carotte.points }} pts</div>
        </q-btn>
      </div>
    </q-card-section>

    <!-- <q-card-section horizontal class="row justify-between items-center">
      <div class="">
        <q-btn flat icon="edit" :href="`/carotte/${carotte.id}`">Edit</q-btn>
      </div>
    </q-card-section> -->
  </q-card>
</template>

<script setup lang="ts">
import { useCarotteStore } from 'src/stores/carotte-store';
import { Carotte } from 'src/models/Carotte';
import { useRouter } from 'vue-router';
import { ref } from 'vue';

const carotteStore = useCarotteStore();
const router = useRouter();

const props = defineProps({
  carotte: { type: Object, required: true },
});
const carotte = ref(props.carotte as Carotte);
// const haveHistory = (item: Carotte) => {
//   return item.history?.length ?? 0 > 0;
// };

const isDisabled = (item: Carotte) => {
  if (item.history != null) {
    var lastDate = new Date(item.history[item.history.length - 1]);
    var time = getMinutesDifference(lastDate);
    if (time < 2) return true;
  }
  if (!item.isReward)
    return carotteStore.profile.scoreDay <= (item.points ?? 0);

  return false;
};

const getMinutesDifference = (date: Date): number => {
  // Obtenir l'heure actuelle en millisecondes
  const now = new Date().getTime();

  // Obtenir l'heure de la date donnée en millisecondes
  const givenDate = date.getTime();

  // Calculer la différence en millisecondes
  const diffInMilliseconds = now - givenDate;

  // Convertir la différence en minutes
  const diffInMinutes = Math.floor(diffInMilliseconds / (1000 * 60));

  return diffInMinutes;
};
</script>

<style scoped></style>
