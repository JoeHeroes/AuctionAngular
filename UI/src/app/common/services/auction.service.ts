import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuctionService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:7257";
  }

  liveAuction(): Observable<any> {

    let url_ = this.baseUrl + "/Auction/LiveAuction";

    return this.http.get<any>(url_);
  }

  liveAuctionList(): Observable<any> {

    let url_ = this.baseUrl + "/Auction/LiveAuctionList";

    return this.http.get<any>(url_);
  }

  startAuction(): Observable<any> {

    let url_ = this.baseUrl + "/Auction/StartAuction";

    return this.http.get<any>(url_);
  }
}