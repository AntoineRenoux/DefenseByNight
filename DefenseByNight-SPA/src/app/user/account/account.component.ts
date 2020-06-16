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
import { ValidatorService } from 'src/app/_services/validator.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

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
    private authService: AuthService,
    private validatorService: ValidatorService) { }

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

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
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

  createEditionForm() {
    this.editionForm = this.fb.group({
      username: [this.userService.currentUser.userName, [Validators.required, Validators.pattern(this.validatorService.userNameRegex)]],
      firstname: [this.userService.currentUser.firstName, [Validators.required, Validators.pattern(this.validatorService.firstNameRegex)]],
      lastname: [this.userService.currentUser.lastName, [Validators.required, Validators.pattern(this.validatorService.lastNameRegex)]],
      dateOfBirth: [new Date(this.userService.currentUser.birthDate), Validators.required],
      email: [this.userService.currentUser.email, [Validators.required, Validators.email
        , Validators.pattern(this.validatorService.emailRegex)]],
      phonenumber: [this.userService.currentUser.phoneNumber
        , [Validators.required, Validators.pattern(this.validatorService.phoneNumberRegex)]],
      city: [this.userService.currentUser.city, Validators.pattern(this.validatorService.cityRegex)],
      address: [this.userService.currentUser.address, Validators.pattern(this.validatorService.addressRegex)],
      zipcode: [this.userService.currentUser.zipcode, Validators.pattern(this.validatorService.zipCodeRegex)]
    }, { validators: [this.validatorService.userMustBeMajor], updateOn: 'blur' });
  }

  createContactForm() {
    this.contactForm = this.fb.group({
      allergies: [this.userService.currentUser.health?.allergies, null],
      firstnameContact: [this.userService.currentUser.health?.contactFirstName, [Validators.required
        , Validators.pattern(this.validatorService.firstNameRegex)]],
      lastnameContact: [this.userService.currentUser.health?.contactLastName
        , [Validators.required, Validators.pattern(this.validatorService.lastNameRegex)]],
      phonenumberContact: [this.userService.currentUser.health?.phoneNumber,
      [Validators.required, Validators.pattern(this.validatorService.phoneNumberRegex)]]
    });
  }

  createSecurityForm() {
    this.securityForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(40)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(40)]]
    }, this.validatorService.changedPasswordMatch);
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

  updateHealth() {
    const health = new Health();

    health.allergies = this.contactForm.get('allergies').value;
    health.contactFirstName = this.contactForm.get('firstnameContact').value;
    health.contactLastName = this.contactForm.get('lastnameContact').value;
    health.phoneNumber = this.contactForm.get('phonenumberContact').value;

    this.userService.editHealth(health).subscribe(() => {
      this.translate.get('SUCCESS_SAVE').subscribe((res: string) => {
        this.toaster.success(res);
      }, (error) => {
        this.translate.get(error.error).subscribe((res: string) => {
          this.toaster.error(res);
        });
      }, () => {
        this.createContactForm();
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
      this.translate.get(error.error).subscribe((res: string) => {
        this.toaster.error(res);
      });
    });
  }
}
