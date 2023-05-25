import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(private http: HttpClient) { }

  public getLocations(): Observable<any> {

    let url_ = "https://localhost:7257/Location/locationList";

    return this.http.get<any>(url_);
  }
}
