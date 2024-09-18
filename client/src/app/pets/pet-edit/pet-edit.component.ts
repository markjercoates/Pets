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
import { ActivatedRoute, Router } from '@angular/router';
import {
  BsDatepickerConfig,
  BsDatepickerModule,
} from 'ngx-bootstrap/datepicker';
import { PetService } from '../../_services/pet.service';
import { PetType } from '../../_models/pettype';
import { Pet } from '../../_models/pet';
import { formatDate } from '@angular/common';
import { PaginatedResult } from '../../_models/pagination';
@Component({
  selector: 'app-pet-edit',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, BsDatepickerModule],
  templateUrl: './pet-edit.component.html',
  styleUrl: './pet-edit.component.css',
})
export class PetEditComponent implements OnInit {
  private activatedRoute = inject(ActivatedRoute);
  private fb = inject(FormBuilder);
  petService = inject(PetService);
  private router = inject(Router);
  cancelRegister = output<boolean>();
  editForm: FormGroup = new FormGroup({});
  bsValue = new Date();
  maxDate = new Date();
  petTypes: PetType[] = [];
  pet?: Pet;
  validationErrors: string[] | undefined;

  constructor() {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadPet();
    this.loadPetTypes();

    this.maxDate.setDate(this.maxDate.getDate());
    this.bsValue.setDate(this.bsValue.getDate());
  }

  initializeForm() {
    this.editForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(50),
      ]),
      description: new FormControl('', Validators.maxLength(100)),
      microChipId: new FormControl('', Validators.maxLength(20)),
      missingSince: new FormControl(null, Validators.required),
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

  loadPet() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;

    this.petService.getPet(+id).subscribe({
      next: (pet) => {
        this.pet = pet;
        this.editForm.patchValue(pet);
        this.editForm.patchValue({
          missingSince: this.formatMissingDate(pet.missingSince),
        });
      },
    });
  }

  private formatMissingDate(date: Date) {
    const d = new Date(date);
    let day = d.getDate();
    let month = d.getMonth();
    let year = d.getFullYear();

    return new Date(year, month, day);
  }

  update() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;

    this.petService.updatePet(+id, this.editForm.value).subscribe({
      next: (_) => {
        this.petService.paginatedResult.set(null);
        this.router.navigateByUrl('/pets');
      },
    });
  }

  delete() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;

    this.petService.deletePet(+id).subscribe({
      next: (_) => {
        this.petService.paginatedResult.set(null);
        this.router.navigateByUrl('/pets');
      },
    });
  }

  cancel() {
    this.router.navigateByUrl('/pets');
  }
}
