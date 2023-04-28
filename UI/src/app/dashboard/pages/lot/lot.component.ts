import { VehicleService, BidDto, WatchDto } from './../../../common/services/vehicle.service';
import { HttpContext, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { AuctionService } from 'src/app/common/services/auction.service';

@Component({
  selector: 'app-lot',
  templateUrl: './lot.component.html',
  styleUrls: ['./lot.component.css']
})
export class LotComponent implements OnInit {
  urlSubscription?: Subscription;
  id: any;
  datasource: any;
  pictures: any;
  user: any;
  slideshowDelay = 2000;
  bidForm!: FormGroup;
  watchLot: boolean = false;


  constructor(private vehicleService: VehicleService,
    private authenticationService: AuthenticationService,
    private activeRoute: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.urlSubscription = this.activeRoute.url.subscribe(segments => {
      this.loadData(segments);
    });
  }

  private loadData(url: UrlSegment[]) {
    this.id = url.map(x => x.path).join('/');

    this.vehicleService.getVehicle(this.id).subscribe(res => {
      this.datasource = res;
      this.pictures = res.images
    });

    this.bidForm = new FormGroup({
      bidNow: new FormControl("", [Validators.required])
    })

    this.authenticationService.loggedUserId()
      .subscribe(res => {
        this.user = res;
      });

    const watchDto: WatchDto = {
      vehicleId: this.datasource.id,
      userId: this.user.userId
    }

    this.vehicleService.checkWatch(watchDto).subscribe({
      next: () => {
        this.watchLot = true;
        alert("True");
      },
      error: () => {
        alert("False");
      }
    })

  }



  watch() {

    const watchDto: WatchDto = {
      vehicleId: this.datasource.id,
      userId: this.user.userId
    }

    this.vehicleService.watch(watchDto)
      .subscribe({
        next: () => {
          this.watchLot = true;
        },
        error: () => {
        }
      })
  }


  removeWatch() {

    const watchDto: WatchDto = {
      vehicleId: this.datasource.id,
      userId: this.user.userId
    }

    this.vehicleService.removeWatch(watchDto)
      .subscribe({
        next: () => {
          this.watchLot = false;
        },
        error: () => {
        }
      })
  }


  checkWatch() {

    const watchDto: WatchDto = {
      vehicleId: this.datasource.id,
      userId: this.user.userId
    }

    this.vehicleService.checkWatch(watchDto)
      .subscribe({
        next: () => {
          this.watchLot = false;
        },
        error: () => {
        }
      })
  }



  bidCar(bidValue: any) {

    const bid = { ...bidValue };
    const bidDto: BidDto = {
      lotNumber: this.datasource.id,
      bidNow: bid.bidNow,
      userId: this.user.userId
    }


    this.vehicleService.bidVehicle(bidDto)
      .subscribe({
        next: () => {
          this.vehicleService.getVehicle(this.id).subscribe(res => {
            this.datasource = res;
          });
        },
        error: () => {

        }
      })
  }
}













