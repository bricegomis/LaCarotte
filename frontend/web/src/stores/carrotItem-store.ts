import { defineStore } from 'pinia';
import { CarrotItem } from 'components/models';
import { api } from 'boot/axios';

export const useCarrotItemStore = defineStore('carrotItem', {
  state: () => ({
    carrotItems: [] as CarrotItem[],
    isEditingCarrotItemDialogVisible: false,
    editingCarrotItem: {} as CarrotItem,
  }),
  actions: {
    async fetchCarrotItems() {
      try {
        const response = await api.get('CarrotItem');
        this.carrotItems = response.data;
      } catch (error) {}
    },
    async createCarrotItem(carrotItem: CarrotItem) {
      try {
        await api.post('CarrotItem', carrotItem);
        await this.fetchCarrotItems();
      } catch (error) {
        console.error('Error creating CarrotItem:', error);
      }
    },
    async updateCarrotItem(carrotItem: CarrotItem) {
      try {
        if (carrotItem.id)
          // TODO use carrotItem ?
          // => editing
          await api.put('CarrotItem', carrotItem);
        // => new
        else await api.post('CarrotItem', carrotItem);
        await this.fetchCarrotItems();
      } catch (error) {
        console.error('Error when editing a carrotItem :', error);
      }
    },
    async deleteCarrotItem(carrotItem: CarrotItem) {
      try {
        await api.delete(`CarrotItem/${carrotItem.id}`);
        await this.fetchCarrotItems();
      } catch (error) {
        console.error('Error deleting CarrotItem:', error);
      }
    },
    async finishCarrotItem(carrotItem: CarrotItem) {
      try {
        await api.put('CarrotItem/finish', carrotItem);
        await this.fetchCarrotItems();
      } catch (error) {
        console.error('Error when finishing a carrotItem :', error);
      }
    },
    async startEditingCarrotItem(carrotItem: CarrotItem) {
      if (carrotItem) this.editingCarrotItem = carrotItem;
      else this.editingCarrotItem = {};
      this.isEditingCarrotItemDialogVisible = true;
    },
    async SaveEditingCarrotItem() {
      await this.updateCarrotItem(this.editingCarrotItem);
      this.isEditingCarrotItemDialogVisible = false;
    },
    async closeNewItemDialog() {
      this.isEditingCarrotItemDialogVisible = false;
    },
  },
  persist: {
    enabled: true,
  },
});
