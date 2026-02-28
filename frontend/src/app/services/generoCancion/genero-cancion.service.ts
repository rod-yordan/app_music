import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GeneroCancionDto } from '../../models/cancion/GeneroCancionDto.model';

@Injectable({
  providedIn: 'root'
})
export class GeneroCancionService {

  //inyectar el servicio de httpclient
  http = inject(HttpClient);

  private apiUrl = 'https://localhost:7000/api/GeneroCancion';

  constructor() { }

  getAll(): Observable<GeneroCancionDto[]> {
    return this.http.get<GeneroCancionDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<GeneroCancionDto> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<GeneroCancionDto>(url);
  }
}
