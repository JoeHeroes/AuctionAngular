import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { LangDefinition, TranslocoService } from '@ngneat/transloco';

@Component({
  selector: 'tess-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss', './main.component.dark.scss']
})
export class MainComponent implements OnInit {
  @Output() sidebarButtonClick = new EventEmitter<void>();

  get availableLangs(): LangDefinition[] {
    return this.transloco.getAvailableLangs() as LangDefinition[];
  }

  get selectedLang(): string {
    return this.transloco.getActiveLang();
  }

  constructor(private router: Router,
    private transloco: TranslocoService) {
  }

  ngOnInit(): void {
  }

  emitSidebarButtonClick = () => {
    this.sidebarButtonClick.emit();
  }

  logout = () => {
    this.router.navigate(["/"]);
  }

  selectLang = (lang: string) => {
    localStorage.setItem('tess:lang', lang);
    this.transloco.setActiveLang(lang);
  }
}