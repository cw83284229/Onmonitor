import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'OnMonitor',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44331',
    redirectUri: baseUrl,
    clientId: 'OnMonitor_App',
    responseType: 'code',
    scope: 'offline_access OnMonitor',
  },
  apis: {
    default: {
      url: 'https://localhost:44331',
      rootNamespace: 'OnMonitor',
    },
    OnMonitor: {
      url: 'https://localhost:44351',
      rootNamespace: 'OnMonitor',
    },
  },
} as Environment;
