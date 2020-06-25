import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AffiliateService } from 'src/app/_services/affiliate.service';
import { Affilate } from 'src/app/_models/affiliate';
import { ValidatorService } from 'src/app/_services/validator.service';
import { Chronicle } from 'src/app/_models/Chronicle';
import { ChronicleService } from 'src/app/_services/chronicle.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  public affilations: Affilate[];
  stepZeroForm: FormGroup;
  stepOneForm: FormGroup;
  chronicles: Chronicle[];

  constructor(private affilateService: AffiliateService,
              private fb: FormBuilder,
              private validatorService: ValidatorService,
              private chronicleService: ChronicleService,
              private translateService: TranslateService) { }

  ngOnInit() {
    this.initializeStepZeroForm();
    this.initializeStepOneForm();
    this.affilateService.getAllAffiliations().subscribe((result) => {
      this.affilations = result;
    });
    this.chronicleService.getAll().subscribe(result => {
      this.chronicles = result;
    });
  }

  initializeStepZeroForm() {
    this.stepZeroForm = this.fb.group({
      chronicle: [null, Validators.required],
      // tslint:disable-next-line: max-line-length
      characterName: [null, [Validators.required, Validators.pattern(this.validatorService.firstNameRegex)]],
      sect: [null, [Validators.required]]
    }, {updateOn: 'blur'});
  }

  initializeStepOneForm() {
    this.stepOneForm = this.fb.group({
      archetypes: [null, Validators.required],
      concept: [null, Validators.required]
    });
  }
}
