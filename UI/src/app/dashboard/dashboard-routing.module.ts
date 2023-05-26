import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from "src/app/dashboard/pages/home/home.component";
import { NotFoundComponent } from "src/app/not-found/not-found.component";
import { DashboardComponent } from "src/app/dashboard/dashboard.component";
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { PrivacyComponent } from './pages/privacy/privacy.component';
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
import { VehicleBidsComponent } from './pages/vehicle-binds/vehicle-bids.component';

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
        path: 'vehicle-won',
        pathMatch: 'full',
        component: VehicleWonComponent,
        data: { title: 'Vehicle Won' }
      },
      {
        path: 'vehicle-lost',
        pathMatch: 'full',
        component: VehicleLostComponent,
        data: { title: 'Vehicle Lost' }
      },
      {
        path: 'vehicle-bids',
        pathMatch: 'full',
        component: VehicleBidsComponent,
        data: { title: 'Vehicle Bids' }
      },
      {
        path: 'auction',
        pathMatch: 'full',
        component: AuctionComponent,
        data: { title: 'Auction' }
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
        path: 'privacy',
        pathMatch: 'full',
        component: PrivacyComponent,
        data: { title: 'Privacy' }
      },
      {
        path: 'watchlist',
        pathMatch: 'full',
        component: WatchListComponent,
        data: { title: 'Watch List' }
      },
      {
        path: 'calendar',
        pathMatch: 'full',
        component: CalendarComponent,
        data: { title: 'Calendar' }
      },
      {
        path: 'login',
        pathMatch: 'full',
        component: LoginComponent,
        data: { title: 'Login' }
      },
      {
        path: 'profile',
        pathMatch: 'full',
        component: ProfileComponent,
        data: { title: 'Profile' }
      },
      {
        path: 'profile-edit',
        pathMatch: 'full',
        component: ProfileEditComponent,
        data: { title: 'Profile Edit' }
      },
      {
        path: 'profile-picture',
        pathMatch: 'full',
        component: ProfilePictureComponent,
        data: { title: 'Profile Picture' }
      },
      {
        path: 'restart-password',
        pathMatch: 'full',
        component: RestartPasswordComponent,
        data: { title: 'Restart Password' }
      },
      {
        path: 'register',
        pathMatch: 'full',
        component: RegisterComponent,
        data: { title: 'Register' }
      },
      {
        path: 'vehicle/lot',
        children: [
          {
            path: ':id',
            pathMatch: 'full',
            component: LotComponent,
            data: { title: 'Lot' }
          },
        ],
      },
      {
        path: 'vehicle/editor',
        pathMatch: 'full',
        component: VehicleEditComponent,
        data: { title: 'Vehicle Edit' }
      },
      {
        path: 'vehicle/picture',
        pathMatch: 'full',
        component: VehiclePictureComponent,
        data: { title: 'Vehicle Picture' }
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