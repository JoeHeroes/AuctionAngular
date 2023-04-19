import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vehicle } from './auction-api.generated.service';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private http: HttpClient) { }

  getVehicles(): Observable<Vehicle[]> {

    let url_ = "https://localhost:7257/Vehicle";

    return this.http.get<any>(url_);
  }

  getVehicle(id: number): Observable<any> {

    let url_ = "https://localhost:7257/Vehicle/" + id;

    return this.http.get<any>(url_);
  }

  bidVehicle(bid: BidDto): Observable<any> {

    let url_ = "https://localhost:7257/Vehicle/bid";

    return this.http.post<any>(url_, bid);
  }
}

export interface BidDto {
  lotNumber: number;
  bidNow: number;
  userId: number;
}