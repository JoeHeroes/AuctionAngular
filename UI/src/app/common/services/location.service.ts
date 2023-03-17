import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(private http: HttpClient) { }

  public loginUser() {
    return this.http.post<any>("https://localhost:7257/Account/login");
  }

}
