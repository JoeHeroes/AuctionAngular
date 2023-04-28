import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { LangDefinition, TranslocoService } from '@ngneat/transloco';
import { AuctionService } from 'src/app/common/services/auction.service';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { TokenService } from 'src/app/common/services/token.service';

@Component({
  selector: 'auction-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  @Output() sidebarButtonClick = new EventEmitter<void>();
  isUserAuthenticated: boolean = false;
  datasource: any;
  liveAuction: boolean = false;

  constructor(private authService: AuthenticationService,
    private auctionService: AuctionService,
    private tokenService: TokenService,
    private router: Router,
    private transloco: TranslocoService) {
  }



  ngOnInit(): void {

    this.tokenService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      })

    this.auctionService.liveAuction().subscribe(res => {
      this.liveAuction = res;
    });

    this.authService.loggedUserId().subscribe(res => {
      this.authService.getUserInfo(res.userId).subscribe(res => {
        this.datasource = res;
        this.isUserAuthenticated = res;
      });
    });
  }

  emitSidebarButtonClick = () => {
    this.sidebarButtonClick.emit();
  }

  openAuction = () => {
    this.router.navigate(["/auction"]);
  }

  public logout() {
    this.tokenService.logout();
    this.router.navigate(["/"]);
  }

  public selectLang(lang: string) {
    localStorage.setItem('auction:lang', lang);
    this.transloco.setActiveLang(lang);
  }

  get availableLangs(): LangDefinition[] {
    return this.transloco.getAvailableLangs() as LangDefinition[];
  }

  get selectedLang(): string {
    return this.transloco.getActiveLang();
  }
}