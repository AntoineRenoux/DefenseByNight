import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { listLocales, updateLocale } from 'ngx-bootstrap/chronos';

import { ToasterService } from '../../_services/toaster.service';
import { AuthService } from 'src/app/_services/auth.service';
import { LanguageService } from 'src/app/_services/language.service';
import { UserRegister } from 'src/app/_models/userRegister';
import { Router } from '@angular/router';
import { ValidatorService } from 'src/app/_services/validator.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  bsConfig: Partial<BsDatepickerConfig>;
  locales = listLocales();

  // tslint:disable-next-line: max-line-length
  constructor(private toaster: ToasterService, private translate: TranslateService,
              private authService: AuthService, private fb: FormBuilder,
              private langService: LanguageService, private localeService: BsLocaleService,
              private routerService: Router, private validatorService: ValidatorService) { }

  ngOnInit() {
    this.createRegisterForm();
    this.bsConfig = Object.assign({}, { containerClass: 'theme-red' });
    this.localeService.use(this.langService.getCurrentLang());
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
    }, { validators: [this.validatorService.passwordMatchValidator, this.validatorService.userMustBeMajor], updateOn: 'blur' });
  }

  register() {
    const user = new UserRegister();

    user.username = this.registerForm.controls.username.value;
    user.firstName = this.registerForm.controls.firstname.value;
    user.lastName = this.registerForm.controls.lastname.value;
    user.birthDate = this.registerForm.controls.dateOfBirth.value;
    user.email = this.registerForm.controls.email.value;
    user.phoneNumber = this.registerForm.controls.phonenumber.value;
    user.password = this.registerForm.controls.password.value;
    user.confirmPassword = this.registerForm.controls.confirmPassword.value;

    this.authService.register(user).subscribe(() => {
      this.translate.get('SUCCESS_CREATION_ACCOUNT').subscribe((res: string) => {
        this.routerService.navigate(['user/connexion']);
        this.toaster.success(res);
      });
    }, (error) => {
      this.translate.get(error.error).subscribe((res: string) => {
        this.toaster.error(res);
      });
    });
  }
}
