import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:7257";
  }
 
  public downloadInvoice(data: InvoiceInfo): Observable<Blob> {

    let url_ = this.baseUrl + "/Invoice/GeneratePDF";
    return this.http.post(url_, data, {responseType: 'blob'});
  }
}


export interface InvoiceInfo {
    userId: number;
    vehicleId: number;
    locationId: number;
  }