import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { TranslocoService } from '@ngneat/transloco';
import { Subscription } from 'rxjs';
import { AuthResponseDto } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';
import { AddOpinionDto, OpinionService } from 'src/app/services/opinion.service';

@Component({
  selector: 'app-opinion-add',
  templateUrl: './opinion-add.component.html',
  styleUrls: ['./opinion-add.component.css']
})
export class OpinionAddComponent implements OnInit {
  urlSubscription?: Subscription;
  id: any;
  returnUrl!: string;
  showError!: boolean;
  addForm!: FormGroup;
  errorMessage: string = '';

  constructor(private notificationService: NotificationService,
    private opinionService: OpinionService,
    private router: Router,
    private route: ActivatedRoute,
    private transloco: TranslocoService) { }

  ngOnInit(): void {
    this.addForm = new FormGroup({
      description: new FormControl("", [Validators.required]),
      origin: new FormControl("", [Validators.required]),
      valuation: new FormControl("", [Validators.required]),
      condition: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/vehicle/verification';

    this.urlSubscription = this.route.url.subscribe(segments => {
      this.loadData(segments);
    });
  }
  private loadData(url: UrlSegment[]) {
    this.id = url.map(x => x.path).join('/');
  }

  addOpinion = (addFormValue: any) => {
    this.showError = false;
    const add = { ...addFormValue };

    const addData: AddOpinionDto = {
      description: add.description,
      origin: add.origin,
      valuation: add.valuation,
      condition: add.condition,
      vehicleId: this.id
    }

    if (add.description == "") {
      this.errorMessage = this.transloco.translate('message.descriptionRequired');
      this.showError = true;
    }
    else if (add.origin == "") {
      this.errorMessage = this.transloco.translate('message.originRequired');
      this.showError = true;
    }
    else if (add.valuation == "") {
      this.errorMessage = this.transloco.translate('message.valuationRequired');
      this.showError = true;
    }
    else if (add.condition == "") {
      this.errorMessage = this.transloco.translate('message.conditionRequired');
      this.showError = true;
    }
    this.opinionService.addOpinion(addData)
      .subscribe({
        next: (res: AuthResponseDto) => {
          this.notificationService.showSuccess( this.transloco.translate('notification.addOpinionCorrect'), "Success");
          this.router.navigate([this.returnUrl]);
        },
        error: (err: HttpErrorResponse) => {
          this.notificationService.showError( this.transloco.translate('notification.addOpinionFail'), "Failed");
        }
      })
   }
}