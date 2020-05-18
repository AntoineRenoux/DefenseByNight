import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

import { AuthService } from '../../_services/auth.service';
import { UserLogin } from '../../_models/userLogin';
import { ToasterService } from '../../_services/toaster.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model = new UserLogin();

  // tslint:disable-next-line: max-line-length
  constructor(public authService: AuthService, private toaster: ToasterService, private router: Router, private translate: TranslateService) { }

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.translate.get('GEN_LBL_CONNECTED').subscribe((res: string) => {
        this.toaster.success(res);
      });
    }, error => {
      this.translate.get('ERR_NOT_ALLOWED').subscribe((res: string) => {
        this.toaster.info(res);
      });
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.translate.get('GEN_LBL_DISCONNECT').subscribe((res: string) => {
      this.toaster.success(res);
    });
    this.router.navigate(['']);
  }
}
