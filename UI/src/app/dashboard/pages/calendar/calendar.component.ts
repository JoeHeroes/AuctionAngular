import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CalendarOptions, EventClickArg } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import { TranslocoService } from '@ngneat/transloco';
import { CalendarService } from 'src/app/common/services/calendar.service';
import allLocales from '@fullcalendar/core/locales-all'

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
    eventClick: this.handleEventClick.bind(this),
    events: [],
    locales: allLocales,
    locale: sessionStorage.getItem('auction:lang')?.toString(),
  };


  constructor(private calendarService: CalendarService,
    private router: Router) {
  }
  ngOnInit(): void {
    this.calendarService.getEvents().subscribe(res => {
      this.calendarOptions.events = res;
    });
  }

  handleEventClick(clickInfo: EventClickArg) {
    var id = clickInfo.event.title.split(" ", 1);
    if (!Number.isNaN(Number(id))) {
      this.router.navigate(['/vehicle/lot', Number(id)].filter(v => !!v));
    }

  }
}