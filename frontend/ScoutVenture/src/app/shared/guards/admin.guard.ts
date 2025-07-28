import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map, switchMap } from 'rxjs';
import { UserService } from '../services/user.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const userService = inject(UserService);

  return userService.loadUserInfo().pipe(
    map((userInfo) => {
      if (userInfo && userService.isAdmin()) {
        return true;
      } else {
        router.navigate(['/events/overview'], {
          queryParams: { error: 'access_denied' },
        });
        return false;
      }
    })
  );
};