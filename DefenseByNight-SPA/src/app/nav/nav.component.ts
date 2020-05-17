import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../_services/auth.service';
import { UserLogin } from '../_models/userLogin';
import { ToasterService } from '../_services/toaster.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model = new UserLogin();

  // tslint:disable-next-line: max-line-length
  constructor(public authService: AuthService, private toaster: ToasterService, private router: Router) { }

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
     this.toaster.success('Connecté_proxy');
    }, error => {
      this.toaster.error('Non authorisé_proxy');
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
    this.toaster.info('Déconnecté_proxy');
    this.router.navigate(['']);
  }
}
