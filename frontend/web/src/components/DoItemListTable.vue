<template>
  <div>
    <div>
      <q-table
        title="Tasks"
        :rows="doItemStore.doItems"
        :columns="columns"
        row-key="id"
        :pagination="pagination"
      >
        <template v-slot:top-right>
          <q-btn
            color="primary"
            label="New Task"
            @click="doItemStore.startEditingDoItem({})"
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
              @click.stop="doItemStore.finishDoItem(props.row)"
            />
            <q-btn
              flat
              round
              padding="qs"
              color="grey"
              size="sm"
              icon="edit"
              @click.stop="doItemStore.startEditingDoItem(props.row)"
            />
            <q-btn
              flat
              round
              size="sm"
              color="red"
              icon="delete"
              @click.stop="doItemStore.deleteDoItem(props.row)"
            />
          </q-td>
        </template>
      </q-table>
    </div>
    
  </div>
</template>

<script setup lang="ts">
import { useDoItemStore } from 'src/stores/doItem-store';
import { DoItem } from './models';

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
    field: (row: DoItem) => row.title,
  },
  { name: 'schedule', label: 'Schedule', field: 'schedule' },
  {
    name: 'points',
    label: 'Points',
    field: (row: DoItem) => row.points,
  },
  { label: '', name: 'delete', field: 'delete' },
];
const doItemStore = useDoItemStore();
</script>
