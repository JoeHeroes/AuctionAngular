import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthResponseDto, AuthenticationService } from 'src/app/common/services/authentication.service';
import { AddEventeDto, CalendarService } from 'src/app/common/services/calendar.service';

@Component({
  selector: 'app-event-add',
  templateUrl: './event-add.component.html',
  styleUrls: ['./event-add.component.css']
})
export class EventAddComponent implements OnInit {

  returnUrl!: string;
  showError!: boolean;
  eventForm!: FormGroup;
  errorMessage: string = '';
  userId: number = 0;

  constructor(
    private authenticationService: AuthenticationService,
    private calendarService: CalendarService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.eventForm = new FormGroup({
      title: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required]),
      date: new FormControl("", [Validators.required]),
      color: new FormControl("", [Validators.required]),
      allDay: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/calendar';

    this.authenticationService.loggedUserId().subscribe(res => {
      this.userId = res.userId;
    });
  }

  addEvent = (addFormValue: any) => {
    this.showError = false;
    const edit = { ...addFormValue };

    const eventData: AddEventeDto = {
      title: edit.title,
      description: edit.description,
      date: edit.date,
      color: edit.color,
      allDay: edit.allDay,
      owner: this.userId,
    }


    if (edit.title == "") {
      this.errorMessage = "Title is required";
      this.showError = true;
    }
    else if (edit.description == "") {
      this.errorMessage = "Description is required";
      this.showError = true;
    }
    else if (edit.date == "") {
      this.errorMessage = "Date is required";
      this.showError = true;
    }
    else if (edit.color == "") {
      this.errorMessage = "Color is required";
      this.showError = true;
    }
    else if (edit.allDay == "") {
      this.errorMessage = "All day is required";
      this.showError = true;
    }


    this.calendarService.addEvent(eventData)
      .subscribe({
        next: (res: AuthResponseDto) => {
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.showError = true;
        }
      })
  }
}