import { Component } from "@angular/core";
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TranslocoService } from "@ngneat/transloco";
import { NotificationService } from "src/app/services/notification.service";
import { DataService } from "src/app/services/data.service";

@Component({
  selector: 'app-vehicle-picture',
  templateUrl: './vehicle-picture.component.html',
  styleUrls: ['./vehicle-picture.component.css']
})
export class VehiclePictureComponent {
  successMessage: string | null = null;
  private returnUrl!: string;
  constructor(private http: HttpClient,
    private router: Router,
    private dataService: DataService,
    private route: ActivatedRoute) { 

      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/vehicle';
    }

  uploadPictures(files: any) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }
    this.http.post<any>('https://localhost:7257/Vehicle/UploadVehicleImage/' + this.dataService.userId, formData, { reportProgress: true, observe: 'events' })
      .subscribe({
        next: (event: any) => {
          this.successMessage = 'File uploaded successfully.';
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
        }
      });
    }
}
