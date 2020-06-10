import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';

import { FileUploader } from 'ng2-file-upload';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { listLocales } from 'ngx-bootstrap/chronos';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import '@ckeditor/ckeditor5-build-classic/build/translations/fr';

import { UserService } from 'src/app/_services/user.service';
import { LanguageService } from 'src/app/_services/language.service';
import { User } from 'src/app/_models/user';
import { ToasterService } from 'src/app/_services/toaster.service';
import { Photo } from 'src/app/_models/photo';
import { Health } from 'src/app/_models/health';
import { AuthService } from 'src/app/_services/auth.service';
import { Security } from 'src/app/_models/security';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  // TODO : ajouter les regex sur les noms
  numberRegex = '^[0-9]*$';
  firstNameRegex = '((^|-)[a-zA-Z][a-zà-ü]+)+';
  lastNameRegex = '((^| )[a-zA-Z][a-zà-ü]+)+';
  cityRegex = '((^| )[a-zA-Z][a-zà-ü]+)+';

  public Editor = ClassicEditor;
  configEditor: any;

  editionForm: FormGroup;
  contactForm: FormGroup;
  securityForm: FormGroup;

  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  response: string;

  bsConfig: Partial<BsDatepickerConfig>;
  locales = listLocales();

  constructor(public userService: UserService,
              private localeService: BsLocaleService,
              private langService: LanguageService,
              private fb: FormBuilder,
              private toaster: ToasterService,
              private translate: TranslateService,
              private authService: AuthService) { }

  ngOnInit() {
    this.createEditionForm();
    this.bsConfig = Object.assign({}, { containerClass: 'theme-red' });
    this.localeService.use(this.langService.getCurrentLang());
    this.initializeUploader();
    this.initializeEditor();
    this.createContactForm();
    this.createSecurityForm();
  }

  initializeEditor() {
    this.configEditor = {
      toolbar: [
        'heading',
        '|',
        'bold',
        'italic',
        'undo',
        'redo'
      ],
      language: this.langService.getCurrentLang()
    };
  }

  createSecurityForm() {
    this.securityForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required]]
    }, this.passwordMatchValidator);
  }

  createContactForm() {
    this.contactForm = this.fb.group({
      allergies: [this.userService.currentUser.health?.allergies, null],
      firstnameContact: [this.userService.currentUser.health?.contactFirstName, Validators.required],
      lastnameContact: [this.userService.currentUser.health?.contactLastName, Validators.required],
      phonenumberContact: [this.userService.currentUser.health?.phoneNumber, [Validators.required, Validators.minLength(10)
        , Validators.maxLength(10), Validators.pattern(this.numberRegex)]]
    });
  }

  createEditionForm() {
    this.editionForm = this.fb.group({
      username: [this.userService.currentUser.userName, Validators.required],
      firstname: [this.userService.currentUser.firstName, [Validators.required, Validators.pattern(this.firstNameRegex)]],
      lastname: [this.userService.currentUser.lastName, [Validators.required, Validators.pattern(this.lastNameRegex)]],
      dateOfBirth: [new Date(this.userService.currentUser.birthDate), Validators.required],
      email: [this.userService.currentUser.email, [Validators.required, Validators.email]],
      phonenumber: [this.userService.currentUser.phoneNumber, [Validators.required
        , Validators.pattern(this.numberRegex), Validators.minLength(10), Validators.maxLength(10)]],
      city: [this.userService.currentUser.city],
      address: [this.userService.currentUser.address, null],
      zipcode: [this.userService.currentUser.zipcode, Validators.pattern(this.numberRegex)]
    }, { validators: [this.userMustBeMajor] });
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('newPassword').value === g.get('confirmPassword').value ? null : { mismatch: true };
  }

  userMustBeMajor(g: FormGroup) {
    if (g.get('dateOfBirth').value != null) {
      const timeDiff = Math.abs(Date.now() - g.get('dateOfBirth').value);
      const age = Math.floor((timeDiff / (1000 * 3600 * 24)) / 365.25);
      return age >= 18 ? null : { underage: true };
    }
  }


  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.userService.baseUrl + this.userService.currentUser.id + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      queueLimit: 1,
      maxFileSize: 10 * 1021 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description
        };

        this.userService.currentUser.photo = photo;
      }
    };
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  updateHealth() {
    const health = new Health();

    health.allergies = this.contactForm.get('allergies').value;
    health.contactFirstName = this.contactForm.get('firstnameContact').value;
    health.contactLastName = this.contactForm.get('lastnameContact').value;
    health.phoneNumber = this.contactForm.get('phonenumberContact').value;

    this.userService.editHealth(health).subscribe(() => {
      this.translate.get('SUCCESS_SAVE').subscribe((res: string) => {
        this.toaster.success(res);
      }, () => { }, () => {
        this.createContactForm();
      });
    });
  }

  updateUser() {
    const user = new User();

    user.id = this.userService.currentUser.id;
    user.userName = this.editionForm.get('username').value;
    user.firstName = this.editionForm.get('firstname').value;
    user.lastName = this.editionForm.get('lastname').value;
    user.birthDate = this.editionForm.get('dateOfBirth').value;
    user.email = this.editionForm.get('email').value;
    user.phoneNumber = this.editionForm.get('phonenumber').value;
    user.city = this.editionForm.get('city').value;
    user.address = this.editionForm.get('address').value;
    user.zipcode = this.editionForm.get('zipcode').value;

    this.userService.editUser(user).subscribe(() => {
      this.translate.get('SUCCESS_SAVE').subscribe((res: string) => {
        this.toaster.success(res);
      }, () => {
        this.translate.get('ERR_UPDATING_USER').subscribe((res: string) => {
          this.toaster.error(res);
        });
      }, () => {
        this.createEditionForm();
      });
    });
  }

  updateSecurity() {
    const model = new Security();

    model.confirmPassword = this.securityForm.get('confirmPassword').value;
    model.currentPassword = this.securityForm.get('currentPassword').value;
    model.newPassword = this.securityForm.get('newPassword').value;

    this.authService.changePassword(model).subscribe(() => {
      this.translate.get('SUCCESS_SAVE').subscribe((res: string) => {
        this.toaster.success(res);
      });
    }, (error) => {
      this.translate.get('ERR_CHANGING_PASSWORD').subscribe((res: string) => {
        this.toaster.error(res);
        console.log(error);
      });
    });
  }
}
