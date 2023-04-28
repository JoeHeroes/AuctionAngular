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
import { DxAccordionModule, DxBarGaugeModule, DxBulletModule, DxButtonGroupModule, DxButtonModule, DxCalendarModule, DxCheckBoxModule, DxCircularGaugeModule, DxDataGridModule, DxDateBoxModule, DxFileUploaderModule, DxFormModule, DxGalleryModule, DxLoadIndicatorModule, DxNumberBoxModule, DxSelectBoxModule, DxTextBoxModule, DxValidationSummaryModule, DxValidatorModule } from 'devextreme-angular';
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
import { VehicleEditComponent } from './pages/vehicle-edit/vehicle-edit.component';
import { AuthGuard } from '../common/components/auth.guard';
import { ProfileComponent } from './pages/profile/profile.component';
import { ProfileEditComponent } from './pages/profile-edit/profile-edit.component';
import { RestartPasswordComponent } from './pages/restart-password/restart-password.component';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { ScrollComponent } from './pages/scroll/scroll.component';
import { CalendarComponent } from './pages/calendar/calendar.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';



import { FlatpickrModule } from 'angularx-flatpickr';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { WatchListComponent } from './pages/watch-list/watch-list.component';




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
    VehicleEditComponent,
    ProfileComponent,
    ProfileEditComponent,
    RestartPasswordComponent,
    ScrollComponent,
    CalendarComponent,
    WatchListComponent,
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
    DxFileUploaderModule,
    DxBulletModule,
    ScrollingModule,
    DxCircularGaugeModule,
    DxCalendarModule,
    NgbModalModule,
    FlatpickrModule.forRoot(),
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory
    })
  ],
  providers: [
    HttpContext,
    AuthGuard
  ],
})
export class DashboardModule {
}