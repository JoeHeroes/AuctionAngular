import { HomeComponent } from "src/app/dashboard/pages/home/home.component";
import { NotFoundComponent } from "src/app/not-found/not-found.component";
import { DashboardComponent } from "src/app/dashboard/dashboard.component";
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { SupportComponent } from './pages/support/support.component';
import { SevicesComponent } from './pages/sevices/sevices.component';
import { LocationComponent } from './pages/location/location.component';
import { AuctionComponent } from './pages/auction/auction.component';
import { VehicleComponent } from './pages/vehicle/vehicle.component';
import { LotComponent } from './pages/lot/lot.component';
import { VehicleEditComponent } from './pages/vehicle-edit/vehicle-edit.component';
import { AuthGuard } from '../common/components/auth.guard';
import { ProfileComponent } from './pages/profile/profile.component';
import { ProfileEditComponent } from './pages/profile-edit/profile-edit.component';
import { RestartPasswordComponent } from './pages/restart-password/restart-password.component';
import { CalendarComponent } from './pages/calendar/calendar.component';
import { WatchListComponent } from './pages/watch-list/watch-list.component';
import { VehiclePictureComponent } from './pages/vehicle-picture/vehicle-picture.component';
import { ProfilePictureComponent } from './pages/profile-picture/profile-picture.component';
import { VehicleWonComponent } from './pages/vehicle-won/vehicle-won.component';
import { VehicleLostComponent } from './pages/vehicle-lost/vehicle-lost.component';
import { VehicleBidsComponent } from './pages/vehicle-bids/vehicle-bids.component';
import { AddEventComponent } from './pages/add-event/add-event.component';
import { CalendarManageComponent } from './pages/calendar-manage/calendar-manage.component';
import { EditEventComponent } from './pages/edit-event/edit-event.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import { ContactusComponent } from "./pages/contactus/contactus.component";
import { FaqComponent } from "./pages/faq/faq.component";
import { HowtobuyComponent } from "./pages/howtobuy/howtobuy.component";
import { PaymentComponent } from "./pages/payment/payment.component";
import { VehicleAddComponent } from "./pages/vehicle-add/vehicle-add.component";
import { AuctionListComponent } from "./pages/auction-list/auction-list.component";
import { AdminPanelVehiclesComponent } from "./pages/admin-panel-vehicles/admin-panel-vehicles.component";
import { AdminPanelAuctionsComponent } from "./pages/admin-panel-auctions/admin-panel-auctions.component";
import { AuctionAddComponent } from "./pages/auction-add/auction-add.component";
import { AuctionEditComponent } from "./pages/auction-edit/auction-edit.component";

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: HomeComponent,
      },
      {
        path: 'home',
        pathMatch: 'full',
        component: HomeComponent,
        data: { title: 'Home' }
      },
      {
        path: 'vehicle',
        pathMatch: 'full',
        component: VehicleComponent,
        data: { title: 'Vehicle' }
      },
      {
        path: 'vehicle',
        children: [
          { path: 'lot', children: [
            {
              path: ':id',
              pathMatch: 'full',
              component: LotComponent,
              data: { title: 'Lot' }
            },
          ], },
          { path: 'edit', children: [{ path: ':id', pathMatch: 'full', component: VehicleEditComponent, data: { title: 'Vehicle Edit' }},],},
          { path: 'add', pathMatch: 'full', component: VehicleAddComponent, data: { title: 'Vehicle Add' } },
          { path: 'picture', pathMatch: 'full', component: VehiclePictureComponent, data: { title: 'Vehicle Picture' } },
          { path: 'won', pathMatch: 'full', component: VehicleWonComponent, data: { title: 'Vehicle Won' }, canActivate: [AuthGuard]},
          { path: 'lost', pathMatch: 'full', component: VehicleLostComponent, data: { title: 'Vehicle Lost' }, canActivate: [AuthGuard] },
          { path: 'bids', pathMatch: 'full', component: VehicleBidsComponent, data: { title: 'Vehicle Bids' }, canActivate: [AuthGuard] },
          { path: 'watchlist', pathMatch: 'full', component: WatchListComponent, data: { title: 'Watch List' }, canActivate: [AuthGuard] },
          { path: 'panel', pathMatch: 'full', component: AdminPanelVehiclesComponent, data: { title: 'Vehicles Panel' } },
        ],
      },
      {
        path: 'auction',
        pathMatch: 'full',
        component: AuctionComponent,
        data: { title: 'Auction' }
      },
      {
        path: 'auction',
        children: [
          { path: 'lot', children: [{ path: ':id', pathMatch: 'full', component: LotComponent, data: { title: 'Lot' }},],},
          { path: 'edit', children: [{ path: ':id', pathMatch: 'full', component: AuctionEditComponent, data: { title: 'Auction Edit' }},],},
          { path: 'list', pathMatch: 'full', component: AuctionListComponent, data: { title: 'Auction List' }},
          { path: 'add', pathMatch: 'full', component: AuctionAddComponent, data: { title: 'Auction Add' }},
          { path: 'panel', pathMatch: 'full', component: AdminPanelAuctionsComponent, data: { title: 'Auctions Panel' }},
        ],
      },
      {
        path: 'support',
        children: [
          { path: 'ContactUs', pathMatch: 'full', component: ContactusComponent, data: { title: 'Contact Us' } },
          { path: 'FAQ', pathMatch: 'full', component: FaqComponent, data: { title: 'FAQ' } },
          { path: 'HowToBuy', pathMatch: 'full', component: HowtobuyComponent, data: { title: 'How To Buy' } },
        ],
      },
      {
        path: 'calendar',
        pathMatch: 'full',
        component: CalendarComponent,
        data: { title: 'Calendar' }
      },
      {
        path: 'calendar',
        children: [
          { path: 'manage', pathMatch: 'full', component: CalendarManageComponent, data: { title: 'Calendar Manage' } },
        ],
      },
      {
        path: 'event',
        children: [
          { path: 'edit', children: [{ path: ':id', pathMatch: 'full', component: EditEventComponent, data: { title: 'Event Edit' }},],},
          { path: 'add', pathMatch: 'full', component: AddEventComponent, data: { title: 'Event Add' } },
        ],
      },
      {
        path: 'profile',
        pathMatch: 'full',
        component: ProfileComponent,
        data: { title: 'Profile' }
      },
      {
        path: 'profile',
        children: [
          { path: 'edit', pathMatch: 'full', component: ProfileEditComponent, data: { title: 'Profile Edit' } },
          { path: 'picture', pathMatch: 'full', component: ProfilePictureComponent, data: { title: 'Profile Picture' } },
        ],
      },
      {
        path: 'account',
        children: [
          { path: 'login', pathMatch: 'full', component: LoginComponent, data: { title: 'Login' } },
          { path: 'register', pathMatch: 'full', component: RegisterComponent, data: { title: 'Register' } },
          { path: 'restart', pathMatch: 'full', component: RestartPasswordComponent, data: { title: 'Restart Password' } },

        ],
      },
      {
        path: 'payment',
        pathMatch: 'full',
        component: PaymentComponent,
        data: { title: 'Payment' }
      },
      {
        path: 'location',
        pathMatch: 'full',
        component: LocationComponent,
        data: { title: 'Location' },
        canActivate: [AuthGuard]
      },
      {
        path: 'services',
        pathMatch: 'full',
        component: SevicesComponent,
        data: { title: 'Sevices' }
      },
      {
        path: 'support',
        pathMatch: 'full',
        component: SupportComponent,
        data: { title: 'Support' }
      },
      {
        path: '**',
        pathMatch: 'full',
        component: NotFoundComponent,
        data: { title: 'Not Found' }
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule {
}