import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-sevices',
  templateUrl: './sevices.component.html',
  styleUrls: ['./sevices.component.css']
})
export class SevicesComponent {
  title = 'Angular Form Validation Tutorial';
  angForm!: FormGroup;
  constructor(private fb: FormBuilder) {
    this.createForm();
  }
  createForm() {
    this.angForm = this.fb.group({
      name: ['', Validators.required]
    });
  }
}
