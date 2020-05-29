import { NgModule } from '@angular/core';
import { OnMonitorComponent } from './components/on-monitor.component';
import { OnMonitorRoutingModule } from './on-monitor-routing.module';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CoreModule } from '@abp/ng.core';

@NgModule({
  declarations: [OnMonitorComponent],
  imports: [CoreModule, ThemeSharedModule, OnMonitorRoutingModule],
  exports: [OnMonitorComponent],
})
export class OnMonitorModule {}
