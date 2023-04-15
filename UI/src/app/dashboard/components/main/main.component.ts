import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { LangDefinition, TranslocoService } from '@ngneat/transloco';
import { AuthenticationService } from 'src/app/common/services/authentication.service';

@Component({
  selector: 'auction-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss', './main.component.dark.scss']
})
export class MainComponent implements OnInit {
  @Output() sidebarButtonClick = new EventEmitter<void>();
  public isUserAuthenticated!: boolean;

  get availableLangs(): LangDefinition[] {
    return this.transloco.getAvailableLangs() as LangDefinition[];
  }

  get selectedLang(): string {
    return this.transloco.getActiveLang();
  }

  constructor(private authService: AuthenticationService,
    private router: Router,
    private transloco: TranslocoService) {
  }

  ngOnInit(): void {

    this.authService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      })

    if (this.authService.isLoggedIn()) {
      this.isUserAuthenticated = true;
    } else {
      this.router.navigateByUrl("login");
      this.isUserAuthenticated = false;
    }
  }

  public emitSidebarButtonClick() {
    this.sidebarButtonClick.emit();
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