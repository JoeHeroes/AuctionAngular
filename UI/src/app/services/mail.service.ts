import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MailService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:7257";
  }

  public SendEmail(data: MailDto): Observable<any> {

    let url_ = this.baseUrl + "/Mail/SendEmail";

    return this.http.post<any>(url_, data);
 }
}

export interface MailDto {
  FromId: number;
  ToId: number;
  Title: string;
  Body: string;
}