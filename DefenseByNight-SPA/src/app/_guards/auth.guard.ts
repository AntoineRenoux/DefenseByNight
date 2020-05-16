import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router, private toaster: ToastrService) { }

  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }

    this.toaster.error('You shall not pass !');
    this.router.navigate(['/home']);
    return false;
  }

}
