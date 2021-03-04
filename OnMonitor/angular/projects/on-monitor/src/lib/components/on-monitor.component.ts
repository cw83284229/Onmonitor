import { Component, OnInit } from '@angular/core';
import { OnMonitorService } from '../services/on-monitor.service';

@Component({
  selector: 'lib-on-monitor',
  template: ` <p>on-monitor works!</p> `,
  styles: [],
})
export class OnMonitorComponent implements OnInit {
  constructor(private service: OnMonitorService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
