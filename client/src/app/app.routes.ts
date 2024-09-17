import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PetListComponent } from './pets/pet-list/pet-list.component';
import { PetCreateComponent } from './pets/pet-create/pet-create.component';
import { LoginComponent } from './login/login.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'pets', component: PetListComponent },
  { path: 'pets/create', component: PetCreateComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];
