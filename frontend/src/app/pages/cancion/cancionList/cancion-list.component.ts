import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { CancionService } from '../../../services/cancion/cancion.service';
import { CancionDto } from '../../../models/cancion/CancionDto.model';
import { CancionEditarComponent } from '../cancionEditar/cancion-editar.component';
import { GeneroCancionService } from '../../../services/generoCancion/genero-cancion.service';
import { GeneroCancionDto } from '../../../models/cancion/GeneroCancionDto.model';
import { CommonModule } from '@angular/common';
import { forkJoin } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cancion-list',
  imports: [CommonModule, CancionEditarComponent],
  templateUrl: './cancion-list.component.html',
  styleUrls: ['./cancion-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CancionListComponent implements OnInit {

  _cancionService = inject(CancionService);
  _generoCancionService = inject(GeneroCancionService);
  _router = inject(Router);

  canciones = signal<CancionDto[]>([]);
  generosCanciones: GeneroCancionDto[] = [];
  mostrarModal = false;
  modoEdicion: 'crear' | 'editar' = 'crear';
  cancionSeleccionada: CancionDto | null = null;

  constructor() { }

  ngOnInit() {
    this.cargarDatosIniciales();
  }

  logout() {
    localStorage.removeItem('token');
    this._router.navigate(['/login']);
  }

  cargarDatosIniciales() {
    forkJoin({
      canciones: this._cancionService.getAll(),
      generos: this._generoCancionService.getAll()
    }).subscribe({
      next: (resultado) => {
        this.canciones.set(resultado.canciones);
        this.generosCanciones = resultado.generos;
      },
      error: (err) => {
        console.log('Error al cargar datos', err);
      }
    });
  }

  recargarCanciones() {
    this._cancionService.getAll().subscribe({
      next: (data) => {
        this.canciones.set(data);
      },
      error: (err) => { 
        console.log('Error al cargar canciones', err); 
      }
    });
  }

  recargarTodosLosDatos() {
    this.cargarDatosIniciales();
  }

  abrirAgregar(): void {
    this.modoEdicion = 'crear';
    this.cancionSeleccionada = null;
    this.mostrarModal = true;
  }

  abrirEditar(cancion: CancionDto): void {
    this.modoEdicion = 'editar';
    this.cancionSeleccionada = { ...cancion };
    this.mostrarModal = true;
  }

  cerrarModal(): void {
    this.mostrarModal = false;
    this.cancionSeleccionada = null;
  }

  onGuardado(): void {
    this.cerrarModal();
    this.recargarCanciones();
  }

  eliminarCancion(cancion: CancionDto): void {
    const confirmado = window.confirm(
      `¿Está seguro de eliminar la canción "${cancion.nombre ?? ''}" de ${cancion.artista ?? ''}?`
    );

    if (!confirmado) return;

    this._cancionService.delete(cancion.id).subscribe({
      next: () => {
        this.recargarCanciones();
      },
      error: (err) => {
        console.log('Error al eliminar', err);
      },
    });
  }

  obtenerNombreGenero(idGenero: number): string {
    const genero = this.generosCanciones.find(g => g.id === idGenero);
    return genero ? genero.nombre : 'Sin género';
  }
}