import { HttpContext } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { TranslocoModule } from "@ngneat/transloco";
import { DxDrawerModule } from "devextreme-angular/ui/drawer";
import { NotFoundComponent } from '../not-found/not-found.component';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from "./dashboard-routing.module";
import { AuctionCommonModule } from '../common/auction-common.module';
import { DashboardComponentsModule } from './components/dashboard-components.module';
import { DxToolbarModule } from "devextreme-angular/ui/toolbar";
import { DxListModule } from "devextreme-angular/ui/list";
import { DxAccordionModule, DxBarGaugeModule, DxBulletModule, DxButtonGroupModule, DxButtonModule, DxCalendarModule, DxCheckBoxModule, DxCircularGaugeModule, DxColorBoxModule, DxDataGridModule, DxDateBoxModule, DxFileUploaderModule, DxFormModule, DxGalleryModule, DxLoadIndicatorModule, DxNumberBoxModule, DxSelectBoxModule, DxTextBoxModule, DxValidationGroupModule, DxValidationSummaryModule, DxValidatorModule } from 'devextreme-angular';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { VehicleComponent } from './pages/vehicle/vehicle.component';
import { AuctionComponent } from './pages/auction/auction.component';
import { LocationComponent } from './pages/location/location.component';
import { SevicesComponent } from './pages/sevices/sevices.component';
import { SupportComponent } from './pages/support/support.component';
import { LotComponent } from './pages/lot/lot.component';
import { VehicleEditComponent } from './pages/vehicle-edit/vehicle-edit.component';
import { AuthGuard } from '../common/components/auth.guard';
import { ProfileComponent } from './pages/profile/profile.component';
import { ProfileEditComponent } from './pages/profile-edit/profile-edit.component';
import { RestartPasswordComponent } from './pages/restart-password/restart-password.component';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { ScrollComponent } from './pages/scroll/scroll.component';
import { CalendarComponent } from './pages/calendar/calendar.component';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { WatchListComponent } from './pages/watch-list/watch-list.component';
import { VehiclePictureComponent } from './pages/vehicle-picture/vehicle-picture.component';
import { ProfilePictureComponent } from './pages/profile-picture/profile-picture.component';
import { VehicleWonComponent } from './pages/vehicle-won/vehicle-won.component';
import { VehicleLostComponent } from './pages/vehicle-lost/vehicle-lost.component';
import { VehicleBidsComponent } from './pages/vehicle-bids/vehicle-bids.component';
import { AddEventComponent } from './pages/add-event/add-event.component';
import { CalendarManageComponent } from './pages/calendar-manage/calendar-manage.component';
import { EditEventComponent } from './pages/edit-event/edit-event.component';
import { HomeComponent } from './pages/home/home.component';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FullCalendarModule } from '@fullcalendar/angular';
import { PaymentComponent } from './pages/payment/payment.component';
import { AdminPanelComponent } from './pages/admin-panel/admin-panel.component';
import { VehicleAddComponent } from './pages/vehicle-add/vehicle-add.component';
import { AuctionListComponent } from './pages/auction-list/auction-list.component';




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
    SupportComponent,
    LotComponent,
    VehicleEditComponent,
    ProfileComponent,
    ProfileEditComponent,
    RestartPasswordComponent,
    ScrollComponent,
    CalendarComponent,
    WatchListComponent,
    VehiclePictureComponent,
    ProfilePictureComponent,
    VehicleWonComponent,
    VehicleLostComponent,
    VehicleBidsComponent,
    AddEventComponent,
    CalendarManageComponent,
    EditEventComponent,
    PaymentComponent,
    AdminPanelComponent,
    VehicleAddComponent,
    AuctionListComponent
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
    DxValidationGroupModule,
    FormsModule,
    DxLoadIndicatorModule,
    DxCheckBoxModule,
    DxGalleryModule,
    DxBarGaugeModule,
    DxFileUploaderModule,
    DxBulletModule,
    DxCircularGaugeModule,
    DxCalendarModule,
    NgbModalModule,
    DxColorBoxModule,
    FullCalendarModule,
    ScrollingModule,
    ReactiveFormsModule
  ],
  providers: [
    HttpContext,
    AuthGuard
  ],
})
export class DashboardModule {
}