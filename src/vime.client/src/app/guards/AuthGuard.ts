import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { UserService } from '../users/services/user.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private userService: UserService, private router: Router) {}
  isAuthenticated: boolean;

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    this.userService.userData$.subscribe((userData) => {
      this.isAuthenticated = userData.isAuthenticated;
    });
    if (this.isAuthenticated) {
      return true;
    } else {
      this.router.navigate(['auth/login']);
      return false;
    }
  }
}
