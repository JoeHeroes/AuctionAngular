import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private AuthService: AuthenticationService, private router: Router) {

  }

  canActivate(): boolean {
    if (this.AuthService.isLoggedIn()) {
      return true;
    } else {
      this.router.navigateByUrl("login");
      return false;
    }
  }

}
