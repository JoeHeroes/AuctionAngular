import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { Subscription } from 'rxjs';
import { AuthResponseDto, AuthenticationService } from 'src/app/services/authentication.service';
import { MailDto, MailService } from 'src/app/services/mail.service';
import { NotificationService } from 'src/app/services/notification.service';
import { VehicleService } from 'src/app/services/vehicle.service';

@Component({
  selector: 'app-contactus',
  templateUrl: './contactus.component.html',
  styleUrls: ['./contactus.component.css']
})
export class ContactusComponent  implements OnInit {

  urlSubscription?: Subscription;
  returnUrl!: string;
  showError!: boolean;
  sendForm!: FormGroup;
  errorMessage: string = '';
  idTo: any;
  idFrom: any;
  idVehicle: any;
 

  constructor(private vehicleService: VehicleService,
    private notificationService: NotificationService,
    private authenticationService: AuthenticationService,
    private mailService: MailService,
    private router: Router,
    private route: ActivatedRoute,
    private transloco: TranslocoService) { }

  ngOnInit(): void {
    this.sendForm = new FormGroup({
      title: new FormControl("", [Validators.required]),
      body: new FormControl("", [Validators.required]),
    })
    this.urlSubscription = this.route.url.subscribe(segments => {
      this.loadData(segments);
    });

    this.authenticationService.loggedUserId().subscribe(res => {
      this.idFrom = res.userId;
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/vehicle/lot/' + this.idVehicle;
  }

  private loadData(url: UrlSegment[]) {
    this.idVehicle = url.map(x => x.path).join('/');

    this.vehicleService.getVehicle(this.idVehicle).subscribe(res => {
      this.idTo = res.ownerId;
    });

  }
  
  sendEmail = (sendEmail: any) => {
    this.showError = false;
    const send = { ...sendEmail };


    const sendData: MailDto = {
      FromId: this.idFrom,
      ToId: this.idTo,
      Title: send.title,
      Body: send.body,
    }

    if (send.title == "") {
      this.errorMessage = "Title is required";
      this.showError = true;
    }
    else if (send.body == "") {
      this.errorMessage = "Body is required";
      this.showError = true;
    }

    this.mailService.SendEmail(sendData)
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