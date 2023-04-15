import { VehicleService, BidDto } from './../../../common/services/vehicle.service';
import { HttpContext, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/common/services/authentication.service';

@Component({
  selector: 'app-lot',
  templateUrl: './lot.component.html',
  styleUrls: ['./lot.component.css']
})
export class LotComponent {
  urlSubscription?: Subscription;
  id: any;
  datasource: any;
  pictures: any;
  user: any;
  slideshowDelay = 2000;



  bidForm!: FormGroup;


  constructor(
    private service: VehicleService,
    private serviceAuth: AuthenticationService,
    private activeRoute: ActivatedRoute,
  ) {
  }

  ngOnInit(): void {
    this.urlSubscription = this.activeRoute.url.subscribe(segments => {
      this.loadData(segments);
    });

    this.bidForm = new FormGroup({
      bidNow: new FormControl("", [Validators.required])
    })

  }

  private loadData(url: UrlSegment[]) {
    this.id = url.map(x => x.path).join('/');

    this.service.getVehicle(this.id).subscribe(res => {
      this.datasource = res;
      this.pictures = res.images
    });





  }



  bidCar(bidValue: any) {


    this.serviceAuth.loggedUserId().subscribe(res => {
      this.user = res;
    });;



    const bid = { ...bidValue };
    const bidDto: BidDto = {
      lotNumber: this.datasource.id,
      bidNow: bid.bidNow,
      userId: this.user.userId
    }


    this.service.bidVehicle(bidDto)
      .subscribe({
        next: () => {
          this.service.getVehicle(this.id).subscribe(res => {
            this.datasource = res;
          });
        },
        error: () => {

        }
      })
  }
}













