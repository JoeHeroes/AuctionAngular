import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-profile-picture',
  templateUrl: './profile-picture.component.html',
  styleUrls: ['./profile-picture.component.css']
})
export class ProfilePictureComponent {
  successMessage: string | null = null;
  returnUrl: string = "/profile";
  constructor(
    private authenticationService: AuthenticationService,
    private http: HttpClient,
    private router: Router) { }

  uploadPictures(files: any) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }
    this.authenticationService.loggedUserId().subscribe(res => {
      this.http.post('https://localhost:7257/StorageLocal/UploadProfileImage/' + res.userId, formData, { reportProgress: true, observe: 'events' })
        .subscribe({
          next: (res: any) => {
            this.successMessage = 'File uploaded successfully.';
            this.router.navigate([this.returnUrl]);
          },
          error: (err: HttpErrorResponse) => {
          }
        });
    })
  }
}