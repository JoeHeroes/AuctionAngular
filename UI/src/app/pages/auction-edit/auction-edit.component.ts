import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuctionService, EditAuctionDto } from 'src/app/services/auction.service';
import { AuthResponseDto } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-auction-edit',
  templateUrl: './auction-edit.component.html',
  styleUrls: ['./auction-edit.component.css']
})
export class AuctionEditComponent implements OnInit {

  urlSubscription?: Subscription;
  id: any;
  returnUrl!: string;
  showError!: boolean;
  editForm!: FormGroup; 
  errorMessage: string = '';


  locationValue!: string;
  descriptionValue!: string;
  auctionDateValue!: string;

  value!: string;

  constructor(private auctionService: AuctionService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.editForm = new FormGroup({
      location: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required]),
      auctionDate: new FormControl("", [Validators.required, this.validateMinDate.bind(this)]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/auction/panel';
  
    this.urlSubscription = this.route.url.subscribe(segments => {
      this.loadData(segments);
    });
  
  }

  private loadData(url: UrlSegment[]) {
    this.id = url.map(x => x.path).join('/');

    this.auctionService.getAuction(this.id).subscribe(res => {

      this.locationValue = res.location;
      this.descriptionValue = res.description;
      this.value = res.dateTime;
      this.auctionDateValue = this.value.slice(0, 10);
    });
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

  editAuction = (editFormValue: any) => {
    this.showError = false;
    const edit = { ...editFormValue };

    if(edit.auctionDate == ""){
      edit.auctionDate = this.value;
    }

    const editData: EditAuctionDto = {
      id: this.id,
      location: edit.location,
      description: edit.description,
      auctionDate: edit.auctionDate,
    }

    this.auctionService.editAuction(editData)
      .subscribe({
        next: (res: AuthResponseDto) => {
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.showError = true;
        }
      })
  }
}