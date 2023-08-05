import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import { SupportComponent } from "./support.component";
import { ContactusComponent } from "./pages/contactus/contactus.component";
import { FaqComponent } from "./pages/faq/faq.component";
import { HowtobuyComponent } from "./pages/howtobuy/howtobuy.component";

const routes: Routes = [
  {
    path: 'support',
    component: SupportComponent,
    children: [
      {
        path: 'ContactUs',
        pathMatch: 'full',
        component: ContactusComponent,
      },
      {
        path: 'FAQ',
        pathMatch: 'full',
        component: FaqComponent,
      },
      {
        path: 'HowToBuy',
        pathMatch: 'full',
        component: HowtobuyComponent,
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SupportRoutingModule {
}