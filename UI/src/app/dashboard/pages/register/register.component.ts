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
  roles: any;


  constructor(private authenticationService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute) {


    this.authenticationService.getRoles().subscribe(res => {
      this.roles = res;
    });
  }

  ngOnInit(): void {


    this.registerForm = new FormGroup({
      email: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required]),
      confirmpassword: new FormControl("", [Validators.required]),
      name: new FormControl("", [Validators.required]),
      surename: new FormControl("", [Validators.required]),
      nationality: new FormControl("", [Validators.required]),
      phone: new FormControl("", [Validators.required]),
      dateOfBirth: new FormControl('', [Validators.required, this.validateMaxDate.bind(this)]),
      roleid: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/login';
  }

  validateMaxDate(control: FormControl): { [key: string]: any } | null {
    const selectedDate = new Date(control.value);
    const currentDate = new Date();
    currentDate.setHours(0, 0, 0, 0);

    if (selectedDate > currentDate) {
      return { maxDate: true };
    }
    return null;
  }


  registerUser = (registerFormValue: any) => {
    this.showError = false;
    const register = { ...registerFormValue };
    const userForAuth: UserRegisterDto = {
      email: register.email,
      password: register.password,
      confirmpassword: register.confirmpassword,
      name: register.name,
      sureName: register.surename,
      nationality: register.nationality,
      phone: register.phone,
      dateOfBirth: register.dateOfBirth,
      roleid: register.roleid,

    }
    this.authenticationService.registerUser(userForAuth)
      .subscribe({
        next: (res: any) => {
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
        }
      })
  }
}
