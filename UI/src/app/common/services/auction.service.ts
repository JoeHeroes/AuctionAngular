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


  getAuctionList(): Observable<any> {

    let url_ = this.baseUrl + "/Auction/AuctionList";

    return this.http.get<any>(url_);
  }

  getAuction(id: number): Observable<any> {

    let url_ = this.baseUrl + "/Auction/GetOne/" + id;

    return this.http.get<any>(url_);
  }

  editAuction(data: EditAuctionDto): Observable<any> {

    let url_ = this.baseUrl + "/Auction/EditAuction";

    return this.http.post<any>(url_, data);
  }

  deleteAuction(id: number): Observable<any> {

    let url_ = this.baseUrl + "/Auction/DeleteAuction/" + id;

    return this.http.delete<any>(url_);
  }

  addAuction(data: AddAuctionDto): Observable<any> {

    let url_ = this.baseUrl + "/Auction/CreateAuction";
    
    return this.http.post<any>(url_, data);
  }


  startAuction(): Observable<any> {

    let url_ = this.baseUrl + "/Auction/StartAuction";

    return this.http.get<any>(url_);
  }


  endAuction(): Observable<any> {

    let url_ = this.baseUrl + "/Auction/EndAuction";

    return this.http.get<any>(url_);
  }
}

export interface AddAuctionDto {
  location: string;
  description: string;
  auctionDate: Date;
}

export interface EditAuctionDto {
  id: number;
  location: string;
  description: string;
  auctionDate: Date;
}
