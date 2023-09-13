import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LangDefinition, TranslocoService } from '@ngneat/transloco';
import { AuctionService } from 'src/app/common/services/auction.service';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { NotificationService } from 'src/app/common/services/notification.service';
import { TokenService } from 'src/app/common/services/token.service';

@Component({
  selector: 'auction-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  @Output() sidebarButtonClick = new EventEmitter<void>();

  returnUrl!: string;
  isUserAuthenticated: boolean = false;
  liveAuction: boolean = false;

  constructor(private authenticationService: AuthenticationService,
    private auctionService: AuctionService,
    private tokenService: TokenService,
    private notificationService: NotificationService,
    private router: Router,
    private route: ActivatedRoute,
    private transloco: TranslocoService) {
  }

  ngOnInit(): void {

    this.router.events.subscribe((value: any) => {
      if (value.url) {
        this.tokenService.authChanged
          .subscribe(res => {
            this.isUserAuthenticated = res;
          });
      }
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/home';

    this.auctionService.liveAuction().subscribe(res => {
      this.liveAuction = res;
    });

    this.authenticationService.loggedUserId().subscribe(res => {
      this.isUserAuthenticated = true;
    });
  }

  emitSidebarButtonClick = () => {
    this.sidebarButtonClick.emit();
  }

  openAuction = () => {
    this.router.navigate(["/auction"]); 
  }

  public logout() {
    this.isUserAuthenticated = false;
    this.notificationService.showSuccess(this.transloco.translate('notification.loggedOut'), "Success");
    this.tokenService.clear();
    this.router.navigate([this.returnUrl]);
  }

  public selectLang(lang: string) {
    sessionStorage.setItem('auction:lang', lang);
    this.transloco.setActiveLang(lang);
  }

  get availableLangs(): LangDefinition[] {
    return this.transloco.getAvailableLangs() as LangDefinition[];
  }

  get selectedLang(): string {
    return this.transloco.getActiveLang();
  }
}