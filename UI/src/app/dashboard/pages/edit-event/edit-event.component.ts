import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthResponseDto, AuthenticationService } from 'src/app/common/services/authentication.service';
import { AddEventeDto, CalendarService } from 'src/app/common/services/calendar.service';

@Component({
  selector: 'app-edit-event',
  templateUrl: './edit-event.component.html',
  styleUrls: ['./edit-event.component.css']
})
export class EditEventComponent {

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
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.eventForm = new FormGroup({
      title: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required]),
      date: new FormControl("", [Validators.required, this.validateMaxDate.bind(this)]),
      color: new FormControl("", [Validators.required]),
      allDay: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/calendar';

    this.authenticationService.loggedUserId().subscribe(res => {
      this.userId = res.userId;
    });
  }


  validateMaxDate(control: FormControl): { [key: string]: any } | null {
    const selectedDate = new Date(control.value);
    const currentDate = new Date();
    currentDate.setHours(0, 0, 0, 0);

    if (selectedDate > currentDate) {
      return { maxDate: true };
    }
    return null;
  }

  addEvent = (editFormValue: any) => {
    this.showError = false;
    const edit = { ...editFormValue };

    const eventData: AddEventeDto = {
      title: edit.title,
      description: edit.description,
      date: edit.date,
      color: edit.color,
      allDay: edit.allDay,
      owner: this.userId,
    }
    this.calendarService.addEvent(eventData)
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