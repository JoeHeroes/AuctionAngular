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

    let url_ = "https://localhost:7257/Vehicle/getAll";

    return this.http.get<any>(url_);
  }

  public getBidedVehicles(id: number): Observable<Vehicle[]> {

    let url_ = "https://localhost:7257/Vehicle/getAllBided/" + id;

    return this.http.get<any>(url_);
  }

  public getWonVehicles(id: number): Observable<Vehicle[]> {

    let url_ = "https://localhost:7257/Vehicle/getAllWon/" + id;

    return this.http.get<any>(url_);
  }

  public getLostVehicles(id: number): Observable<Vehicle[]> {

    let url_ = "https://localhost:7257/Vehicle/getAllLost/" + id;

    return this.http.get<any>(url_);
  }

  public getVehicle(id: number): Observable<any> {

    let url_ = "https://localhost:7257/Vehicle/getOne/" + id;

    return this.http.get<any>(url_);
  }

  public bidVehicle(data: BidDto): Observable<any> {

    let url_ = "https://localhost:7257/Vehicle/bid";

    return this.http.patch<any>(url_, data);
  }

  public watch(data: WatchDto): Observable<any> {

    let url_ = "https://localhost:7257/Vehicle/watch";

    return this.http.post<any>(url_, data);
  }


  public removeWatch(data: WatchDto): Observable<any> {

    let url_ = "https://localhost:7257/Vehicle/removeWatch";

    return this.http.post<any>(url_, data);
  }

  public checkWatch(data: WatchDto): Observable<any> {

    let url_ = "https://localhost:7257/Vehicle/checkWatch";

    return this.http.post<any>(url_, data);
  }

  public getAllWatch(id: number): Observable<Vehicle[]> {

    let url_ = "https://localhost:7257/Vehicle/allWatch/" + id;

    return this.http.get<any>(url_);
  }

  public addVehicle(data: CreateVehicleDto) {

    let url_ = "https://localhost:7257/Vehicle/create";
    return this.http.post<any>(url_, data);
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


export interface CreateVehicleDto {
  producer: string;
  modelSpecifer: string;
  modelGeneration: string;
  registrationYear: number;
  color: string;
  locationId: number;
  bodyType: string;
  transmission: string;
  drive: string;
  meterReadout: number;
  fuel: string;
  primaryDamage: string;
  secondaryDamage: string;
  engineCapacity: number;
  engineOutput: number;
  numberKeys: number;
  serviceManual: boolean;
  secondTireSet: boolean;
  VIN: string;
  dateTime: Date;
  saleTerm: string;
  highlights: string;
}








