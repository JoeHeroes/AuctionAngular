import { VehicleService } from './../../../common/services/vehicle.service';
import { HttpContext } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-lot',
  templateUrl: './lot.component.html',
  styleUrls: ['./lot.component.css']
})
export class LotComponent {
  urlSubscription?: Subscription;
  id: any;
  datasource: any;
  pictures: any;
  slideshowDelay = 2000;


  images: string[] = [
    'images/gallery/1.jpg',
    'images/gallery/2.jpg',
    'images/gallery/3.jpg',
    'images/gallery/4.jpg',
    'images/gallery/5.jpg',
    'images/gallery/6.jpg',
    'images/gallery/7.jpg',
    'images/gallery/8.jpg',
    'images/gallery/9.jpg'];

  constructor(
    private service: VehicleService,
    private activeRoute: ActivatedRoute,
  ) {
  }

  ngOnInit(): void {
    this.urlSubscription = this.activeRoute.url.subscribe(segments => {
      this.loadData(segments);
    });

  }

  private loadData(url: UrlSegment[]) {
    this.id = url.map(x => x.path).join('/');

    this.service.getVehicle(this.id).subscribe(res => {
      this.datasource = res;
    });

    this.pictures = this.images;

  }

  private valueChanged(e: any) {
    this.slideshowDelay = e.value ? 2000 : 0;
  }
}













