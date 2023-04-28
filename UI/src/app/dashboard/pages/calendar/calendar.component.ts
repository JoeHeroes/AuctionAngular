import { Component } from '@angular/core';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent {
  now: Date = new Date();

  currentValue: Date = new Date();

  minDateValue: Date | null = null;

  maxDateValue: Date | null = null;

  disabledDates: Function | null = null;

  zoomLevels: string[] = [
    'month', 'year', 'decade', 'century',
  ];

  weekDays: { id: number; text: string }[] = [
    { id: 0, text: 'Sunday' },
    { id: 1, text: 'Monday' },
    { id: 2, text: 'Tuesday' },
    { id: 3, text: 'Wednesday' },
    { id: 4, text: 'Thursday' },
    { id: 5, text: 'Friday' },
    { id: 6, text: 'Saturday' },
  ];

  weekNumberRules: string[] = [
    'auto', 'firstDay', 'firstFourDays', 'fullWeek',
  ];

  cellTemplate = 'cell';



  holidays: any = [[1, 0], [4, 6], [25, 11]];



}
