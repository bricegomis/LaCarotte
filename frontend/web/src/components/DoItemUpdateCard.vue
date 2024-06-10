<template>
  <q-dialog v-model="doItemStore.isEditingDoItemDialogVisible">
    <q-card class="q-pa-md">
      <q-card-section>
        <q-input v-model="doItemStore.editingDoItem.title" label="Titre" />
        <q-input
          v-model.number="doItemStore.editingDoItem.points"
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
        <q-input v-model="doItemStore.editingDoItem.image" label="Image" />
        <q-input
          v-model="doItemStore.editingDoItem.schedule"
          label="Schedule"
        />
      </q-card-section>
      <q-card-actions align="right">
        <q-btn
          label="Cancel"
          color="negative"
          @click="doItemStore.closeNewItemDialog"
        />
        <q-btn
          label="Save"
          color="primary"
          @click="doItemStore.SaveEditingDoItem"
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import { useDoItemStore } from 'src/stores/doItem-store';
import { computed } from 'vue';

const doItemStore = useDoItemStore();
const points = computed({
  // getter
  get() {
    return doItemStore.editingDoItem.points ?? 0;
  },
  // setter
  set(newValue) {
    doItemStore.editingDoItem.points = newValue;
  },
});
</script>
