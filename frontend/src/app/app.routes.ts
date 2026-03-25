import { Routes } from '@angular/router';
import { MantenimientoPersonaListComponent } from './pages/mantenimiento/mantenimiento-persona-list/mantenimiento-persona-list.component';
import { CancionListComponent } from './pages/cancion/cancionList/cancion-list.component';
import { LoginComponent } from './pages/login.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'canciones',
    component: CancionListComponent,
    canActivate: [authGuard]
  },
  {
    path: 'personas',
    component: MantenimientoPersonaListComponent,
    canActivate: [authGuard]
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];