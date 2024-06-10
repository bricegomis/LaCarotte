import { defineStore } from 'pinia';
import { DoItem, Profile } from 'components/models';
import { api } from 'boot/axios';

export const useDoItemStore = defineStore('doItem', {
  state: () => ({
    doItems: [] as DoItem[],
    isEditingDoItemDialogVisible: false,
    editingDoItem: {} as DoItem,
    profile: {} as Profile,
    isOnline: false,
  }),
  actions: {
    async fetchProfile() {
      try {
        const response = await api.get('profile');
        this.isOnline = true;
        this.profile = response.data;
      } catch (error) {
        //console.error('Error fetching profile:', error);
        this.isOnline = false;
      }
    },
    async fetchDoItems() {
      try {
        const response = await api.get('DoItem');
        this.doItems = response.data;
      } catch (error) {
        //console.error('Error fetching DoItems:', error);
      }
    },
    async createDoItem(doItem: DoItem) {
      try {
        await api.post('DoItem', doItem);
        await this.fetchDoItems();
      } catch (error) {
        console.error('Error creating DoItem:', error);
      }
    },
    async updateDoItem(doItem: DoItem) {
      try {
        if (doItem.id)
          // TODO use doItem ?
          // => editing
          await api.put('DoItem', doItem);
        // => new
        else await api.post('DoItem', doItem);
        await this.fetchDoItems();
      } catch (error) {
        console.error('Error when editing a doItem :', error);
      }
    },
    async deleteDoItem(doItem: DoItem) {
      try {
        await api.delete(`DoItem/${doItem.id}`);
        await this.fetchDoItems();
      } catch (error) {
        console.error('Error deleting DoItem:', error);
      }
    },
    async finishDoItem(doItem: DoItem) {
      try {
        await api.put('DoItem/finish', doItem);
        await this.fetchDoItems();
      } catch (error) {
        console.error('Error when finishing a doItem :', error);
      }
    },
    async startEditingDoItem(doItem: DoItem) {
      if (doItem) this.editingDoItem = doItem;
      else this.editingDoItem = {};
      this.isEditingDoItemDialogVisible = true;
    },
    async SaveEditingDoItem() {
      await this.updateDoItem(this.editingDoItem);
      this.isEditingDoItemDialogVisible = false;
    },
    async closeNewItemDialog() {
      this.isEditingDoItemDialogVisible = false;
    },
  },
  persist: {
    enabled: true,
  },
});
