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


    const ageDifference = currentDate.getFullYear() - selectedDate.getFullYear();

    if (ageDifference < 18) {
      return { minAge: true };
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
      this.errorMessage = this.transloco.translate('message.emailRequired');
      this.showError = true;
    }
    else if (register.password == "") {
      this.errorMessage = this.transloco.translate('message.passwordRequired');
      this.showError = true;
    }
    else if (register.password != register.confirmpassword) {
      this.errorMessage = this.transloco.translate('message.passwordAndConfirmPasswordSame');
      this.showError = true;
    }
    else if (register.confirmpassword == "") {
      this.errorMessage = this.transloco.translate('message.confirmPasswordRequired');
      this.showError = true;
    }
    else if (register.name == "") {
      this.errorMessage = this.transloco.translate('message.nameRequired');
      this.showError = true;
    }
    else if (register.surename == "") {
      this.errorMessage = this.transloco.translate('message.surenameRequired');
      this.showError = true;
    }
    else if (register.nationality == "") {
      this.errorMessage = this.transloco.translate('message.nationalityRequired');
      this.showError = true;
    }
    else if (register.phone == "") {
      this.errorMessage = this.transloco.translate('message.phoneRequired');
      this.showError = true;
    }
    else if (register.dateOfBirth == "") {
      this.errorMessage = this.transloco.translate('message.dateBirthRequired');
      this.showError = true;
    }
    else if (register.roleid == "") {
      this.errorMessage = this.transloco.translate('message.roleRequired');
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
