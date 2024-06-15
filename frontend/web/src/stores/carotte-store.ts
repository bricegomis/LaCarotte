import { defineStore } from 'pinia';
import { Carotte } from 'src/models/Carotte';
import { Profile } from 'src/models/Profile';
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
        const response = await api.get('carotte');
        this.carottes = response.data;
      } catch (error) {
        //console.error('Error fetching Carottes:', error);
      }
    },
    async getCarotte(id: string) {
      try {
        console.log('api.getCarotte', id);
        const response = await api.get(`carotte/${id}`);
        this.editingCarotte = response.data;
        console.log('this.editingCarrote', this.editingCarotte);
      } catch (error) {
        console.error('Error fetching Carotte:', error);
      }
    },
    async createCarotte(carotte: Carotte) {
      try {
        await api.post('carotte', carotte);
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
          await api.put('carotte', carotte);
        // => new
        else await api.post('Carotte', carotte);
        await this.fetchCarottes();
      } catch (error) {
        console.error('Error when editing a carotte :', error);
      }
    },
    async deleteCarotte(carotte: Carotte) {
      try {
        await api.delete(`carotte/${carotte.id}`);
        await this.fetchCarottes();
      } catch (error) {
        console.error('Error deleting Carotte:', error);
      }
    },
    async finishCarotte(carotte: Carotte) {
      try {
        await api.put('carotte/finish', carotte);
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
