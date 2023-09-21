import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { Subscription } from 'rxjs';
import { AuthResponseDto, AuthenticationService } from 'src/app/common/services/authentication.service';
import { CalendarService, EditEventeDto } from 'src/app/common/services/calendar.service';
import { NotificationService } from 'src/app/common/services/notification.service';

@Component({
  selector: 'app-event-edit',
  templateUrl: './event-edit.component.html',
  styleUrls: ['./event-edit.component.css']
})
export class EventEditComponent {

  urlSubscription?: Subscription;
  id: any;
  returnUrl!: string;
  showError!: boolean;
  eventForm!: FormGroup;
  errorMessage: string = '';
  userId: number = 0;
  day: boolean = true;
 
  titleValue!: string;
  descriptionValue!: string;
  dateValue!: string;
  colorValue!: string;
  allDayValue!: string;
  value!: string;

  constructor(
    private authenticationService: AuthenticationService,
    private calendarService: CalendarService,
    private notificationService: NotificationService,
    private router: Router,
    private route: ActivatedRoute,
    private transloco: TranslocoService) { }

  ngOnInit(): void {
    this.eventForm = new FormGroup({
      title: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required]),
      date: new FormControl("", [Validators.required]),
      color: new FormControl("", [Validators.required]),
      allDay: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/calendar/manage';

    this.urlSubscription = this.route.url.subscribe(segments => {
      this.loadData(segments);
    });
  }

  private loadData(url: UrlSegment[]) {
    this.id = url.map(x => x.path).join('/');

    this.authenticationService.loggedUserId().subscribe(res => {
      this.userId = res.userId;

      this.calendarService.getEvent(this.id).subscribe(res => {
        this.titleValue = res.title;
        this.descriptionValue = res.description;
        this.colorValue = res.color;
        this.allDayValue = res.allDay;
        this.dateValue = res.start;
      })
    });
  }


  addEvent = (editFormValue: any) => {
    this.showError = false;
    const edit = { ...editFormValue };

    if(edit.date == ""){
      edit.date = this.value;
    }

    if(edit.allDay=="true"){
      this.day = true
    }
    else{
      this.day = false
    }

    const eventData: EditEventeDto = {
      id: this.id,
      title: edit.title,
      description: edit.description,
      date: edit.date,
      color: edit.color,
      allDay: this.day,
    }

    this.calendarService.editEvent(eventData)
      .subscribe({
        next: (res: AuthResponseDto) => {
          this.notificationService.showSuccess( this.transloco.translate('notification.editEventCorrect'), "Success");
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.notificationService.showError( this.transloco.translate('notification.editEventFail'), "Failed");
        }
      })
  }
}