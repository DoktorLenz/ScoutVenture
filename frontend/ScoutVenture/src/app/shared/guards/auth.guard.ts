import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  return router.navigate(['/auth/login'], {
    queryParams: { redirect: state.url },
  });
};
