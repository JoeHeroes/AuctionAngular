import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vehicle } from './auction-api.generated.service';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:7257";
  }
 
  public getPayments(): Observable<Vehicle[]> {

    let url_ = "https://localhost:7257/Payment/GetAllPayments";

    return this.http.get<any>(url_);
  }
}
