// @ts-nocheck
import React from 'react';
import { ApplyPluginsType, dynamic } from 'e:/Projects/GeekBrains/3.AspNetCoreWebApi/Metrics/MetricsManager/ClientApp/node_modules/@umijs/runtime';
import * as umiExports from './umiExports';
import { plugin } from './plugin';
import LoadingComponent from '@ant-design/pro-layout/es/PageLoading';

export function getRoutes() {
  const routes = [
  {
    "path": "/",
    "component": dynamic({ loader: () => import(/* webpackChunkName: '.umi__plugin-layout__Layout' */'e:/Projects/GeekBrains/3.AspNetCoreWebApi/Metrics/MetricsManager/ClientApp/src/.umi/plugin-layout/Layout.tsx'), loading: LoadingComponent}),
    "routes": [
      {
        "path": "/dashboard",
        "name": "dashboard",
        "icon": "dashboard",
        "component": dynamic({ loader: () => import(/* webpackChunkName: 'p__dashboard' */'e:/Projects/GeekBrains/3.AspNetCoreWebApi/Metrics/MetricsManager/ClientApp/src/pages/dashboard'), loading: LoadingComponent}),
        "exact": true
      },
      {
        "path": "/agents",
        "icon": "form",
        "name": "agents",
        "component": dynamic({ loader: () => import(/* webpackChunkName: 'p__agents' */'e:/Projects/GeekBrains/3.AspNetCoreWebApi/Metrics/MetricsManager/ClientApp/src/pages/agents'), loading: LoadingComponent}),
        "exact": true
      },
      {
        "path": "/index.html",
        "redirect": "/dashboard",
        "exact": true
      },
      {
        "path": "/",
        "redirect": "/dashboard",
        "exact": true
      },
      {
        "component": dynamic({ loader: () => import(/* webpackChunkName: 'p__404' */'e:/Projects/GeekBrains/3.AspNetCoreWebApi/Metrics/MetricsManager/ClientApp/src/pages/404'), loading: LoadingComponent}),
        "exact": true
      }
    ]
  }
];

  // allow user to extend routes
  plugin.applyPlugins({
    key: 'patchRoutes',
    type: ApplyPluginsType.event,
    args: { routes },
  });

  return routes;
}
