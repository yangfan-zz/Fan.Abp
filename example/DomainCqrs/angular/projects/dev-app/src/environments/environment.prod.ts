import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'DomainCqrs',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44305',
    redirectUri: baseUrl,
    clientId: 'DomainCqrs_App',
    responseType: 'code',
    scope: 'offline_access DomainCqrs',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44305',
      rootNamespace: 'DomainCqrs',
    },
    DomainCqrs: {
      url: 'https://localhost:44329',
      rootNamespace: 'DomainCqrs',
    },
  },
} as Environment;
