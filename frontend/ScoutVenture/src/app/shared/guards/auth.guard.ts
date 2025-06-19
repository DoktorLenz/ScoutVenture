import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { catchError, map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const http = inject(HttpClient);

  return http.get<void>('/api/manage/info').pipe(
    map(() => true),
    catchError(() =>
      router.navigate(['/auth/login'], {
        queryParams: { redirect: state.url },
      })
    )
  );
};
