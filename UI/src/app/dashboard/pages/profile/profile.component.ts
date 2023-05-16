import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthResponseDto, AuthenticationService, EditProfileDto } from 'src/app/common/services/authentication.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  returnUrl!: string;
  showError!: boolean;
  editForm!: FormGroup;
  errorMessage: string = '';
  email: string = '';
  userId: number = 0;
  urlSubscription?: Subscription;
  datasource: any;
  user: any;
  isPictureNull!: boolean;

  constructor(private authenticationService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.editForm = new FormGroup({
      userId: new FormControl("", [Validators.required]),
      name: new FormControl("", [Validators.required]),
      sureName: new FormControl("", [Validators.required]),
      nationality: new FormControl("", [Validators.required]),
      phone: new FormControl("", [Validators.required]),
      dateOfBirth: new FormControl("", [Validators.required]),
      pathPicture: new FormControl("", [Validators.required]),
    })

    this.authenticationService.loggedUserId().subscribe(res => {
      this.userId = res.userId;
      this.authenticationService.getUserInfo(res.userId).subscribe(res => {
        this.email = res.email;
        this.datasource = res;

        if (res.profilePicture == "") {
          this.isPictureNull = true;
        } else {
          this.isPictureNull = false;
        }
      });
    });
  }

  editProfile = (editFormValue: any) => {
    this.showError = false;
    const edit = { ...editFormValue };

    const editData: EditProfileDto = {
      userId: this.userId,
      name: edit.name,
      sureName: edit.sureName,
      phone: edit.phone,
      nationality: edit.nationality,
      dateofbirth: edit.dateOfBirth,
    }
    this.authenticationService.editProfile(editData)
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