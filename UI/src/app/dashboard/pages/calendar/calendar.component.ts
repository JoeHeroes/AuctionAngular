import dayGridPlugin from '@fullcalendar/daygrid';
import { CalendarService } from 'src/app/common/services/calendar.service';
import allLocales from '@fullcalendar/core/locales-all'
import { Component } from '@angular/core';
import { CalendarOptions } from '@fullcalendar/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/common/services/authentication.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})


export class CalendarComponent {
  calendarOptions: CalendarOptions = {
    firstDay: 1,
    plugins: [dayGridPlugin],
    initialView: 'dayGridMonth',
    weekends: true,
    editable: true,
    events: [],
    locales: allLocales,
    locale: sessionStorage.getItem('auction:lang')?.toString(),
  };


  constructor(private authenticationService: AuthenticationService,
    private calendarService: CalendarService) {
  }
  ngOnInit(): void {


    this.authenticationService.loggedUserId().subscribe(res => {
      this.calendarService.getEvents(res.userId).subscribe(res => {
        this.calendarOptions.events = res;
      });
    });
  }

}