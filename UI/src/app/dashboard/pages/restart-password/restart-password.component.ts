import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthResponseDto, AuthenticationService, RestartPasswordDto } from 'src/app/common/services/authentication.service';

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

  constructor(private authenticationService: AuthenticationService, private router: Router, private route: ActivatedRoute) { }

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

    const restartData: RestartPasswordDto = {
      email: this.email,
      oldPassword: restart.oldPassword,
      newPassword: restart.newPassword,
      confirmNewPassword: restart.confirmNewPassword

    }
    this.authenticationService.restartPassword(restartData)
      .subscribe({
        next: (res: AuthResponseDto) => {
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      })
  }
}