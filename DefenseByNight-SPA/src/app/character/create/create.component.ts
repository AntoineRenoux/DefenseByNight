import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AffiliateService } from 'src/app/_services/affiliate.service';
import { Affilate } from 'src/app/_models/affiliate';
import { ValidatorService } from 'src/app/_services/validator.service';
import { Chronicle } from 'src/app/_models/Chronicle';
import { ChronicleService } from 'src/app/_services/chronicle.service';
import { ArchetypesService } from 'src/app/_services/archetypes.service';
import { Archetype } from 'src/app/_models/archetype';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  affilations: Affilate[];
  archetypes: Archetype[];
  chronicles: Chronicle[];

  stepZeroForm: FormGroup;
  stepOneForm: FormGroup;

  constructor(private affilateService: AffiliateService,
              private fb: FormBuilder,
              private validatorService: ValidatorService,
              private chronicleService: ChronicleService,
              private translateService: TranslateService,
              private archetypeService: ArchetypesService) { }

  ngOnInit() {
    this.initializeStepZeroForm();
    this.initializeStepOneForm();
    this.affilateService.getAllAffiliations().subscribe(result => {
      this.affilations = result;
    });
    this.archetypeService.getAllArchetypes().subscribe(result => {
      this.archetypes = result;
    });
    this.chronicleService.getAll().subscribe(result => {
      this.chronicles = result;
    });
  }

  initializeStepZeroForm() {
    this.stepZeroForm = this.fb.group({
      chronicle: [null, Validators.required],
      // tslint:disable-next-line: max-line-length
      characterName: [null, [Validators.required, Validators.pattern(this.validatorService.firstNameRegex), Validators.maxLength(30)]],
      sect: [null, [Validators.required]]
    });
  }

  initializeStepOneForm() {
    this.stepOneForm = this.fb.group({
      archetypes: [null, Validators.required],
      concept: [null, [Validators.required, Validators.maxLength(100)]]
    });
  }
}
