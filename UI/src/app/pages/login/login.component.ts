import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { AuthResponseDto, AuthenticationService, UserAuthenticationDto } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private returnUrl!: string;

  loginForm!: FormGroup;
  errorMessage: string = '';
  showError: boolean = false;
  constructor(private authenticationService: AuthenticationService,
    private tokenService: TokenService,
    private notificationService: NotificationService,
    private router: Router,
    private route: ActivatedRoute,
    private transloco: TranslocoService) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl("", [Validators.required, Validators.email]),
      password: new FormControl("", [Validators.required])
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  loginUser = (loginFormValue: any) => {
    const login = { ...loginFormValue };

    if (login.email == "") {
      this.errorMessage = this.transloco.translate('message.emailRequired');
      this.showError = true;
    }
    else if (login.password == "") {
      this.errorMessage = this.transloco.translate('message.passwordRequired');
      this.showError = true;
    }
    else {
      const userForAuth: UserAuthenticationDto = {
        email: login.email,
        password: login.password
      }

      this.authenticationService.checkEmail(login.email)
        .subscribe({
          next: (res: any) => {
            if (res == true) {
              this.authenticationService.loginUser(userForAuth)
                .subscribe({
                  next: (res: AuthResponseDto) => {
                    this.tokenService.set(res.token);
                    this.notificationService.showSuccess( this.transloco.translate('notification.loggedIn'), "Success");
                    this.router.navigate([this.returnUrl]);
                  },
                  error: (err: any) => {
                    this.errorMessage = this.transloco.translate('message.incorrectEmailAddressPassword');
                    this.showError = true;
                  }
                })
            }
            else {
              this.errorMessage = this.transloco.translate('message.confirmAccount');
              this.showError = true;
            }
          },
          error: (err: any) => {
            this.errorMessage = this.transloco.translate('message.incorrectEmailAddressPassword');
            this.showError = true;
          }
        });
    }
  }
}