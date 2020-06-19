import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AffiliateService } from 'src/app/_services/affiliate.service';
import { Affilate } from 'src/app/_models/affiliate';
import { ValidatorService } from 'src/app/_services/validator.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  public affilations: Affilate[];
  characterForm: FormGroup;

  constructor(private translate: TranslateService,
              private affilateService: AffiliateService,
              private fb: FormBuilder,
              private validatorService: ValidatorService) { }

  ngOnInit() {
    this.initializeCreationForm();
    this.affilateService.getAllAffiliations().subscribe((result) => {
      this.affilations = result;
    });
  }

  initializeCreationForm() {
    this.characterForm = this.fb.group({
      // tslint:disable-next-line: max-line-length
      characterName: [null, [Validators.required, Validators.pattern(this.validatorService.firstNameRegex)]],
      sect: [null, [Validators.required]]
    });
  }

}
