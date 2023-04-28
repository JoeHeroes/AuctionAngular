import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vehicle } from './auction-api.generated.service';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private http: HttpClient) { }

  public getVehicles(): Observable<Vehicle[]> {

    let url_ = "https://localhost:7257/Vehicle";

    return this.http.get<any>(url_);
  }

  public getVehicle(id: number): Observable<any> {

    let url_ = "https://localhost:7257/Vehicle/" + id;

    return this.http.get<any>(url_);
  }

  public bidVehicle(bid: BidDto): Observable<any> {

    let url_ = "https://localhost:7257/Vehicle/bid";

    return this.http.post<any>(url_, bid);
  }


  public watch(bid: WatchDto): Observable<any> {

    let url_ = "https://localhost:7257/Vehicle/watch";

    return this.http.post<any>(url_, bid);
  }


  public removeWatch(bid: WatchDto): Observable<any> {

    let url_ = "https://localhost:7257/Vehicle/removeWatch";

    return this.http.post<any>(url_, bid);
  }

  public checkWatch(bid: WatchDto): Observable<any> {

    let url_ = "https://localhost:7257/Vehicle/checkWatch";

    alert("3");

    return this.http.post<any>(url_, bid);
  }

  public getAllVehicleWatch(id: number): Observable<Vehicle[]> {

    let url_ = "https://localhost:7257/Vehicle/getAllVehicleWatch/" + id;

    return this.http.get<any>(url_);
  }
}

export interface WatchDto {
  vehicleId: number;
  userId: number;
}


export interface BidDto {
  lotNumber: number;
  bidNow: number;
  userId: number;
}