import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService, UserRegisterDto } from 'src/app/common/services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  returnUrl!: string;
  showError!: boolean;
  registerForm!: FormGroup;
  errorMessage: string = '';
  email: string = '';


  constructor(private authService: AuthenticationService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      email: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required]),
      confirmpassword: new FormControl("", [Validators.required]),
      firstname: new FormControl("", [Validators.required]),
      lastname: new FormControl("", [Validators.required]),
      nationality: new FormControl("", [Validators.required]),
      dateofbirth: new FormControl("", [Validators.required]),
      roleid: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/login';
  }

  registerUser = (registerFormValue: any) => {
    this.showError = false;
    const register = { ...registerFormValue };
    const userForAuth: UserRegisterDto = {
      email: register.email,
      password: register.password,
      confirmpassword: register.confirmpassword,
      firstname: register.firstname,
      lastname: register.lastname,
      nationality: register.nationality,
      dateofbirth: register.dateofbirth,
      roleid: register.roleid,

    }
    this.authService.registerUser(userForAuth)
      .subscribe({
        next: (res: any) => {
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.name;
          this.showError = true;
        }
      })
  }
}
