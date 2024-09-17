import { Component, OnInit, Self, inject, output, input } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormControl,
  ReactiveFormsModule,
  ValidatorFn,
  NgControl,
  Validators,
} from '@angular/forms';
import { NgIf } from '@angular/common';
import { Router } from '@angular/router';
import {
  BsDatepickerConfig,
  BsDatepickerModule,
} from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-pet-create',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, BsDatepickerModule],
  templateUrl: './pet-create.component.html',
  styleUrl: './pet-create.component.css',
})
export class PetCreateComponent implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  cancelRegister = output<boolean>();
  createForm: FormGroup = new FormGroup({});
  bsValue: Date;
  maxDate: Date;
  validationErrors: string[] | undefined;

  constructor() {
    this.maxDate = new Date();
    this.bsValue = new Date();
    this.maxDate.setDate(this.maxDate.getDate());
    this.bsValue.setDate(this.bsValue.getDate());
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.createForm = new FormGroup({
      petName: new FormControl('', Validators.required),
      description: new FormControl(''),
      microChipId: new FormControl(''),
      missingSince: new FormControl('', Validators.required),
      ownerName: new FormControl(''),
      ownerEmail: new FormControl(''),
    });
  }

  create() {}

  cancel() {
    this.cancelRegister.emit(false);
  }
}
