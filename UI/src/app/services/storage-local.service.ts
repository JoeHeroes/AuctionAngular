import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StorageLocalService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = "https://localhost:7257";
  }
 
  public getPayments(userId: number) {
     
  }

  public getProfileImageUrl(fileName: string) {
    let url_ = this.baseUrl + "/StorageLocal/GetProfileImage/"+ fileName;
    return this.http.get(url_, { responseType: 'blob' });
  }
}
