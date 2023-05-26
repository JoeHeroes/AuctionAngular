import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CalendarOptions, DateSelectArg, EventClickArg } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import { CalendarService } from 'src/app/common/services/calendar.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent {

  calendarOptions: CalendarOptions = {
    plugins: [dayGridPlugin],
    initialView: 'dayGridMonth',
    weekends: true,
    editable: true,
    eventClick: this.handleEventClick.bind(this),
    events: [],
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
    alert('Clicked on event: ' + clickInfo.event.title);
    this.router.navigate(['/vehicle/lot', 1].filter(v => !!v));
  }
}