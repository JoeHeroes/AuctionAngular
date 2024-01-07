import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { Subscription } from 'rxjs';
import { OpinionService } from 'src/app/services/opinion.service';

@Component({
  selector: 'app-opinion',
  templateUrl: './opinion.component.html',
  styleUrls: ['./opinion.component.css']
})
export class OpinionComponent implements OnInit {
  urlSubscription?: Subscription;
  id: any;
  datasource: any = {
    description: "",
    origin: "",
    valuation: "",
    condition: 0,
    descriptionConditionInside: "",
    descriptionConditionBodywork: "",
    complianceWithVIN: false,
  };
  returnUrl!: string;
  showError!: boolean;
  errorMessage: string = '';

  constructor(private opinionService: OpinionService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.urlSubscription = this.route.url.subscribe(segments => {
      this.loadData(segments);
    });

    this.opinionService.getOpinion(this.id).subscribe(res => {
      this.datasource = res;
    });
  }

  private loadData(url: UrlSegment[]) {
    this.id = url.map(x => x.path).join('/');
  }

  getRepeatCountYellow(): number[] {
    return new Array(this.datasource.condition);
  }

  getRepeatCountGray(): number[] {
    return new Array(5-this.datasource.condition);
  }
}