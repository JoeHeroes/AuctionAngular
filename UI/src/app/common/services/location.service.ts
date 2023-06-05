import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:7257";
  }

  public getLocations(): Observable<any> {

    let url_ = this.baseUrl + "/Location/LocationList";

    return this.http.get<any>(url_);
  }
}
