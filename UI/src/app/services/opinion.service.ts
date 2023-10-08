import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vehicle } from './auction-api.generated.service';

@Injectable({
  providedIn: 'root'
})
export class OpinionService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:7257";
  }

  public getOpinion(id: number): Observable<any> {

    let url_ = this.baseUrl + "/Opinion/GetOne/" + id;

    return this.http.get<any>(url_);
  }

  public addOpinion(data: AddOpinionDto) {

    let url_ = this.baseUrl + "/Opinion/CreateOpinion";
    return this.http.post<any>(url_, data);
  }
}



export interface AddOpinionDto {
  description: string,
  origin: string,
  valuation: string,
  condition: number,
  vehicleId: number,
}








