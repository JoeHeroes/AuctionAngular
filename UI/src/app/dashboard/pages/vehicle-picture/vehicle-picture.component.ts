import { DxDataGridModule } from 'devextreme-angular';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { DataService } from 'src/app/common/services/data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vehicle-picture',
  templateUrl: './vehicle-picture.component.html',
  styleUrls: ['./vehicle-picture.component.css']
})
export class VehiclePictureComponent {
  constructor(private http: HttpClient,
    private router: Router,
    private dataService: DataService) { }

  uploadPictures(files: any) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }

    this.http.post('https://localhost:7257/Vehicle/uploadFile/' + this.dataService.id, formData, { reportProgress: true, observe: 'events' })
      .subscribe({
        next: (event: any) => {
          this.router.navigate(['/vehicle/lot', this.dataService.id].filter(v => !!v));
        },
        error: (err: HttpErrorResponse) => console.log(err)
      });


  }

}
