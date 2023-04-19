import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { TokenService } from '../services/token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private tokenService: TokenService, private router: Router) {

  }

  canActivate(): boolean {
    if (this.tokenService.isLoggedIn()) {
      return true;
    } else {
      this.router.navigateByUrl("login");
      return false;
    }
  }

}
