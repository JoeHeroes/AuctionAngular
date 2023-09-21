import { Component } from "@angular/core";
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { DataService } from 'src/app/common/services/data.service';
import { NotificationService } from "src/app/common/services/notification.service";
import { TranslocoService } from "@ngneat/transloco";

@Component({
  selector: 'app-vehicle-picture',
  templateUrl: './vehicle-picture.component.html',
  styleUrls: ['./vehicle-picture.component.css']
})
export class VehiclePictureComponent {
  private returnUrl!: string;

  constructor(private http: HttpClient,
    private notificationService: NotificationService,
    private router: Router,
    private dataService: DataService,
    private route: ActivatedRoute,
    private transloco: TranslocoService) { 

      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/vehicle';
    }

  uploadPictures(files: any) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }

    this.http.patch('https://localhost:7257/Vehicle/UploadVehicleImage/' + this.dataService.userId, formData, { reportProgress: true, observe: 'events' })
      .subscribe({
        next: (event: any) => {
          this.notificationService.showSuccess( this.transloco.translate('notification.pictureAddCorrect'), "Success");
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.notificationService.showError( this.transloco.translate('notification.pictureAddFail'), "Failed");
        }
      });
    }
}
