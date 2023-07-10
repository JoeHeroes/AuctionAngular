import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService, AuthResponseDto, UserAuthenticationDto } from 'src/app/common/services/authentication.service';
import { TokenService } from 'src/app/common/services/token.service';

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
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl("", [Validators.required, Validators.email]),
      password: new FormControl("", [Validators.required])
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get email() { return this.loginForm.get('email'); }

  get password() { return this.loginForm.get('password'); }

  loginUser = (loginFormValue: any) => {
    const login = { ...loginFormValue };

    if (login.email == "") {
      this.errorMessage = "Email is required";
      this.showError = true;
    }
    else if (login.password == "") {
      this.errorMessage = "Password is required";
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
                    this.router.navigate([this.returnUrl]);
                  },
                  error: (err: any) => {
                    this.errorMessage = "Incorrect email address or password";
                    this.showError = true;
                  }
                })
            }
            else {
              this.errorMessage = "Confirm your account on email";
              this.showError = true;
            }
          },
          error: (err: any) => {
            this.errorMessage = "Incorrect email address or password";
            this.showError = true;
          }
        });
    }
  }
}
