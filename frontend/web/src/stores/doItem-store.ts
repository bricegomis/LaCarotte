import { defineStore } from 'pinia';
import { Carotte, Profile } from 'components/models';
import { api } from 'boot/axios';

export const useCarotteStore = defineStore('carotte', {
  state: () => ({
    carottes: [] as Carotte[],
    isEditingCarotteDialogVisible: false,
    editingCarotte: {} as Carotte,
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
    async fetchCarottes() {
      try {
        const response = await api.get('Carotte');
        this.carottes = response.data;
      } catch (error) {
        //console.error('Error fetching Carottes:', error);
      }
    },
    async createCarotte(carotte: Carotte) {
      try {
        await api.post('Carotte', carotte);
        await this.fetchCarottes();
      } catch (error) {
        console.error('Error creating Carotte:', error);
      }
    },
    async updateCarotte(carotte: Carotte) {
      try {
        if (carotte.id)
          // TODO use carotte ?
          // => editing
          await api.put('Carotte', carotte);
        // => new
        else await api.post('Carotte', carotte);
        await this.fetchCarottes();
      } catch (error) {
        console.error('Error when editing a carotte :', error);
      }
    },
    async deleteCarotte(carotte: Carotte) {
      try {
        await api.delete(`Carotte/${carotte.id}`);
        await this.fetchCarottes();
      } catch (error) {
        console.error('Error deleting Carotte:', error);
      }
    },
    async finishCarotte(carotte: Carotte) {
      try {
        await api.put('Carotte/finish', carotte);
        await this.fetchCarottes();
      } catch (error) {
        console.error('Error when finishing a carotte :', error);
      }
    },
    async startEditingCarotte(carotte: Carotte) {
      if (carotte) this.editingCarotte = carotte;
      else this.editingCarotte = {};
      this.isEditingCarotteDialogVisible = true;
    },
    async SaveEditingCarotte() {
      await this.updateCarotte(this.editingCarotte);
      this.isEditingCarotteDialogVisible = false;
    },
    async closeNewItemDialog() {
      this.isEditingCarotteDialogVisible = false;
    },
  },
  persist: {
    enabled: true,
  },
});
