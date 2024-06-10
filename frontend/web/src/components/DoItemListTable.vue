<template>
  <div>
    <div>
      <q-table
        title="Tasks"
        :rows="carotteStore.carottes"
        :columns="columns"
        row-key="id"
        :pagination="pagination"
      >
        <template v-slot:top-right>
          <q-btn
            color="primary"
            label="New Task"
            @click="carotteStore.startEditingCarotte({})"
          />
        </template>
        <template v-slot:body-cell-img="props">
          <q-td :props="props">
            <q-img :src="props.row.image" style="height: 50px; width: 50px" />
          </q-td>
        </template>
        <template v-slot:body-cell-delete="props">
          <q-td :props="props">
            <q-btn
              flat
              round
              color="green"
              size="sm"
              icon="check"
              :class="{ invisible: props.row.isFinished == true }"
              @click.stop="carotteStore.finishCarotte(props.row)"
            />
            <q-btn
              flat
              round
              padding="qs"
              color="grey"
              size="sm"
              icon="edit"
              @click.stop="carotteStore.startEditingCarotte(props.row)"
            />
            <q-btn
              flat
              round
              size="sm"
              color="red"
              icon="delete"
              @click.stop="carotteStore.deleteCarotte(props.row)"
            />
          </q-td>
        </template>
      </q-table>
    </div>
    
  </div>
</template>

<script setup lang="ts">
import { useCarotteStore } from 'src/stores/carotte-store';
import { Carotte } from './models';

const pagination = {
  sortBy: 'title',
  descending: false,
  page: 1,
  rowsPerPage: 20,
};

const columns = [
  { name: 'img', label: '', field: 'img' },
  {
    name: 'Title',
    label: 'Title',
    field: (row: Carotte) => row.title,
  },
  { name: 'schedule', label: 'Schedule', field: 'schedule' },
  {
    name: 'points',
    label: 'Points',
    field: (row: Carotte) => row.points,
  },
  { label: '', name: 'delete', field: 'delete' },
];
const carotteStore = useCarotteStore();
</script>
