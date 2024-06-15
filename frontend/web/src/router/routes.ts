import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'Carottes',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: '',
        name: 'carottes',
        component: () => import('pages/CarottesPage.vue'),
        meta: { transition: 'slide-left' },
      },
      {
        path: 'carotte/:id',
        name: 'carotte',
        component: () => import('pages/CarottePage.vue'),
        meta: { transition: 'slide-right' },
      },
    ],
  },
  {
    path: '/scanner',
    name: 'Scanner',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: '/scanner',
        name: 'scanner',
        component: () => import('pages/ScannerPage.vue'),
        meta: { transition: 'slide-left' },
      },
    ],
  },
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue'),
  },
];

export default routes;
