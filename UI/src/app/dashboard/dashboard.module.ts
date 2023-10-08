import { HttpContext } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { TranslocoModule } from "@ngneat/transloco";
import { DxDrawerModule } from "devextreme-angular/ui/drawer";
import { NotFoundComponent } from '../not-found/not-found.component';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from "./dashboard-routing.module";
import { AuctionCommonModule } from '../common/auction-common.module';
import { RegisterComponent } from '../pages/register/register.component';
import { HomeComponent } from '../pages/home/home.component';
import { LoginComponent } from '../pages/login/login.component';
import { VehicleComponent } from '../pages/vehicle/vehicle.component';
import { AuctionComponent } from '../pages/auction/auction.component';
import { LocationComponent } from '../pages/location/location.component';
import { SevicesComponent } from '../pages/sevices/sevices.component';
import { SupportComponent } from '../pages/support/support.component';
import { LotComponent } from '../pages/lot/lot.component';
import { VehicleEditComponent } from '../pages/vehicle-edit/vehicle-edit.component';
import { ProfileComponent } from '../pages/profile/profile.component';
import { ProfileEditComponent } from '../pages/profile-edit/profile-edit.component';
import { RestartPasswordComponent } from '../pages/restart-password/restart-password.component';
import { CalendarComponent } from '../pages/calendar/calendar.component';
import { WatchListComponent } from '../pages/watch-list/watch-list.component';
import { VehiclePictureComponent } from '../pages/vehicle-picture/vehicle-picture.component';
import { ProfilePictureComponent } from '../pages/profile-picture/profile-picture.component';
import { VehicleWonComponent } from '../pages/vehicle-won/vehicle-won.component';
import { VehicleLostComponent } from '../pages/vehicle-lost/vehicle-lost.component';
import { VehicleBidsComponent } from '../pages/vehicle-bids/vehicle-bids.component';
import { EventAddComponent } from '../pages/event-add/event-add.component';
import { CalendarManageComponent } from '../pages/calendar-manage/calendar-manage.component';
import { EventEditComponent } from '../pages/event-edit/event-edit.component';
import { PaymentComponent } from '../pages/payment/payment.component';
import { VehicleAddComponent } from '../pages/vehicle-add/vehicle-add.component';
import { AuctionListComponent } from '../pages/auction-list/auction-list.component';
import { AdminPanelVehiclesComponent } from '../pages/admin-panel-vehicles/admin-panel-vehicles.component';
import { AdminPanelAuctionsComponent } from '../pages/admin-panel-auctions/admin-panel-auctions.component';
import { AuctionAddComponent } from '../pages/auction-add/auction-add.component';
import { AuctionEditComponent } from '../pages/auction-edit/auction-edit.component';
import { VerificationVehiclesComponent } from '../pages/verification-vehicles/verification-vehicles.component';
import { VehicleSetComponent } from '../pages/vehicle-set/vehicle-set.component';
import { DashboardComponentsModule } from '../common/components/dashboard-components.module';
import { DxAccordionModule, DxBarGaugeModule, DxBulletModule, DxButtonGroupModule, DxButtonModule, DxCalendarModule, DxCheckBoxModule, DxCircularGaugeModule, DxColorBoxModule, DxDataGridModule, DxDateBoxModule, DxFileUploaderModule, DxFormModule, DxGalleryModule, DxListModule, DxLoadIndicatorModule, DxNumberBoxModule, DxSelectBoxModule, DxTextAreaModule, DxTextBoxModule, DxToolbarModule, DxValidationGroupModule, DxValidationSummaryModule, DxValidatorModule } from 'devextreme-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { FullCalendarModule } from '@fullcalendar/angular';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthGuard } from '../common/components/auth.guard';
import { ContactusComponent } from '../pages/contactus/contactus.component';




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
    CalendarComponent,
    WatchListComponent,
    VehiclePictureComponent,
    ProfilePictureComponent,
    VehicleWonComponent,
    VehicleLostComponent,
    VehicleBidsComponent,
    EventAddComponent,
    CalendarManageComponent,
    EventEditComponent,
    PaymentComponent,
    VehicleAddComponent,
    AuctionListComponent,
    AdminPanelVehiclesComponent,
    AdminPanelAuctionsComponent,
    AuctionAddComponent,
    AuctionEditComponent,
    EventEditComponent,
    EventAddComponent,
    VerificationVehiclesComponent,
    VehicleSetComponent,
    ContactusComponent
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
    DxTextAreaModule,
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