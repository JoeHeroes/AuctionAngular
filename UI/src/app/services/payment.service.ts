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
 
  public getPayments(userId: number): Observable<Vehicle[]> {

     let url_ = this.baseUrl + "/Payment/GetAllPayments/"+ userId;

    return this.http.get<any>(url_);
  }


  public createPayment(data: PaymentInfo): Observable<Vehicle[]> {

    let url_ = this.baseUrl + "/Payment/CreatePayment";

   return this.http.post<any>(url_, data);
 }
}

export interface PaymentInfo {
  lotId: number;
  auctionId: number;
  description: string;
  invoiceAmount: number;
  lotLeftLocationDate: Date;
}