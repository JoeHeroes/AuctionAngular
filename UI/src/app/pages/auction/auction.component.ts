import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TranslocoService } from '@ngneat/transloco';
import { Subscription, interval } from 'rxjs';
import { AuctionService } from 'src/app/services/auction.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';
import { BidDto, VehicleService } from 'src/app/services/vehicle.service';

@Component({
  selector: 'app-auction',
  templateUrl: './auction.component.html',
  styleUrls: ['./auction.component.css']
})
export class AuctionComponent implements OnInit {

  mySubscription: Subscription
  liveAuction: boolean = false;
  datasource: any;
  time: number = 0;
  index: number = 0;
  id: any;
  user: any; 
  bidForm!: FormGroup;

  constructor(private auctionService: AuctionService,
    private notificationService: NotificationService,
    private authenticationService: AuthenticationService,
    private vehicleService: VehicleService,
    private transloco: TranslocoService) {

      this.mySubscription = interval(200).subscribe((x => {
        this.doTimer();
      }));
  }

  ngOnInit(): void {
    window.scroll(0, 0);
    this.auctionService.liveAuction().subscribe(res => {
      this.liveAuction = res;
      if (res) {
        this.auctionService.liveAuctionList().subscribe(res => {
          this.datasource = res;
        });
      }
    });

    this.authenticationService.loggedUserId().subscribe(res => {
      this.user = res;
    });

    this.bidForm = new FormGroup({
      bidNow: new FormControl("", [Validators.required])
    })
  }

  doTimer() {
    this.time++;
    if (this.time > 110) {
      this.time = 0;
      this.index++;
      if (this.index == this.datasource.length) {
        this.liveAuction = false;
        this.auctionService.endAuction().subscribe(res => {
        });
      }
    }
  }

  bidCar(bidValue: any) {
    const bid = { ...bidValue };
    const bidDto: BidDto = {
      lotNumber: this.datasource[this.index].id,
      bidNow: bid.bidNow,
      userId: this.user.userId
    }

    this.vehicleService.bidVehicle(bidDto)
      .subscribe({
        next: () => {
          this.auctionService.liveAuctionList().subscribe(res => {
            this.datasource = res;
            this.notificationService.showSuccess( this.transloco.translate('notification.bidVehicleCorrect'), "Success");
          });
        },
        error: () => {
          this.notificationService.showError( this.transloco.translate('notification.bidVehicleFail'), "Failed");
        }
      })
  }
}
