import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';
import Jwt_decode from 'jwt-decode';

export const authGuard: CanActivateFn = (route, state) => {

  const cookieService = inject(CookieService);
  const authService = inject(AuthService);
  const router = inject(Router);
  const user = authService.getUser();


  //Check for te JWD Token

  let token = cookieService.get('Authorization');

  if (token && user) {
    token = token.replace('Bearer ', '');
    const decotedToken: any = Jwt_decode(token);
    // Check Expire Date
    const expireDate = decotedToken.exp * 1000;
    const currentTime = new Date().getTime();

    if (expireDate < currentTime) {
      //logOut
      authService.logout();
      return router.createUrlTree(['/login'], {
        queryParams: {
          returnUrl: state.url
        }
      });
    } else {
      //Toke Still Valid
      if (user.roles.includes('Writer')) {
        return true;
      } else {
        alert('Unauthorised');
        return false;
      }
    }
  } else {
    //Logout
    authService.logout();
    return router.createUrlTree(['/login'], {
      queryParams: {
        returnUrl: state.url
      }
    });
  }
};
