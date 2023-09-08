import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RowDblClickEvent } from 'devextreme/ui/data_grid';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
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

  constructor(private authenticationService: AuthenticationService,
    private calendarService: CalendarService,
    private router: Router) {
    



    this.authenticationService.loggedUserId().subscribe(res => {
      this.calendarService.getEvents(res.userId).subscribe(res => {
        this.datasource = res;
      });
    });

  }

  handleRowDoubleClick(event: RowDblClickEvent) {
    const template = event.data
    this.router.navigate(['/event/edit', template.id].filter(v => !!v));
  }

}

