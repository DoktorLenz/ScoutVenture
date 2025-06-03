import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { catchError, mergeMap, of } from 'rxjs';

export const antiAuthGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const http = inject(HttpClient);

  return http.get<void>('/api/manage/info').pipe(
    mergeMap((x) => {
      return router.navigate(['/']);
    }),
    catchError(() => {
      console.log('User is not authenticated');
      return of(true);
    })
  );
};
