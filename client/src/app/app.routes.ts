import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PetListComponent } from './pets/pet-list/pet-list.component';
import { PetCreateComponent } from './pets/pet-create/pet-create.component';
import { LoginComponent } from './account/login/login.component';
import { authGuard } from './_guards/auth.guard';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'pets', component: PetListComponent },
  {
    path: 'pets/create',
    component: PetCreateComponent,
    canActivate: [authGuard],
  },
  { path: 'account/login', component: LoginComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];
