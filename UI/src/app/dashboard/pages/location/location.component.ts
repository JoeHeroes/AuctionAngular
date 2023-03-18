import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { LocationService } from 'src/app/common/services/location.service';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent implements OnInit {
  locations!: Location[];

  constructor(private service: LocationService) {


    this.service.getLocations()
      .subscribe({
        next: (res: any) => {

          this.locations = res
        },
        error: (err: HttpErrorResponse) => {

        }
      })

  }
  ngOnInit(): void {
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