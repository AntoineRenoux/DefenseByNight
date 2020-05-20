import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { UserLogin } from 'src/app/_models/userLogin';
import { Router } from '@angular/router';

@Component({
  selector: 'app-connexion',
  templateUrl: './connexion.component.html',
  styleUrls: ['./connexion.component.css']
})
export class ConnexionComponent implements OnInit {
  model = new UserLogin();

  // tslint:disable-next-line: max-line-length
  constructor(private authService: AuthService, private toaster: ToastrService, private translate: TranslateService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.router.navigate(['dashboard/home']);
      this.translate.get('GEN_LBL_CONNECTED').subscribe((res: string) => {
        this.toaster.success(res);
      });
    }, error => {
      this.translate.get('ERR_NOT_ALLOWED').subscribe((res: string) => {
        this.toaster.info(res);
      });
    });
  }
}
