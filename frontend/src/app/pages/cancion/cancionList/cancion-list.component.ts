import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { CancionService } from '../../../services/cancion/cancion.service';
import { CancionDto } from '../../../models/cancion/CancionDto.model';
import { CancionEditarComponent } from '../cancionEditar/cancion-editar.component';
import { GeneroCancionService } from '../../../services/generoCancion/genero-cancion.service';
import { GeneroCancionDto } from '../../../models/cancion/GeneroCancionDto.model';
import { CommonModule } from '@angular/common';
import { forkJoin } from 'rxjs';

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
    this.cargarDatosIniciales();
  }

  /**
   * Método que carga los datos iniciales usando forkJoin para paralelizar las peticiones
   */
  cargarDatosIniciales() {
    // Usamos forkJoin para ejecutar ambas peticiones en paralelo
    forkJoin({
      canciones: this._cancionService.getAll(),
      generos: this._generoCancionService.getAll()
    }).subscribe({
      next: (resultado) => {
        console.log('Datos cargados exitosamente:', resultado);
        
        // Asignamos los resultados
        this.canciones.set(resultado.canciones);
        this.generosCanciones = resultado.generos;
      },
      error: (err) => {
        console.log('Ocurrió un error al cargar los datos iniciales', err);
        
        // Si hay error, podemos mostrar un mensaje o manejar el error según necesidades
        // Por ejemplo, podrías tener un signal para manejar errores
      },
      complete: () => {
        console.log('Carga de datos iniciales completada');
      }
    });
  }

  /**
   * Método para recargar solo las canciones cuando sea necesario
   * Útil después de operaciones CRUD que solo afectan a canciones
   */
  recargarCanciones() {
    this._cancionService.getAll().subscribe({
      next: (data) => {
        console.log('Respuesta canciones:', data);
        this.canciones.set(data);
      },
      error: (err) => { 
        console.log('Ocurrió un error al cargar canciones', err); 
      }
    });
  }

  /**
   * Método para recargar todos los datos
   * Útil si hay cambios que afectan tanto a canciones como a géneros
   */
  recargarTodosLosDatos() {
    this.cargarDatosIniciales();
  }

  getGenerosCanciones() {
    // Este método ya no es necesario en el ngOnInit
    // Pero lo mantenemos por si se necesita usar en otro lugar
    this._generoCancionService.getAll().subscribe({
      next: (data) => {
        console.log('Géneros cargados:', data);
        this.generosCanciones = data;
      },
      error: (err) => { console.log('Ocurrió un error al cargar géneros', err); },
    });
  }

  getAllCanciones() {
    // Este método ya no es necesario en el ngOnInit
    // Pero lo mantenemos por si se necesita usar en otro lugar
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
    // Después de guardar, recargamos solo las canciones
    // ya que los géneros probablemente no cambiaron
    this.recargarCanciones();
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
        // Después de eliminar, recargamos solo las canciones
        this.recargarCanciones();
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