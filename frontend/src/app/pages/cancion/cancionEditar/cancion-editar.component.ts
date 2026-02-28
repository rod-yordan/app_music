import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  output,
} from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CancionDto } from '../../../models/cancion/CancionDto.model';
import { CancionService } from '../../../services/cancion/cancion.service';
import { GeneroCancionDto } from '../../../models/cancion/GeneroCancionDto.model';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-cancion-editar',
  imports: [ReactiveFormsModule],
  templateUrl: './cancion-editar.component.html',
  styleUrls: ['./cancion-editar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CancionEditarComponent {

  //DECLARANDO VARIABLES DE ENTRADA QUE PROVIENE DEL COMPONENTE PADRE (CANCION-LIST)
  // PARA SABER SI SE ESTA CREANDO O EDITANDO UNA CANCIÓN
  cancion = input<CancionDto | null>(null);
  generos = input<GeneroCancionDto[] | null>(null);
  modo = input<'crear' | 'editar'>('crear');

  //DECLARANDO VARIABLES DE SALIDA QUE PROVIENE DEL COMPONENTE PADRE (CANCION-LIST)
  cancelar = output<void>();
  guardado = output<void>();

  //INYECTAMOS EL SERVICIO DE CANCION SERVICES
  private readonly cancionService = inject(CancionService);
  //INYECTAMOS EL FORM BUILDER PARA CONSTRUIR EL FORMULARIO REACTIVO
  private readonly formBuilder = inject(FormBuilder);

  //INICIAMOS LA CONSTRUCCIÓN DEL FORMULARIO REACTIVO CON LOS CAMPOS CORRESPONDIENTES A LA ENTIDAD CANCION
  readonly form = this.formBuilder.group({
    nombre: ['', [Validators.required]],
    artista: ['', [Validators.required]],
    fechaLanzamiento: [''],
    idGeneroCancion: [0, [Validators.required]],
  });

  cargando = false;

  //CONSTRUCTOR DONDE SE EJECUTA 
  // UN EFECTO CUANDO LA PROPIEDAD cancion CAMBIA, PARA ACTUALIZAR LOS VALORES 
  // DEL FORMULARIO CON LOS DATOS DE LA CANCIÓN SELECCIONADA
  constructor() {
    effect(() => {
      const cancion = this.cancion();

      // Formatear fecha para input type="date" (YYYY-MM-DD)
      let fechaFormateada = '';
      if (cancion?.fechaLanzamiento) {
        const fecha = new Date(cancion.fechaLanzamiento);
        fechaFormateada = fecha.toISOString().split('T')[0];
      }

      this.form.reset({
        nombre: cancion?.nombre ?? '',
        artista: cancion?.artista ?? '',
        fechaLanzamiento: fechaFormateada,
        idGeneroCancion: cancion?.idGeneroCancion,
      });
    });
  }

  onCancelar(): void {
    this.cancelar.emit();
  }

  onGuardar(): void {
    this.form.markAllAsTouched();
    if (this.form.invalid || this.cargando) {
      return;
    }

    this.cargando = true;

    const valores = this.form.getRawValue();
    const actual = this.cancion();
    
    const payload: CancionDto = {
      id: actual?.id ?? 0,
      nombre: valores.nombre,
      artista: valores.artista,
      fechaLanzamiento: valores.fechaLanzamiento ? new Date(valores.fechaLanzamiento).toISOString() : null,
      idGeneroCancion: valores.idGeneroCancion ?? 0
    };

    const request$ = this.modo() === 'editar' && payload.id > 0
      ? this.cancionService.update(payload)
      : this.cancionService.create(payload);

    request$.subscribe({
      next: () => {
        this.guardado.emit();
      },
      error: (error) => {
        console.error('Error al guardar canción', error);
        this.cargando = false;
      },
      complete: () => {
        this.cargando = false;
      },
    });
  }
}
