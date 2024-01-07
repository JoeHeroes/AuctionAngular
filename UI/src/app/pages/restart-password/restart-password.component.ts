import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { AuthResponseDto, AuthenticationService, RestartPasswordDto } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';


@Component({
  selector: 'app-restart-password',
  templateUrl: './restart-password.component.html',
  styleUrls: ['./restart-password.component.css']
})
export class RestartPasswordComponent implements OnInit {
  returnUrl!: string;
  showError!: boolean;
  restartForm!: FormGroup;
  errorMessage: string = '';
  email: string = '';

  constructor(private authenticationService: AuthenticationService, 
    private notificationService: NotificationService,
    private router: Router, 
    private route: ActivatedRoute,
    private transloco: TranslocoService) { }

  ngOnInit(): void {
    this.restartForm = new FormGroup({
      oldPassword: new FormControl("", [Validators.required]),
      newPassword: new FormControl("", [Validators.required]),
      confirmNewPassword: new FormControl("", [Validators.required])
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/profile';

    this.authenticationService.loggedUserId().subscribe(res => {
      this.authenticationService.getUserInfo(res.userId).subscribe(res => {
        this.email = res.email;
      });
    });
  }


  restartPassword = (restartFormValue: any) => {
    this.showError = false;
    const restart = { ...restartFormValue };

    if (restart.oldPassword == "") {
      this.errorMessage = this.transloco.translate('message.oldPasswordRequired');
      this.showError = true;
    }
    else if (restart.newPassword == "") {
      this.errorMessage = this.transloco.translate('message.newPasswordRequired');
      this.showError = true;
    }
    else if (restart.confirmNewPassword == "") {
      this.errorMessage = this.transloco.translate('message.confirmNewPasswordRequired');
      this.showError = true;
    }
    else if (restart.newPassword != restart.confirmNewPassword) {
      this.errorMessage = this.transloco.translate('message.newPasswordSame');
      this.showError = true;
    }
    else if (restart.oldPassword == restart.newPassword) {
      this.errorMessage = this.transloco.translate('message.newAndOldPasswordNotSame');
      this.showError = true;
    }
    else{
      const restartData: RestartPasswordDto = {
        email: this.email,
        oldPassword: restart.oldPassword,
        newPassword: restart.newPassword,
        confirmNewPassword: restart.confirmNewPassword
      }
      this.authenticationService.restartPassword(restartData)
        .subscribe({
          next: (res: AuthResponseDto) => {
            this.notificationService.showSuccess( this.transloco.translate('notification.pictureAddCorrect'), "Success");
            this.router.navigate([this.returnUrl]);
          },
          error: (err: HttpErrorResponse) => {
            this.notificationService.showError( this.transloco.translate('notification.pictureAddFail'), "Failed");
            this.errorMessage = err.message;
            this.showError = true;
          }
        })
    }
  }
}