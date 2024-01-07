import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { AuthResponseDto, AuthenticationService, EditProfileDto } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent implements OnInit {

  returnUrl!: string;
  showError!: boolean;
  editForm!: FormGroup;
  errorMessage: string = '';
  email: string = '';
  userId: number = 0;
  nameValue!: string;
  sureNameValue!: string;
  phoneValue!: string;
  nationalityValue!: string;
  dateOfBirthValue!: string;
  value!: string;

  constructor(private authenticationService: AuthenticationService,
    private notificationService: NotificationService,
    private router: Router,
    private route: ActivatedRoute,
    private transloco: TranslocoService) { }

  ngOnInit(): void {
    this.editForm = new FormGroup({
      userId: new FormControl("", [Validators.required]),
      name: new FormControl("", [Validators.required]),
      sureName: new FormControl("", [Validators.required]),
      phone: new FormControl("", [Validators.required]),
      nationality: new FormControl("", [Validators.required]),
      dateOfBirth: new FormControl("", [Validators.required, this.validateMaxDate.bind(this)]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/profile';

    this.authenticationService.loggedUserId().subscribe(res => {
      this.userId = res.userId;
      this.authenticationService.getUserInfo(res.userId).subscribe(res => {

        this.nameValue = res.name;
        this.sureNameValue = res.sureName;
        this.phoneValue = res.phone;
        this.nationalityValue = res.nationality;
        this.value = res.dateOfBirth;
        this.dateOfBirthValue = this.value.slice(0, 10);
      });
    });
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

  editProfile = (editFormValue: any) => {
    this.showError = false;
    const edit = { ...editFormValue };

    if(edit.dateOfBirth == ""){
      edit.dateOfBirth = this.value;
    }

    const editData: EditProfileDto = {
      userId: this.userId,
      name: edit.name,
      sureName: edit.sureName,
      phone: edit.phone,
      nationality: edit.nationality,
      dateOfBirth: edit.dateOfBirth,
    }

    this.authenticationService.editProfile(editData)
      .subscribe({
        next: (res: AuthResponseDto) => {
          this.notificationService.showSuccess( this.transloco.translate('notification.profileEditCorrect'), "Success");
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.notificationService.showError( this.transloco.translate('notification.profileEditFail'), "Failed");
          this.errorMessage = err.message;
          this.showError = true;
        }
      })
  }
}