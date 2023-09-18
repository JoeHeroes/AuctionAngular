import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuctionService } from 'src/app/common/services/auction.service';

@Component({
  selector: 'app-vehicle-set',
  templateUrl: './vehicle-set.component.html',
  styleUrls: ['./vehicle-set.component.css']
})
export class VehicleSetComponent  implements OnInit {

  urlSubscription?: Subscription;
  returnUrl: string = "/panel";
  id: any;
  auctions: any;


  setAuctionForm !: FormGroup;
  errorMessage: string = '';
  constructor(private auctionService: AuctionService,
    private router: Router,
    private activeRoute: ActivatedRoute) {
      this.auctionService.getAuctionList().subscribe(res => {
        this.auctions = res;
      });
  }

  private loadData(url: UrlSegment[]) {
    this.id = url.map(x => x.path).join('/');
  }

  ngOnInit(): void {

    this.urlSubscription = this.activeRoute.url.subscribe(segments => {
      this.loadData(segments);
    });

    this.setAuctionForm = new FormGroup({
      producer: new FormControl("", [Validators.required]),
    })
    
    
  }


  setAuctionForVehicle = (vehicleFormValue: any) => {


  }
}