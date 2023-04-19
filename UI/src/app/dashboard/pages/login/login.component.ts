import { HttpErrorResponse } from '@angular/common/http';
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
  constructor(private authService: AuthenticationService, private tokenService: TokenService, private router: Router, private route: ActivatedRoute) { }

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
    this.showError = true;
    const login = { ...loginFormValue };
    const userForAuth: UserAuthenticationDto = {
      email: login.email,
      password: login.password
    }
    this.authService.loginUser(userForAuth)
      .subscribe({
        next: (res: AuthResponseDto) => {
          this.showError = false;
          localStorage.setItem("token", res.token);
          this.tokenService.sendAuthStateChangeNotification(res.isAuthSuccessful);
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.showError = false;
          this.errorMessage = err.message;
        }
      })
  }
}
