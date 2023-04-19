import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vehicle } from './auction-api.generated.service';

@Injectable({
  providedIn: 'root'
})
export class AuctionService {

  constructor(private http: HttpClient) { }

  liveAuction(): Observable<any> {

    let url_ = "https://localhost:7257/Auction/live";

    return this.http.get<any>(url_);
  }

  liveAuctionList(): Observable<any> {

    let url_ = "https://localhost:7257/Auction/liveAuctionList";

    return this.http.get<any>(url_);
  }

}