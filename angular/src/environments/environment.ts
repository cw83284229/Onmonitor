export const environment = {
  production: false,
  hmr: false,
  application: {
    name: 'OnMonitor',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44365',
    clientId: 'OnMonitor_App',
    dummyClientSecret: '1q2w3e*',
    scope: 'OnMonitor',
    showDebugInformation: true,
    oidc: false,
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44365',
    },
  },
  localization: {
    defaultResourceName: 'OnMonitor',
  },
};
