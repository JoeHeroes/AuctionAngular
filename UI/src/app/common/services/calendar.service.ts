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

  getEvents(userId: number): Observable<any> {

    let url_ = this.baseUrl + "/Calendar/EventList/" + userId;

    return this.http.get<any>(url_);
  }

  getEvent(id: number): Observable<any> {

    let url_ = this.baseUrl + "/Calendar/GetOne/" + id;

    return this.http.get<any>(url_);
  }

  addEvent(data: AddEventeDto): Observable<any> {

    let url_ = this.baseUrl + "/Calendar/CreateEvent";

    return this.http.post<any>(url_, data);
  }

  editEvent(data: EditEventeDto): Observable<any> {

    let url_ = this.baseUrl + "/Calendar/EditEvent";
    
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

export interface EditEventeDto {
  id: number;
  title: number;
  description: string;
  date: Date;
  color: string;
  allDay: boolean;
}