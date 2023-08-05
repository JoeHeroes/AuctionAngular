import { Component } from "@angular/core";

@Component({
  selector: 'auction-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  sidebarOpened: boolean = true;
}