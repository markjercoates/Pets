import { Component, inject, OnInit } from '@angular/core';
import { DatePipe, NgFor } from '@angular/common';
import { PetService } from '../../_services/pet.service';
import { Pet } from '../../_models/pet';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { PetType } from '../../_models/pettype';

@Component({
  selector: 'app-pet-list',
  standalone: true,
  imports: [PaginationModule, DatePipe, FormsModule, NgFor],
  templateUrl: './pet-list.component.html',
  styleUrl: './pet-list.component.css',
})
export class PetListComponent implements OnInit {
  petService = inject(PetService);
  pets: Pet[] = [];
  petTypes: PetType[] = [];
  pageNumber = 1;
  pageSize = 5;
  petType = 0;
  petName = '';

  ngOnInit(): void {
    if (!this.petService.paginatedResult()) this.loadPets();

    this.loadPetTypes();
  }

  loadPets(): void {
    this.petService.getPets(
      this.pageNumber,
      this.pageSize,
      this.petType,
      this.petName
    );
  }

  loadPetTypes(): void {
    this.petService.getPetTypes().subscribe({
      next: (petTypes) => {
        this.petTypes = petTypes || [];
      },
    });
  }

  resetFilters() {
    this.petType = 0;
    this.pageNumber = 1;
    this.pageSize = 5;
    this.petName = '';
    this.loadPets();
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadPets();
    }
  }
}
