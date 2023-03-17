import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogoComponent } from "src/app/common/components/logo/logo.component";
import { MessageComponent } from "src/app/common/components/message/message.component";

@NgModule({
  declarations: [
    LogoComponent,
    MessageComponent,
  ],
  imports: [
    CommonModule,
  ],
  exports: [
    LogoComponent,
    MessageComponent,
  ]
})
export class AuctionCommonModule {
}
