import {Component, HostBinding, Input, OnInit} from '@angular/core';

type MessageType = "info" | "success" | "warning" | "danger";

@Component({
  selector: 'tess-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit {
  @Input() type: MessageType = "info"
  @Input() title?: string;
  @HostBinding('class.info') get info() { return this.type == "info" }
  @HostBinding('class.success') get success() { return this.type == "success" }
  @HostBinding('class.warning') get warning() { return this.type == "warning" }
  @HostBinding('class.danger') get danger() { return this.type == "danger" }

  constructor() { }

  ngOnInit(): void {
  }
}
