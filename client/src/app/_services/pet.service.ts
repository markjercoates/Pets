import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, model, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { PetType } from '../_models/pettype';
import { Pet } from '../_models/pet';
import { PaginatedResult } from '../_models/pagination';
import { setPaginatedResponse } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class PetService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  paginatedResult = signal<PaginatedResult<Pet[]> | null>(null);
  
  getPets(pageNumber?: number, pageSize?: number) {
    let params = new HttpParams();

    if(pageNumber != null && pageSize != null){
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());
    }
    
    return this.http.get<Pet[]>(this.baseUrl + 'pets', {observe: 'response', params}).subscribe({
      next: response => {
        setPaginatedResponse(response, this.paginatedResult);
       /*  this.paginatedResult.set({
           items: response.body as Pet[],
           pagination: JSON.parse(response.headers.get('Pagination')!)
        }) */
      }   
    })
  }

  getPet(id:number) {
    return this.http.get<Pet>(this.baseUrl + 'pets/' + id);
  }

  getPetTypes(){
    return this.http.get<PetType[]>(this.baseUrl + 'pets/types');
 } 
}
