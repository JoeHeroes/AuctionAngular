import {Component, HostBinding, Input, OnInit} from '@angular/core';

@Component({
  selector: 'tess-logo',
  templateUrl: './logo.component.html',
  styleUrls: ['./logo.component.scss']
})
export class LogoComponent implements OnInit {
  @Input() noText: boolean = false;
  @Input() @HostBinding('class.force-light') forceLight: boolean = false;
  @Input() @HostBinding('class.force-dark') forceDark: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

}
