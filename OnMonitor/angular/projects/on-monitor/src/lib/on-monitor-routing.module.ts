import { NgModule } from '@angular/core';
import { DynamicLayoutComponent } from '@abp/ng.core';
import { Routes, RouterModule } from '@angular/router';
import { OnMonitorComponent } from './components/on-monitor.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: DynamicLayoutComponent,
    children: [
      {
        path: '',
        component: OnMonitorComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OnMonitorRoutingModule {}
