import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'Carottes',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: '',
        name: 'carotte',
        component: () => import('pages/CarottePage.vue'),
        meta: { transition: 'slide-left' },
      },
      {
        path: '',
        name: 'carotte',
        component: () => import('pages/CarottePage.vue'),
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
