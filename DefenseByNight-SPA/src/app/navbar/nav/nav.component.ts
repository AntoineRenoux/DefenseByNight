import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

import { AuthService } from '../../_services/auth.service';
import { ToasterService } from '../../_services/toaster.service';
import { UserService } from 'src/app/_services/user.service';
import { ReferencesService } from 'src/app/_services/references.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  public banner: any;

  // tslint:disable-next-line: max-line-length
  constructor(public authService: AuthService,
              private toaster: ToasterService,
              public router: Router,
              private translate: TranslateService,
              public userService: UserService,
              private refService: ReferencesService) { }

  ngOnInit(): void {
    this.refService.getBanner().subscribe((res) => {
      this.banner = res.value;
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.userService.currentUser = null;
    this.router.navigate(['user/anonyme']);
    this.translate.get('GEN_LBL_DISCONNECT').subscribe((res: string) => {
      this.toaster.success(res);
    });
  }
}
