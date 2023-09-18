import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vehicle } from './auction-api.generated.service';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:7257";
  }


  public getVehicles(): Observable<Vehicle[]> {

    let url_ = this.baseUrl + "/Vehicle/GetAllVehicle";

    return this.http.get<any>(url_);
  }


  public getVehiclesAuctionEnd(): Observable<Vehicle[]> {

    let url_ = this.baseUrl + "/Vehicle/GetAllVehicleAuctionEnd";

    return this.http.get<any>(url_);
  }

  
  public getBidedVehicles(id: number): Observable<Vehicle[]> {

    let url_ = this.baseUrl + "/Vehicle/GetAllBidedVehicle/" + id;

    return this.http.get<any>(url_);
  }

  public getWonVehicles(id: number): Observable<Vehicle[]> {

    let url_ = this.baseUrl + "/Vehicle/GetAllWonVehicle/" + id;

    return this.http.get<any>(url_);
  }

  public getLostVehicles(id: number): Observable<Vehicle[]> {

    let url_ = this.baseUrl + "/Vehicle/GetAllLostVehicle/" + id;

    return this.http.get<any>(url_);
  }

  public getVehicle(id: number): Observable<any> {

    let url_ = this.baseUrl + "/Vehicle/GetOne/" + id;

    return this.http.get<any>(url_);
  }

  public sellVehicle(id: number): Observable<any> {

    let url_ = this.baseUrl + "/Vehicle/SoldVehicle/" + id;

    return this.http.get<any>(url_);
  }

  public deleteVehicle(id: number): Observable<any> {

    let url_ = this.baseUrl + "/Vehicle/DeleteVehicle/" + id;

    return this.http.delete<any>(url_);
  }

  public bidVehicle(data: BidDto): Observable<any> {

    let url_ = this.baseUrl + "/Vehicle/UpdateBid";

    return this.http.patch<any>(url_, data);
  }

  public watch(data: WatchDto): Observable<any> {

    let url_ = this.baseUrl + "/Vehicle/Watch";

    return this.http.post<any>(url_, data);
  }


  public removeWatch(data: WatchDto): Observable<any> {

    let url_ = this.baseUrl + "/Vehicle/RemoveWatch";

    return this.http.post<any>(url_, data);
  }

  public checkWatch(data: WatchDto): Observable<any> {

    let url_ = this.baseUrl + "/Vehicle/CheckWatch";

    return this.http.post<any>(url_, data);
  }

  public getAllWatch(id: number): Observable<Vehicle[]> {

    let url_ = this.baseUrl + "/Vehicle/AllWatch/" + id;

    return this.http.get<any>(url_);
  }

  public addVehicle(data: CreateVehicleDto) {

    let url_ = this.baseUrl + "/Vehicle/CreateVehicle";
    return this.http.post<any>(url_, data);
  }

  public editVehicle(id: number, data: EditVehicleDto) {

    let url_ = this.baseUrl + "/Vehicle/UpdateVehicle/" + id;
    return this.http.patch<any>(url_, data);
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
  auction: number;
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
  saleTerm: string;
  highlights: string;
  confirm: boolean;
}


export interface EditVehicleDto {
  producer: string;
  modelSpecifer: string;
  modelGeneration: string;
  registrationYear: number;
  color: string;
  auction: number;
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
  saleTerm: string;
  highlights: string;
}








