import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  private azureUrl: string;
  private containerProfileImages: string;
  private containerVehicleImages: string;

  constructor(private http: HttpClient) {
    this.azureUrl = "https://storagevehicleauction.blob.core.windows.net/";
    this.containerProfileImages = "profile-images/";
    this.containerVehicleImages = "vehicle-images/";

  }

  public getProfileImageUrl(fileName: string): string {
    return this.azureUrl + this.containerProfileImages + fileName;
  }


  public getVehicleImageUrl(fileName: string): string {
    return this.azureUrl + this.containerVehicleImages + fileName;
  }
}
