import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

// 👇 IMPORTA TU MODELO
import { LoginResponse } from '../models/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7000/api/auth';

  constructor(private http: HttpClient) {}

  // 👇 YA TIPADO (IMPORTANTE)
  login(username: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, {
      username,
      password
    });
  }
}