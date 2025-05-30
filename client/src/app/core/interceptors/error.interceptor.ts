import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  //If we want to do something on the way out, we have to do it before the request
  //If way back, after the request
  const router = inject(Router);
  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      if (err.status === 400) {
        alert(err.error.title || err.error)
      }
      if (err.status === 401) {
        alert(err.error.title || err.error)
      }
      if (err.status === 404) {
        router.navigateByUrl('/not-found');
      }
      if (err.status === 500) {
        router.navigateByUrl('/server-error');
      }
      return throwError(() => err)
    })
  )
};
