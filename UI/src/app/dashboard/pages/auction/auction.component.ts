import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs/internal/Subscription';
import { interval } from 'rxjs/internal/observable/interval';
import { AuctionService } from 'src/app/common/services/auction.service';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { BidDto, VehicleService } from 'src/app/common/services/vehicle.service';

@Component({
  selector: 'app-auction',
  templateUrl: './auction.component.html',
  styleUrls: ['./auction.component.css']
})
export class AuctionComponent implements OnInit {

  mySubscription: Subscription
  liveAuction: boolean = false;
  live: boolean = true;
  datasource: any;
  pictures: any;
  slideshowDelay = 10000;
  time: number = 0;
  index: number = 0;
  id: any;
  user: any;


  bidForm!: FormGroup;

  constructor(private auctionService: AuctionService, private authenticationService: AuthenticationService, private vehicleService: VehicleService) {
    this.mySubscription = interval(500).subscribe((x => {
      this.doStuff();
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


    this.bidForm = new FormGroup({
      bidNow: new FormControl("", [Validators.required])
    })
  }


  doStuff() {
    this.time++;
    if (this.time > 110) {
      this.time = 0;
      this.index++;
      if (this.index == this.datasource.length) {
        this.liveAuction = false;
      }
    }
  }


  bidCar(bidValue: any) {
    this.authenticationService.loggedUserId().subscribe(res => {
      this.user = res;
    });;

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
          });
        },
        error: () => {

        }
      })
  }
}
