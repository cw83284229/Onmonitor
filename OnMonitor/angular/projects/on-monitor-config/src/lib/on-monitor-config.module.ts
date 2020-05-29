import { NgModule, APP_INITIALIZER } from '@angular/core';
import { OnMonitorConfigService } from './services/on-monitor-config.service';
import { noop } from '@abp/ng.core';
import { OnMonitorSettingsComponent } from './components/on-monitor-settings.component';

@NgModule({
  declarations: [OnMonitorSettingsComponent],
  providers: [{ provide: APP_INITIALIZER, deps: [OnMonitorConfigService], multi: true, useFactory: noop }],
  exports: [OnMonitorSettingsComponent],
  entryComponents: [OnMonitorSettingsComponent],
})
export class OnMonitorConfigModule {}
