import { VehicleService, BidDto, WatchDto } from './../../../common/services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, UrlSegment } from '@angular/router';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/common/services/authentication.service';

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
  userId: any;
  bidForm!: FormGroup;
  watchLot: boolean = false;
  isUserAuthenticated: boolean = false;

  watchDto: WatchDto = {
    vehicleId: 0,
    userId: 0
  };


  constructor(private vehicleService: VehicleService,
    private authService: AuthenticationService,
    private authenticationService: AuthenticationService,
    private activeRoute: ActivatedRoute) {

    this.authService.loggedUserId().subscribe({
      next: (res) => {
        this.isUserAuthenticated = true;
      },
      error: () => {
      }
    })
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

    this.authenticationService.loggedUserId().subscribe(res => {
      this.watchDto.userId = res.userId;
      this.watchDto.vehicleId = this.id;
      this.vehicleService.checkWatch(this.watchDto)
        .subscribe({
          next: (res) => {
            this.watchLot = res;
          },
          error: () => {
          }
        })
    });
  }




  watch() {

    this.vehicleService.watch(this.watchDto)
      .subscribe({
        next: () => {
          this.watchLot = true;
        },
        error: () => {
        }
      })
  }


  removeWatch() {
    this.vehicleService.removeWatch(this.watchDto)
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
      userId: this.watchDto.userId
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