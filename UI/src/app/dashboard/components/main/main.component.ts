import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { LangDefinition, TranslocoService } from '@ngneat/transloco';
import { AuctionService } from 'src/app/common/services/auction.service';
import { AuthenticationService } from 'src/app/common/services/authentication.service';

@Component({
  selector: 'auction-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss', './main.component.dark.scss']
})
export class MainComponent implements OnInit {
  @Output() sidebarButtonClick = new EventEmitter<void>();
  public isUserAuthenticated!: boolean;
  datasource: any;
  liveAuction: boolean = false;


  get availableLangs(): LangDefinition[] {
    return this.transloco.getAvailableLangs() as LangDefinition[];
  }

  get selectedLang(): string {
    return this.transloco.getActiveLang();
  }

  constructor(private authService: AuthenticationService,
    private auctionService: AuctionService,
    private router: Router,
    private transloco: TranslocoService) {
  }



  ngOnInit(): void {

    if (this.authService.isLoggedIn()) {
      this.isUserAuthenticated = true;
    } else {
      this.router.navigateByUrl("login");
      this.isUserAuthenticated = false;
    }

    this.auctionService.liveAuction().subscribe(res => {
      this.liveAuction = res;
    });

    this.authService.loggedUserId().subscribe(res => {
      this.authService.getUserInfo(res.userId).subscribe(res => {
        this.datasource = res;
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
    this.authService.logout();
    this.router.navigate(["/"]);
  }

  public selectLang(lang: string) {
    localStorage.setItem('auction:lang', lang);
    this.transloco.setActiveLang(lang);
  }
}