import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, model, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { PetType } from '../_models/pettype';
import { Pet } from '../_models/pet';
import { PaginatedResult } from '../_models/pagination';
import { setPaginatedResponse } from './paginationHelper';
import { Observable } from 'rxjs/internal/Observable';
import { CreatePet } from '../_models/createpet';

@Injectable({
  providedIn: 'root',
})
export class PetService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  paginatedResult = signal<PaginatedResult<Pet[]> | null>(null);

  getPets(
    pageNumber?: number,
    pageSize?: number,
    petTypeId?: number,
    petName?: string
  ) {
    let params = new HttpParams();

    if (pageNumber != null && pageSize != null) {
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());
    }

    params = params.append('petTypeId', petTypeId!.toString());
    if (petName) {
      params = params.append('name', petName);
    }

    return this.http
      .get<Pet[]>(this.baseUrl + 'pets', { observe: 'response', params })
      .subscribe({
        next: (response) => {
          setPaginatedResponse(response, this.paginatedResult);
        },
      });
  }

  getPet(id: number) {
    return this.http.get<Pet>(this.baseUrl + 'pets/' + id);
  }

  getPetTypes() {
    return this.http.get<PetType[]>(this.baseUrl + 'petTypes');
  }

  createPet(model: any) {
    return this.http.post<Pet>(this.baseUrl + 'pets', model);
  }

  updatePet(id: number, model: any) {
    return this.http.put(this.baseUrl + 'pets/' + id, model);
  }

  deletePet(id: number) {
    return this.http.delete(this.baseUrl + 'pets/' + id);
  }
}
