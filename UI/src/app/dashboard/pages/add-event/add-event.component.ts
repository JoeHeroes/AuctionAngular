import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthResponseDto, AuthenticationService } from 'src/app/common/services/authentication.service';
import { AddEventeDto, CalendarService } from 'src/app/common/services/calendar.service';

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css']
})
export class AddEventComponent implements OnInit {

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