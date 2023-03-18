import { Component } from '@angular/core';

@Component({
  selector: 'app-privacy',
  templateUrl: './privacy.component.html',
  styleUrls: ['./privacy.component.css']
})
export class PrivacyComponent {

  dataSource: string[];


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

  slideshowDelay = 2000;

  constructor() {
    this.dataSource = this.images;
  }

  valueChanged(e: any) {
    this.slideshowDelay = e.value ? 2000 : 0;
  }
}
