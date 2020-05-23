import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';

import { FileUploader } from 'ng2-file-upload';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { listLocales } from 'ngx-bootstrap/chronos';

import { UserService } from 'src/app/_services/user.service';
import { LanguageService } from 'src/app/_services/language.service';
import { User } from 'src/app/_models/user';
import { ToasterService } from 'src/app/_services/toaster.service';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  editionForm: FormGroup;

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
              private translate: TranslateService) { }

  ngOnInit() {
    this.createEditionForm();
    this.bsConfig = Object.assign({}, { containerClass: 'theme-red' });
    this.localeService.use(this.langService.getCurrentLang());
    this.initializeUploader();
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

  createEditionForm() {

    // TODO : ajouter les regex sur les noms
    const numberRegex = '^[0-9]*$';

    this.editionForm = this.fb.group({
      username: [this.userService.currentUser.userName, Validators.required],
      firstname: [this.userService.currentUser.firstName, [Validators.required]],
      lastname: [this.userService.currentUser.lastName, [Validators.required]],
      dateOfBirth: [new Date(this.userService.currentUser.birthDate), Validators.required],
      email: [this.userService.currentUser.email, [Validators.required, Validators.email]],
      phonenumber: [this.userService.currentUser.phoneNumber, [Validators.required
        , Validators.pattern(numberRegex), Validators.minLength(10), Validators.maxLength(10)]],
      city: [this.userService.currentUser.city, [Validators.required]],
      address: [this.userService.currentUser.address, null],
      zipcode: [this.userService.currentUser.zipcode, Validators.pattern(numberRegex)]
    }, { validators: [this.userMustBeMajor] });
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { mismatch: true };
  }

  userMustBeMajor(g: FormGroup) {
    if (g.get('dateOfBirth').value != null) {
      const timeDiff = Math.abs(Date.now() - g.get('dateOfBirth').value);
      const age = Math.floor((timeDiff / (1000 * 3600 * 24)) / 365.25);
      return age >= 18 ? null : { underage: true };
    }
  }

  rollbackEditionForm() {
    this.createEditionForm();
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
      }, () => { }, () => {
        this.createEditionForm();
      });
    });
  }
}
