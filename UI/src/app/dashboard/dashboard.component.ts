import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'auction-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  sidebarOpened: boolean = true;

  constructor() {
  }

  ngOnInit(): void {
  }
}