import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DxDataGridComponent } from 'devextreme-angular';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  @Input() gridContainer: DxDataGridComponent | undefined;
  @Input() searchText: string | undefined;
  @Input() searchId: string | undefined;
  @Output() readonly searchTextChange: EventEmitter<string> = new EventEmitter<string>();
  destroyed$ = new Subject<boolean>();

  constructor() {
  }

  ngOnInit(): void { }

  onSearchText(searchText: string): void {
    alert(searchText);
    if (this.gridContainer) {
      this.gridContainer.instance.searchByText(searchText);
    } else if (this.searchTextChange) {
      this.searchTextChange.emit(searchText);
    }
  }


}
