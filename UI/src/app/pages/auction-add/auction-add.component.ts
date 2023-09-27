import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { AddAuctionDto, AuctionService } from 'src/app/services/auction.service';
import { AuthResponseDto } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';


@Component({
  selector: 'app-auction-add',
  templateUrl: './auction-add.component.html',
  styleUrls: ['./auction-add.component.css']
})
export class AuctionAddComponent implements OnInit {

  returnUrl!: string;
  showError!: boolean;
  addForm!: FormGroup;
  errorMessage: string = '';

  constructor(private auctionService: AuctionService,
    private notificationService: NotificationService,
    private router: Router,
    private route: ActivatedRoute,
    private transloco: TranslocoService) { }

  ngOnInit(): void {
    this.addForm = new FormGroup({
      location: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required]),
      auctionDate: new FormControl("", [Validators.required, this.validateMinDate.bind(this)]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/auction/panel';
  }

  validateMinDate(control: FormControl): { [key: string]: any } | null {
    const selectedDate = new Date(control.value);
    const currentDate = new Date();
    currentDate.setHours(0, 0, 0, 0);

    if (selectedDate < currentDate) {
      return { minDate: true };
    }
    return null;
  }

  addAuction = (addFormValue: any) => {
    this.showError = false;
    const add = { ...addFormValue };


    const addData: AddAuctionDto = {
      location: add.location,
      description: add.description,
      auctionDate: add.auctionDate,
    }

    if (add.location == "") {
      this.errorMessage = "Location is required";
      this.showError = true;
    }
    else if (add.description == "") {
      this.errorMessage = "Description is required";
      this.showError = true;
    }
    else if (add.auctionDate == "") {
      this.errorMessage = "Auction date is required";
      this.showError = true;
    }
    
    this.auctionService.addAuction(addData)
      .subscribe({
        next: (res: AuthResponseDto) => {
          this.notificationService.showSuccess( this.transloco.translate('notification.addAuctionCorrect'), "Success");
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.notificationService.showError( this.transloco.translate('notification.addAuctionFail'), "Failed");
        }
      })
  }
}