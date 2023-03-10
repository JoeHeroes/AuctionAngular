import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { DxDataGridComponent } from 'devextreme-angular';
import { debounceTime, distinctUntilChanged, fromEvent, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() gridContainer: DxDataGridComponent | undefined;
  @Input() searchText: string | undefined;
  @Input() searchId: string | undefined;
  @Output() readonly searchTextChange: EventEmitter<string> = new EventEmitter<string>();
  destroyed$ = new Subject<boolean>();

  // eslint-disable-next-line no-useless-constructor
  constructor() {
  }

  ngOnInit(): void { }



  onSearchText(searchText: string): void {
    if (this.gridContainer) {
      this.gridContainer.instance.searchByText(searchText);
    } else if (this.searchTextChange) {
      this.searchTextChange.emit(searchText);
    }
  }


}
