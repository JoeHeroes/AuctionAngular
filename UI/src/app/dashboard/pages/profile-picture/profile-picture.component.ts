import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ms } from 'date-fns/locale';
import { AuthenticationService } from 'src/app/common/services/authentication.service';

@Component({
  selector: 'app-profile-picture',
  templateUrl: './profile-picture.component.html',
  styleUrls: ['./profile-picture.component.css']
})
export class ProfilePictureComponent {
  successMessage: string | null = null;
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
      this.http.patch('https://localhost:7257/Account/uploadFile/' + res.userId, formData, { reportProgress: true, observe: 'events' })
        .subscribe({
          next: (res: any) => {
            this.successMessage = 'File uploaded successfully.';
          },
          error: (err: HttpErrorResponse) => {
            console.log(err)
          }
        });
    })

  }

}
