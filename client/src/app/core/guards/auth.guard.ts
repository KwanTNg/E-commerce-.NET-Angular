import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { map, of } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);

  if (accountService.currentUser()) {
    //return it as observable for auth guard
    return of(true);
  } else {
    //no need to subscribe as auth guard automatically handle it
    return accountService.getAuthState().pipe(
      map(auth => {
        if (auth.isAuthenticated) {
          //no need to use of as it already return an observable
          return true
        } else {
           router.navigate(['/account/login'], {queryParams: {returnUrl: state.url}});
          return false;
        }
        })
      )
    }
  }
      
    
