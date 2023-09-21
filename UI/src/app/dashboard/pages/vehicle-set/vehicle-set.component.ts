import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { Subscription } from 'rxjs';
import { AuctionService } from 'src/app/common/services/auction.service';
import { AuthResponseDto } from 'src/app/common/services/authentication.service';
import { NotificationService } from 'src/app/common/services/notification.service';
import { SetAuctionVehcileDto, VehicleService } from 'src/app/common/services/vehicle.service';

@Component({
  selector: 'app-vehicle-set',
  templateUrl: './vehicle-set.component.html',
  styleUrls: ['./vehicle-set.component.css']
})
export class VehicleSetComponent  implements OnInit {

  urlSubscription?: Subscription;
  returnUrl: string = "/vehicle/verification";
  id: any;
  auctions: any;

  setAuctionForm !: FormGroup;
  errorMessage: string = '';
  constructor(private auctionService: AuctionService,
    private vehicleService: VehicleService,
    private notificationService: NotificationService,
    private router: Router,
    private activeRoute: ActivatedRoute,
    private transloco: TranslocoService) {
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
      auction: new FormControl("", [Validators.required]),
    })
  }

  setAuctionForVehicle = (setAuctionForm: any) => {
    const set = { ...setAuctionForm };


    const setData: SetAuctionVehcileDto = {
      userId: this.id,
      auctionId: set.auction,
    }

    this.vehicleService.setAuctionForVehicle(setData)
      .subscribe({
        next: (res: AuthResponseDto) => {
          this.notificationService.showSuccess( this.transloco.translate('notification.setAuctionCorrect'), "Success");
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.notificationService.showError( this.transloco.translate('notification.setAuctionFail'), "Failed");
          this.errorMessage = err.message;
        }
      })
  }
}