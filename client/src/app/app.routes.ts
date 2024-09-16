import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PetListComponent } from './pets/pet-list/pet-list.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'pets', component: PetListComponent},
    {path: '**', component: HomeComponent, pathMatch: 'full'},
];
