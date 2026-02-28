import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { CancionService } from '../../../services/cancion/cancion.service';
import { CancionDto } from '../../../models/cancion/CancionDto.model';
import { CancionEditarComponent } from '../cancionEditar/cancion-editar.component';
import { GeneroCancionService } from '../../../services/generoCancion/genero-cancion.service';
import { GeneroCancionDto } from '../../../models/cancion/GeneroCancionDto.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cancion-list',
  imports: [CommonModule, CancionEditarComponent],
  templateUrl: './cancion-list.component.html',
  styleUrls: ['./cancion-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CancionListComponent implements OnInit {

  //INYECTAR LOS SERVICIOS
  _cancionService = inject(CancionService);
  _generoCancionService = inject(GeneroCancionService);

  canciones = signal<CancionDto[]>([]);
  generosCanciones: GeneroCancionDto[] = [];
  mostrarModal = false;
  modoEdicion: 'crear' | 'editar' = 'crear';
  cancionSeleccionada: CancionDto | null = null;

  constructor() { }

  ngOnInit() {
    this.getAllCanciones();
    this.getGenerosCanciones();
  }

  getGenerosCanciones() {
    this._generoCancionService.getAll().subscribe({
      next: (data) => {
        console.log('Géneros cargados:', data);
        this.generosCanciones = data;
      },
      error: (err) => { console.log('Ocurrió un error al cargar géneros', err); },
    });
  }

  getAllCanciones() {
    this._cancionService.getAll().subscribe({
      next: (data) => {
        console.log('Respuesta canciones:', data);
        this.canciones.set(data);
      },
      error: (err) => { console.log('Ocurrió un error al cargar canciones', err); },
      complete: () => { console.log('getAllCanciones completed'); }
    });
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
    this.getAllCanciones();
  }

  eliminarCancion(cancion: CancionDto): void {
    const confirmado = window.confirm(
      `¿Está seguro de eliminar la canción "${cancion.nombre ?? ''}" de ${cancion.artista ?? ''}?`
    );

    if (!confirmado) {
      return;
    }

    this._cancionService.delete(cancion.id).subscribe({
      next: () => {
        this.getAllCanciones();
      },
      error: (err) => {
        console.log('Ocurrió un error al eliminar', err);
      },
    });
  }

  obtenerNombreGenero(idGenero: number): string {
    const genero = this.generosCanciones.find(g => g.id === idGenero);
    return genero ? genero.nombre : 'Sin género';
  }
}
