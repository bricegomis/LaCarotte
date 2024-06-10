<template>
  <q-dialog v-model="carotteStore.isEditingCarotteDialogVisible">
    <q-card class="q-pa-md">
      <q-card-section>
        <q-input v-model="carotteStore.editingCarotte.title" label="Titre" />
        <q-input
          v-model.number="carotteStore.editingCarotte.points"
          type="number"
        />
        <q-knob
          :step="10"
          label="Points"
          v-model="points"
          show-value
          size="90px"
          :thickness="0.22"
          color="lime"
          track-color="lime-3"
          class="text-lime q-ma-md"
        />
        <q-input v-model="carotteStore.editingCarotte.image" label="Image" />
        <q-input
          v-model="carotteStore.editingCarotte.schedule"
          label="Schedule"
        />
      </q-card-section>
      <q-card-actions align="right">
        <q-btn
          label="Cancel"
          color="negative"
          @click="carotteStore.closeNewItemDialog"
        />
        <q-btn
          label="Save"
          color="primary"
          @click="carotteStore.SaveEditingCarotte"
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import { useCarotteStore } from 'src/stores/carotte-store';
import { computed } from 'vue';

const carotteStore = useCarotteStore();
const points = computed({
  // getter
  get() {
    return carotteStore.editingCarotte.points ?? 0;
  },
  // setter
  set(newValue) {
    carotteStore.editingCarotte.points = newValue;
  },
});
</script>
