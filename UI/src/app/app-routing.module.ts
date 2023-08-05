import { RouterModule, Routes } from "@angular/router";
import { NotFoundComponent } from "./not-found/not-found.component";
import { NgModule } from "@angular/core";


const routes: Routes = [
  {
    path: '',
    loadChildren: () => import("./dashboard/dashboard.module").then(m => m.DashboardModule),
  },
  {
    path: '**', component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
