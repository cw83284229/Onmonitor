export const environment = {
  production: true,
  application: {
    name: 'OnMonitor',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44378',
    clientId: 'OnMonitor_ConsoleTestApp',
    dummyClientSecret: '1q2w3e*',
    scope: 'OnMonitor',
    showDebugInformation: true,
    oidc: false,
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44394',
    },
  },
  localization: {
    defaultResourceName: 'OnMonitor',
  },
};
