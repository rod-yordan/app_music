import { Routes } from '@angular/router';
import { MantenimientoPersonaListComponent } from './pages/mantenimiento/mantenimiento-persona-list/mantenimiento-persona-list.component';
import { CancionListComponent } from './pages/cancion/cancionList/cancion-list.component';

export const routes: Routes = [

    {
        path: '',
        component: CancionListComponent
    },
    {
        path: 'personas',
        component: MantenimientoPersonaListComponent
    },
    {
        path: 'canciones',
        component: CancionListComponent
    },
    {
        path: '**',
        redirectTo: 'canciones'
    }

];
