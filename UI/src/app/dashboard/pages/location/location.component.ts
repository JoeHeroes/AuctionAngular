import { Component } from '@angular/core';
import { LocationService } from 'src/app/common/services/location.service';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent{
  locations!: Location[];

  constructor(private locationService: LocationService) {
    this.locationService.getLocations().subscribe(res => this.locations = res)
  }
}

export default class Location {
  ID: number | undefined;
  CompanyName: string | undefined;
  name: string | undefined;
  phone: string | undefined;
  email: string | undefined;
  city: string | undefined;
  street: string | undefined;
  postalCode: string | undefined;
  profileImg: string | undefined;
}