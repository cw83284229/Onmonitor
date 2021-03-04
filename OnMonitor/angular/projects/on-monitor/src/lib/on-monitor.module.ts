import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { OnMonitorComponent } from './components/on-monitor.component';
import { OnMonitorRoutingModule } from './on-monitor-routing.module';

@NgModule({
  declarations: [OnMonitorComponent],
  imports: [CoreModule, ThemeSharedModule, OnMonitorRoutingModule],
  exports: [OnMonitorComponent],
})
export class OnMonitorModule {
  static forChild(): ModuleWithProviders<OnMonitorModule> {
    return {
      ngModule: OnMonitorModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<OnMonitorModule> {
    return new LazyModuleFactory(OnMonitorModule.forChild());
  }
}
