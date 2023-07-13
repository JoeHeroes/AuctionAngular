import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthResponseDto, AuthenticationService } from 'src/app/common/services/authentication.service';
import { AddEventeDto, CalendarService, EditEventeDto } from 'src/app/common/services/calendar.service';

@Component({
  selector: 'app-edit-event',
  templateUrl: './edit-event.component.html',
  styleUrls: ['./edit-event.component.css']
})
export class EditEventComponent {

  urlSubscription?: Subscription;
  id: any;
  returnUrl!: string;
  showError!: boolean;
  eventForm!: FormGroup;
  errorMessage: string = '';
  userId: number = 0;


  titleValue!: string;
  descriptionValue!: string;
  dateValue!: string;
  colorValue!: string;
  allDayValue!: string;
  value!: string;


  constructor(
    private authenticationService: AuthenticationService,
    private calendarService: CalendarService,
    private router: Router,
    private route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.eventForm = new FormGroup({
      title: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required]),
      date: new FormControl("", [Validators.required]),
      color: new FormControl("", [Validators.required]),
      allDay: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/calendar';

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

    const eventData: EditEventeDto = {
      id: this.id,
      title: edit.title,
      description: edit.description,
      date: edit.date,
      color: edit.color,
      allDay: true,
    }
    this.calendarService.editEvent(eventData)
      .subscribe({
        next: (res: AuthResponseDto) => {
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      })
  }
}