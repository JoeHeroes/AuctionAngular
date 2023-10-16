import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { AuthenticationService, UserRegisterDto } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';


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
    private notificationService: NotificationService, 
    private router: Router,
    private route: ActivatedRoute,
    private transloco: TranslocoService) {
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
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/account/login';
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
 
    if (register.email == "") {
      this.errorMessage = "Email is required";
      this.showError = true;
    }
    else if (register.password == "") {
      this.errorMessage = "Password is required";
      this.showError = true;
    }
    else if (register.confirmpassword == "") {
      this.errorMessage = "Confirm password is required";
      this.showError = true;
    }
    else if (register.name == "") {
      this.errorMessage = "Name is required";
      this.showError = true;
    }
    else if (register.surename == "") {
      this.errorMessage = "Surename is required";
      this.showError = true;
    }
    else if (register.nationality == "") {
      this.errorMessage = "Nationality is required";
      this.showError = true;
    }
    else if (register.phone == "") {
      this.errorMessage = "Phone is required";
      this.showError = true;
    }
    else if (register.dateOfBirth == "") {
      this.errorMessage = "Date of birth is required";
      this.showError = true;
    }
    else if (register.roleid == "") {
      this.errorMessage = "Role ID is required";
      this.showError = true;
    }
    else {
      this.authenticationService.registerUser(userForAuth)
        .subscribe({
          next: (res: any) => {

          },
          error: () => {
            this.notificationService.showSuccess( this.transloco.translate('notification.registeredCorrect'), "Success");
            this.router.navigate([this.returnUrl]);
          }
        })
    }
  }
}
