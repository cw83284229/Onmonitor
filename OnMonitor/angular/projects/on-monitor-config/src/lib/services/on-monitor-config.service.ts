import { Injectable } from '@angular/core';
import { eLayoutType, addAbpRoutes, ABP } from '@abp/ng.core';
import { addSettingTab } from '@abp/ng.theme.shared';
import { OnMonitorSettingsComponent } from '../components/on-monitor-settings.component';

@Injectable({
  providedIn: 'root',
})
export class OnMonitorConfigService {
  constructor() {
    addAbpRoutes({
      name: 'OnMonitor',
      path: 'on-monitor',
      layout: eLayoutType.application,
      order: 2,
    } as ABP.FullRoute);

    const route = addSettingTab({
      component: OnMonitorSettingsComponent,
      name: 'OnMonitor Settings',
      order: 1,
      requiredPolicy: '',
    });
  }
}
