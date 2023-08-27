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

  liveAuction: boolean = false;
  datasource: any;
  time: number = 0;
  index: number = 0;
  id: any;
  user: any;


  bidForm!: FormGroup;

  constructor(private auctionService: AuctionService,
    private authenticationService: AuthenticationService,
    private vehicleService: VehicleService) {
  }

  ngOnInit(): void {
    window.scroll(0, 0);
    this.auctionService.liveAuction().subscribe(res => {
      this.liveAuction = res;
      if (res) {
        this.auctionService.liveAuctionList().subscribe(res => {
          this.datasource = res;
        });

        this.auctionService.startAuction().subscribe(res => {
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
          });
        },
        error: () => {

        }
      })
  }
}
