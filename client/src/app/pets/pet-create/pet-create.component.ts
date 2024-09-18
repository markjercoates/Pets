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
import { PetService } from '../../_services/pet.service';
import { PetType } from '../../_models/pettype';
@Component({
  selector: 'app-pet-create',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, BsDatepickerModule],
  templateUrl: './pet-create.component.html',
  styleUrl: './pet-create.component.css',
})
export class PetCreateComponent implements OnInit {
  private fb = inject(FormBuilder);
  petService = inject(PetService);
  private router = inject(Router);
  cancelRegister = output<boolean>();
  createForm: FormGroup = new FormGroup({});
  bsValue: Date;
  maxDate: Date;
  petTypes: PetType[] = [];
  validationErrors: string[] | undefined;

  constructor() {
    this.maxDate = new Date();
    this.bsValue = new Date();
    this.maxDate.setDate(this.maxDate.getDate());
    this.bsValue.setDate(this.bsValue.getDate());
  }

  ngOnInit(): void {
    this.initializeForm();
    this.loadPetTypes();
  }

  initializeForm() {
    this.createForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(50),
      ]),
      description: new FormControl('', Validators.maxLength(100)),
      microChipId: new FormControl('', Validators.maxLength(20)),
      missingSince: new FormControl('', Validators.required),
      ownerName: new FormControl('', Validators.maxLength(100)),
      ownerEmail: new FormControl('', Validators.maxLength(100)),
      petTypeId: new FormControl('', Validators.required),
    });
  }

  loadPetTypes() {
    this.petService.getPetTypes().subscribe({
      next: (petTypes) => {
        this.petTypes = petTypes || [];
      },
    });
  }

  create() {
    this.petService.createPet(this.createForm.value).subscribe({
      next: (_) => this.router.navigateByUrl('/pets'),
      error: (error) => (this.validationErrors = error),
    });
  }

  cancel() {
    this.router.navigateByUrl('/pets');
  }
}
