import { ModuleWithProviders, NgModule } from '@angular/core';
import { ON_MONITOR_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class OnMonitorConfigModule {
  static forRoot(): ModuleWithProviders<OnMonitorConfigModule> {
    return {
      ngModule: OnMonitorConfigModule,
      providers: [ON_MONITOR_ROUTE_PROVIDERS],
    };
  }
}
