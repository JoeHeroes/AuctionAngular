import { VehicleService, BidDto, WatchDto } from './../../../common/services/vehicle.service';
import { Subscription } from 'rxjs';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { NotificationService } from 'src/app/common/services/notification.service';
import { TranslocoService } from '@ngneat/transloco';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, UrlSegment } from '@angular/router';

@Component({
  selector: 'app-lot',
  templateUrl: './lot.component.html',
  styleUrls: ['./lot.component.css']
})
export class LotComponent implements OnInit {
  urlSubscription?: Subscription;
  id: any;
  datasource: any = {
    Producer: "",
    ModelSpecifer: "",
    ModelGeneration: "",
    RegistrationYear: 0,
    Color: "",
    BodyType: "",
    EngineCapacity: 0,
    EngineOutput: 0,
    Transmission: "",
    Drive: "",
    MeterReadout: 0,
    Fuel: "",
    NumberKeys: 0,
    ServiceManual: "",
    SecondTireSet: "",
    CurrentBid: 0,
    PrimaryDamage: "",
    SecondaryDamage: "",
    DateTime: Date,
    VIN: "",
    LocationId: 0,
    SalesFinised: false,
    SaleTerm: "",
    Highlights: "",
    WaitingForConfirm: false,
  };
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
    private notificationService: NotificationService,
    private activeRoute: ActivatedRoute,
    private transloco: TranslocoService) {

    this.isUserAuthenticated = false;

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
            this.notificationService.showSuccess( this.transloco.translate('notification.bid'), "Success");
          });
        },
        error: () => {
          this.notificationService.showError( this.transloco.translate('notification.bidFail'), "Failed");
        }
      })
  }
}