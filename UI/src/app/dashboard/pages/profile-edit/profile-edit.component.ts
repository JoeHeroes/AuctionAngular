import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthResponseDto, AuthenticationService, EditProfileDto } from 'src/app/common/services/authentication.service';

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

  constructor(private authenticationService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.editForm = new FormGroup({
      userId: new FormControl("", [Validators.required]),
      firstName: new FormControl("", [Validators.required]),
      lastName: new FormControl("", [Validators.required]),
      nationality: new FormControl("", [Validators.required]),
      pathPicture: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/profile';

    this.authenticationService.loggedUserId().subscribe(res => {
      this.userId = res.userId;
      this.authenticationService.getUserInfo(res.userId).subscribe(res => {
        this.email = res.email;
      });
    });
  }

  editProfile = (editFormValue: any) => {
    this.showError = false;
    const edit = { ...editFormValue };

    const editData: EditProfileDto = {
      userId: this.userId,
      firstName: edit.firstName,
      lastName: edit.lastName,
      nationality: edit.nationality,
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