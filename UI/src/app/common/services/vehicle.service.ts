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

    return this.http.post<any>(url_, bid);
  }

  public getAllWatch(id: number): Observable<Vehicle[]> {

    let url_ = "https://localhost:7257/Vehicle/allWatch/" + id;

    return this.http.get<any>(url_);
  }

  public addVehicle(dto: CreateVehicleDto) {

    let url_ = "https://localhost:7257/Vehicle/create";

    return this.http.post<any>(url_, dto);
  }



  public addPictureVehicle(formData: any) {

    let url_ = "https://localhost:7257/Vehicle/uploadFile";

    return this.http.post<any>(url_, formData, { reportProgress: true, observe: 'events' });
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
  registrationYear: string;
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
}








