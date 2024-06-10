import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'DoItems',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: '',
        name: 'doItem',
        component: () => import('pages/DoItemPage.vue'),
        meta: { transition: 'slide-left' },
      },
      {
        path: '',
        name: 'carrot',
        component: () => import('pages/CarrotPage.vue'),
        meta: { transition: 'slide-right' },
      },
    ],
  },
  // {
  //   path: '/templates',
  //   name: 'templates',
  //   component: () => import('layouts/MainLayout.vue'),
  //   children: [
  //     { path: '', component: () => import('pages/TemplatesPage.vue') },
  //   ],
  // },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue'),
  },
];

export default routes;
