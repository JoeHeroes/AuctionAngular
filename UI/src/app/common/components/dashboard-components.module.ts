import { CommonModule } from '@angular/common';
import { TranslocoModule } from "@ngneat/transloco";
import { DxListModule } from "devextreme-angular/ui/list";
import { DxToolbarModule } from "devextreme-angular/ui/toolbar";
import { DxLoadPanelModule } from "devextreme-angular/ui/load-panel";
import { DxSelectBoxModule } from "devextreme-angular/ui/select-box";
import { DxPopupModule } from "devextreme-angular/ui/popup";
import { DxFormModule } from "devextreme-angular/ui/form";
import { DxButtonModule } from "devextreme-angular/ui/button";
import { DxLoadIndicatorModule } from "devextreme-angular/ui/load-indicator";
import { DxDropDownButtonModule } from "devextreme-angular/ui/drop-down-button";
import { MainComponent } from './main/main.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { AuctionCommonModule } from 'src/app/common/auction-common.module';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DxTreeViewModule } from 'devextreme-angular';

@NgModule({
  declarations: [
    MainComponent,
    NavBarComponent
  ],
  imports: [
    CommonModule,
    TranslocoModule,
    RouterModule,
    DxListModule,
    AuctionCommonModule,
    DxToolbarModule,
    DxLoadPanelModule,
    FormsModule,
    DxSelectBoxModule,
    DxPopupModule,
    DxFormModule,
    DxButtonModule,
    DxLoadIndicatorModule,
    DxDropDownButtonModule,
    DxTreeViewModule,
  ],
  exports: [
    MainComponent,
    NavBarComponent,
  ]
})
export class DashboardComponentsModule {
}