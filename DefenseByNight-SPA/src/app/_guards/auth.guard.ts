import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_services/auth.service';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  // tslint:disable-next-line: max-line-length
  constructor(private authService: AuthService, private router: Router, private toaster: ToastrService, private translate: TranslateService) { }

  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }
    this.translate.get('ERR_NOT_ALLOWED').subscribe((res: string) => {
      this.toaster.error(res);
    });
    this.router.navigate(['/home']);
    return false;
  }

}
