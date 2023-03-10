import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslocoModule } from "@ngneat/transloco";
import { RouterModule } from "@angular/router";
import { DxListModule } from "devextreme-angular/ui/list";
import { DxToolbarModule } from "devextreme-angular/ui/toolbar";
import { DxLoadPanelModule } from "devextreme-angular/ui/load-panel";
import { FormsModule } from "@angular/forms";
import { DxSelectBoxModule } from "devextreme-angular/ui/select-box";
import { DxPopupModule } from "devextreme-angular/ui/popup";
import { DxFormModule } from "devextreme-angular/ui/form";
import { DxButtonModule } from "devextreme-angular/ui/button";
import { DxLoadIndicatorModule } from "devextreme-angular/ui/load-indicator";
import { DxDropDownButtonModule } from "devextreme-angular/ui/drop-down-button";
import { MainComponent } from './main/main.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { TessCommonModule } from 'src/app/common/tess-common.module';
import { SearchComponent } from './search/search.component';

@NgModule({
  declarations: [
    MainComponent,
    NavBarComponent,
    SearchComponent,
  ],
  imports: [
    CommonModule,
    TranslocoModule,
    RouterModule,
    DxListModule,
    TessCommonModule,
    DxToolbarModule,
    DxLoadPanelModule,
    FormsModule,
    DxSelectBoxModule,
    DxPopupModule,
    DxFormModule,
    DxButtonModule,
    DxLoadIndicatorModule,
    DxDropDownButtonModule
  ],
  exports: [
    MainComponent,
    NavBarComponent,
  ]
})
export class DashboardComponentsModule {
}