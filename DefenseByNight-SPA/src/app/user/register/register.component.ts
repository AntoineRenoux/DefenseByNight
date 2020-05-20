import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';

import { ToasterService } from '../../_services/toaster.service';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';
import { LanguageService } from 'src/app/_services/language.service';

import { listLocales } from 'ngx-bootstrap/chronos';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  private user = new User();
  registerForm: FormGroup;

  bsConfig: Partial<BsDatepickerConfig>;
  locales = listLocales();

  // tslint:disable-next-line: max-line-length
  constructor(private toaster: ToasterService, private translate: TranslateService,
              private authService: AuthService, private fb: FormBuilder,
              private langService: LanguageService, private localeService: BsLocaleService) { }

  ngOnInit() {
    this.createRegisterForm();
    this.bsConfig = Object.assign({}, { containerClass: 'theme-red' });
    this.localeService.use('gb');
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      dateOfBirth: [null, Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phonenumber: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required],
    }, { validators: this.passwordMatchValidator });
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { mismatch: true };
  }

  register() {
    console.log(this.registerForm);
  }

}
