import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RowDblClickEvent } from 'devextreme/ui/data_grid';
import { CalendarService } from 'src/app/common/services/calendar.service';


@Component({
  selector: 'app-calendar-manage',
  templateUrl: './calendar-manage.component.html',
  styleUrls: ['./calendar-manage.component.css']
})
export class CalendarManageComponent {
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private calendarService: CalendarService,
    private router: Router) {
    this.calendarService.getEvents().subscribe(res => {
      this.datasource = res;
    });
  }

  handleRowDoubleClick(event: RowDblClickEvent) {
    const template = event.data
    this.router.navigate(['/edit-event', template.id].filter(v => !!v));
  }

}

