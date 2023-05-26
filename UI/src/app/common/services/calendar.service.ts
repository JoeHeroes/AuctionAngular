import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {

  constructor(private http: HttpClient) { }

  public getEvents(): Observable<any> {

    let url_ = "https://localhost:7257/Calendar/eventList";

    return this.http.get<any>(url_);
  }
}
