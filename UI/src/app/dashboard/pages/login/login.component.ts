import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService, AuthResponseDto, UserAuthenticationDto } from 'src/app/common/services/authentication.service';

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
  constructor(private authService: AuthenticationService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl("", [Validators.required, Validators.email]),
      password: new FormControl("", [Validators.required])
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }


  /*doLogin = (e: SubmitEvent) => {
     e.preventDefault();
     this.submitting = true;
     this.auth.getToken(new LoginCredentials({user: this.credentials}))
       .subscribe({
         next: response => {
           this.submitting = false;
           this.error = undefined;
           this.tokenService.set(response.token!, response.expiresAt!);
           this.router.navigate([this.returnTo ?? "dashboard"], {relativeTo: null});
         },
         error: (err: ITecsProblemDetails) => {
           this.submitting = false;
           this.error = err;
         }
       });
   }
 */

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
          this.authService.sendAuthStateChangeNotification(res.isAuthSuccessful);
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.showError = false;
          this.errorMessage = err.message;
        }
      })
  }
}
