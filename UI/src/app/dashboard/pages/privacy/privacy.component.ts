import { Component } from '@angular/core';
import { Company, InfoService } from 'src/app/common/services/info.service';

@Component({
  selector: 'app-privacy',
  templateUrl: './privacy.component.html',
  styleUrls: ['./privacy.component.css']
})
export class PrivacyComponent {
  companies: Company[];

  constructor(service: InfoService) {
    this.companies = service.getCompanies();
  }

}
