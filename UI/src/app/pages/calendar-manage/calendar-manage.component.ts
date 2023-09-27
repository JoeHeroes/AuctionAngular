import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { AuthResponseDto, AuthenticationService } from 'src/app/services/authentication.service';
import { CalendarService } from 'src/app/services/calendar.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-calendar-manage',
  templateUrl: './calendar-manage.component.html',
  styleUrls: ['./calendar-manage.component.css']
})
export class CalendarManageComponent {
  userId: any;
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private authenticationService: AuthenticationService,
    private calendarService: CalendarService,
    private notificationService: NotificationService,
    private router: Router,
    private transloco: TranslocoService) {
    
      this.authenticationService.loggedUserId().subscribe(res => {
        this.calendarService.getEvents(res.userId).subscribe(res => {
          this.datasource = res;
        });
      });
  }

  editClick(vehicleId: any)  {
    this.router.navigate(['/event/edit', vehicleId].filter(v => !!v));
  }

  deleteClick(eventId: any)  {
    this.calendarService.deleteEvent(eventId).subscribe({
      next: (res: AuthResponseDto) => {
        this.authenticationService.loggedUserId().subscribe(res => {
          this.calendarService.getEvents(res.userId).subscribe(res => {
            this.datasource = res;
          });
        });
        this.notificationService.showSuccess( this.transloco.translate('notification.deleteEventCorrect'), "Success");
      },
      error: (err: HttpErrorResponse) => {
        this.notificationService.showError( this.transloco.translate('notification.deleteEventFail'), "Failed");
      }
    })
  }
}



