import { Component } from '@angular/core';


@Component({
  selector: 'app-privacy',
  templateUrl: './privacy.component.html',
  styleUrls: ['./privacy.component.css']
})
export class PrivacyComponent {

  value: number = 360;
  percet: number = 100;

  increaseValueLoop() {
    for (let i = 0; i < 10; i++) {
      setTimeout(() => {
        this.percet--;
        this.value = 3.6 * this.percet
      }, i * 1000);
    }
  }

}

