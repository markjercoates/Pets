import { Component, inject, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { PetService } from '../../_services/pet.service';
import { Pet } from '../../_models/pet';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pet-list',
  standalone: true,
  imports: [PaginationModule, DatePipe, FormsModule],
  templateUrl: './pet-list.component.html',
  styleUrl: './pet-list.component.css'
})
export class PetListComponent implements OnInit {
  petService = inject(PetService);
  pets: Pet[] = [];  
  pageNumber = 1;
  pageSize = 5;
  
  ngOnInit(): void {
    if(!this.petService.paginatedResult()) this.loadPets();
  }

  loadPets(): void {
    this.petService.getPets(this.pageNumber, this.pageSize);  
  }

  pageChanged(event:any){
    if (this.pageNumber !== event.page) {
       this.pageNumber = event.page;
       this.loadPets();
    }
  }
}
