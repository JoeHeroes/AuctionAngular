import { InfoService } from './../common/services/info.service';
import { HttpContext } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslocoModule } from "@ngneat/transloco";
import { DxDrawerModule } from "devextreme-angular/ui/drawer";
import { NotFoundComponent } from '../not-found/not-found.component';
import { DashboardComponent } from './dashboard.component';
import { HomeComponent } from './pages/home/home.component';
import { DashboardRoutingModule } from "./dashboard-routing.module";
import { AuctionCommonModule } from '../common/auction-common.module';
import { DashboardComponentsModule } from './components/dashboard-components.module';
import { DxToolbarModule } from "devextreme-angular/ui/toolbar";
import { DxListModule } from "devextreme-angular/ui/list";
import { DxAccordionModule, DxBarGaugeModule, DxButtonGroupModule, DxButtonModule, DxCheckBoxModule, DxDataGridModule, DxDateBoxModule, DxFormModule, DxGalleryModule, DxLoadIndicatorModule, DxNumberBoxModule, DxSelectBoxModule, DxTextBoxModule, DxValidationSummaryModule, DxValidatorModule } from 'devextreme-angular';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { VehicleComponent } from './pages/vehicle/vehicle.component';
import { AuctionComponent } from './pages/auction/auction.component';
import { LocationComponent } from './pages/location/location.component';
import { SevicesComponent } from './pages/sevices/sevices.component';
import { PrivacyComponent } from './pages/privacy/privacy.component';
import { SupportComponent } from './pages/support/support.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LotComponent } from './pages/lot/lot.component';

@NgModule({
  declarations: [
    HomeComponent,
    NotFoundComponent,
    DashboardComponent,
    RegisterComponent,
    LoginComponent,
    VehicleComponent,
    AuctionComponent,
    LocationComponent,
    SevicesComponent,
    PrivacyComponent,
    SupportComponent,
    LotComponent,
  ],
  exports: [],
  imports: [
    DashboardRoutingModule,
    CommonModule,
    TranslocoModule,
    AuctionCommonModule,
    DashboardComponentsModule,
    DxDrawerModule,
    DxToolbarModule,
    DxListModule,
    DxFormModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxButtonModule,
    DxDataGridModule,
    DxButtonGroupModule,
    DxAccordionModule,
    DxSelectBoxModule,
    DxNumberBoxModule,
    DxDateBoxModule,
    DxValidationSummaryModule,
    FormsModule,
    ReactiveFormsModule,
    DxLoadIndicatorModule,
    DxCheckBoxModule,
    DxGalleryModule,
    DxBarGaugeModule,
  ],
  providers: [
    HttpContext,
    InfoService
  ],
})
export class DashboardModule {
}