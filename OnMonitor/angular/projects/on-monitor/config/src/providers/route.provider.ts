import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eOnMonitorRouteNames } from '../enums/route-names';

export const ON_MONITOR_ROUTE_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureRoutes,
    deps: [RoutesService],
    multi: true,
  },
];

export function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/on-monitor',
        name: eOnMonitorRouteNames.OnMonitor,
        iconClass: 'fas fa-book',
        layout: eLayoutType.application,
        order: 3,
      },
    ]);
  };
}
