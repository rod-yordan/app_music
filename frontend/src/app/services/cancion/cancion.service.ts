import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CancionDto } from '../../models/cancion/CancionDto.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CancionService {

  //inyectar el servicio de httpclient
  http = inject(HttpClient);

  private apiUrl = 'https://localhost:7000/api/Cancion';

  constructor() { }

  getAll(): Observable<CancionDto[]> {
    return this.http.get<CancionDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<CancionDto> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<CancionDto>(url);
  }

  create(data: CancionDto): Observable<CancionDto> {
    return this.http.post<CancionDto>(this.apiUrl, data);
  }

  update(data: CancionDto): Observable<CancionDto> {
    return this.http.put<CancionDto>(this.apiUrl, data);
  }

  delete(id: number): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url);
  }
}
