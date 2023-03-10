import { Component, OnInit, OnDestroy } from '@angular/core';
import { TessSystemApiClient } from 'src/app/common/services/tess-api.generated.service';
import { marker as _ } from '@ngneat/transloco-keys-manager/marker';

export interface cardItem {
  text: string;
  href: string;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {


  constructor(private system: TessSystemApiClient) {
  }

  ngOnDestroy(): void {
  }

  ngOnInit(): void {
  }

}