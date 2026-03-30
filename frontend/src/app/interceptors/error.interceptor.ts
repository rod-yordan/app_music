import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  
  return next(req).pipe(
    catchError((error) => {
      let errorMessage = 'Ocurrió un error desconocido';
      let errorId: number | null = null;

      if (error.error instanceof ErrorEvent) {
        // Error del lado del cliente
        errorMessage = `Error: ${error.error.message}`;
        console.error('Error del cliente:', error.error);
      } else {
        // Error del servidor
        errorId = error.error?.errorId || error.error?.ErrorId;
        errorMessage = error.error?.message || error.error?.Message || `Error ${error.status}: ${error.statusText}`;
        
        console.error('Error del servidor:', {
          status: error.status,
          message: errorMessage,
          errorId: errorId,
          url: error.url
        });

        // Manejar errores específicos por código de estado
        switch (error.status) {
          case 401:
            errorMessage = 'Credenciales incorrectas.';
            // Redirigir al login
            router.navigate(['/login']);
            break;
          case 403:
            errorMessage = 'No tienes permiso para realizar esta acción.';
            break;
          case 404:
            errorMessage = 'El recurso solicitado no existe.';
            break;
          case 500:
            if (errorId) {
              errorMessage = `Error interno del servidor. ID de error: ${errorId}`;
            } else {
              errorMessage = 'Error interno del servidor. Por favor, intenta más tarde.';
            }
            break;
        }
      }

      // Mostrar mensaje de error (puedes cambiar alert por un modal o toast)
      alert(errorMessage);

      return throwError(() => error);
    })
  );
};