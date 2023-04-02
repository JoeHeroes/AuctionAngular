import { NgModule } from '@angular/core';
import { Route, RouterModule, Routes } from '@angular/router';
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
      },
      {
        path: 'vehicle',
        pathMatch: 'full',
        component: VehicleComponent,
      },
      {
        path: 'auction',
        pathMatch: 'full',
        component: AuctionComponent,
      },
      {
        path: 'location',
        pathMatch: 'full',
        component: LocationComponent,
      },
      {
        path: 'services',
        pathMatch: 'full',
        component: SevicesComponent,
      },
      {
        path: 'support',
        pathMatch: 'full',
        component: SupportComponent,
      },
      {
        path: 'privacy',
        pathMatch: 'full',
        component: PrivacyComponent,
      },
      {
        path: 'login',
        pathMatch: 'full',
        component: LoginComponent,
      },
      {
        path: 'register',
        pathMatch: 'full',
        component: RegisterComponent,
      },
      {
        path: 'vehicle/lot',
        children: [
          {
            path: ':id',
            pathMatch: 'full',
            component: LotComponent,
          },
        ],
      },
      {
        path: 'vehicle/editor',
        pathMatch: 'full',
        component: VehicleEditComponent,
      },
      {
        path: '**',
        pathMatch: 'full',
        component: NotFoundComponent,
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