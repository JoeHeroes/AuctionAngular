import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:7257";
  }

  public getEvents(): Observable<any> {

    let url_ = this.baseUrl + "/Calendar/EventList";

    return this.http.get<any>(url_);
  }
  public addEvent(data: AddEventeDto): Observable<any> {

    let url_ = this.baseUrl + "/Calendar/CreateEvent";

    return this.http.post<any>(url_, data);
  }
}

export interface AddEventeDto {
  title: number;
  description: string;
  date: Date;
  color: string;
  allDay: boolean;
  owner: number;
}
