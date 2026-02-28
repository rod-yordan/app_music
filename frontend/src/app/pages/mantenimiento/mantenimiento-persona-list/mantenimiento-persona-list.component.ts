import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { PersonaService } from '../../../services/persona/persona.service';
import { PersonaDto } from '../../../models/persona/PersonaDto.model';
import { MantenimientoPersonaEditarComponent } from '../mantenimiento-persona-editar/mantenimiento-persona-editar.component';
import { PersonTipoDocumentoServices } from '../../../services/personTipoDocumento/person-tipo-documento.service';
import { PersonaTipoDocumentoDto } from '../../../models/persona/PersonaTipoDocumentoDto.model';

@Component({
  selector: 'app-mantenimiento-persona-list',
  imports: [MantenimientoPersonaEditarComponent],
  templateUrl: './mantenimiento-persona-list.component.html',
  styleUrls: ['./mantenimiento-persona-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoPersonaListComponent implements OnInit {


  //INYECTAR LOS SERVICIOS HHTPCLIENT ==> PERSONA SERVICE

  _personaService = inject(PersonaService);
  _personaTipoDocumentoService = inject(PersonTipoDocumentoServices);

  personas = signal<PersonaDto[]>([]);
  tiposDocumentos:PersonaTipoDocumentoDto[] = [];
  mostrarModal = false;
  modoEdicion: 'crear' | 'editar' = 'crear';
  personaSeleccionada: PersonaDto | null = null;

  constructor() { }

  ngOnInit() {
    this.getAllPersonas();
    this.getTipoDocumento();
  }

  getTipoDocumento(){
    this._personaTipoDocumentoService.getAll().subscribe({
      next: (data) => {
        console.log(data);
        this.tiposDocumentos = data;
      },
      error: (err) => { console.log("ocurrio un error", err); },
    });
  }

  getAllPersonas() {

    this._personaService.getAll().subscribe({
      //next => quiere decir que se ejecuta cuando la respuesta es exitosa
      next: (data) => {
        console.log("respuesta", data);
        this.personas.set(data);
      },
      //error => se ejecuta cuando hay un error en la respuesta
      error: (err) => { console.log("ocurrio un error", err); },
      //complete => se ejecuta cuando la respuesta se completa, ya sea exitosa o con error
      complete: () => { console.log('getAllPersonas completed'); }
    });

  }

  abrirAgregar(): void {
    this.modoEdicion = 'crear';
    this.personaSeleccionada = null;
    this.mostrarModal = true;
  }

  abrirEditar(persona: PersonaDto): void {
    this.modoEdicion = 'editar';
    this.personaSeleccionada = { ...persona };
    this.mostrarModal = true;
  }

  cerrarModal(): void {
    this.mostrarModal = false;
    this.personaSeleccionada = null;
  }

  onGuardado(): void {
    this.cerrarModal();
    this.getAllPersonas();
  }

  eliminarPersona(persona: PersonaDto): void {
    const confirmado = window.confirm(
      `¿Está seguro de eliminar el registro de ${persona.nombres ?? ''} ${persona.apellidoPaterno ?? ''}?`
    );

    if (!confirmado) {
      return;
    }

    this._personaService.delete(persona.id).subscribe({
      next: () => {
        this.getAllPersonas();
      },
      error: (err) => {
        console.log('ocurrio un error al eliminar', err);
      },
    });
  }


}
