import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { AuctionService } from 'src/app/services/auction.service';
import { AuthResponseDto } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-admin-panel-auctions',
  templateUrl: './admin-panel-auctions.component.html',
  styleUrls: ['./admin-panel-auctions.component.css']
})
export class AdminPanelAuctionsComponent  {
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private auctionService: AuctionService,
    private notificationService: NotificationService,
    private router: Router,
    private transloco: TranslocoService) {
    this.auctionService.getAuctionList().subscribe(res => {
      this.datasource = res;
    });
  }

  editClick(auctionId: any)  {
    this.router.navigate(['/auction/edit', auctionId].filter(v => !!v));
  }

  deleteClick(auctionId: any)  {
    this.auctionService.deleteAuction(auctionId).subscribe({
      next: (res: AuthResponseDto) => {
        this.auctionService.getAuctionList().subscribe(res => {
          this.datasource = res;
        });
        this.notificationService.showSuccess( this.transloco.translate('notification.deleteVehicleCorrect'), "Success");
      },
      error: (err: HttpErrorResponse) => {
        this.notificationService.showError( this.transloco.translate('notification.deleteVehicleFail'), "Failed");
      }
    })
  }
}






