import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CalendarOptions, EventClickArg } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import { TranslocoService } from '@ngneat/transloco';
import { CalendarService } from 'src/app/common/services/calendar.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})






export class CalendarComponent {

  translations = {
    week: this.translocoService.translate('calendar.week'),
    day: this.translocoService.translate('calendar.day'),
    list: this.translocoService.translate('calendar.list'),
    today: this.translocoService.translate('calendar.today'),
    prev: this.translocoService.translate('calendar.prev'),
    next: this.translocoService.translate('calendar.next'),
    month: this.translocoService.translate('calendar.month'),
    more: this.translocoService.translate('calendar.more'),
    allDayText: this.translocoService.translate('calendar.allDayText'),
    noEventsText: this.translocoService.translate('calendar.noEventsText')
  };

  calendarOptions: CalendarOptions = {
    firstDay: 1,
    plugins: [dayGridPlugin],
    initialView: 'dayGridMonth',
    weekends: true,
    editable: true,
    eventClick: this.handleEventClick.bind(this),
    events: [],
    locale: sessionStorage.getItem('auction:lang')?.toString(),
    buttonText: this.translations,
  };


  constructor(private calendarService: CalendarService,
    private translocoService: TranslocoService,
    private router: Router) {
  }
  ngOnInit(): void {
    this.calendarService.getEvents().subscribe(res => {
      this.calendarOptions.events = res;
    });
  }

  handleEventClick(clickInfo: EventClickArg) {
    var id = clickInfo.event.title.split(" ", 1);
    this.router.navigate(['/vehicle/lot', Number(id)].filter(v => !!v));
  }
}