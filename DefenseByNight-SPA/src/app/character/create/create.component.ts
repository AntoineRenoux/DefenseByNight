import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import '@ckeditor/ckeditor5-build-classic/build/translations/fr';

import { AffiliateService } from 'src/app/_services/affiliate.service';
import { Affilate } from 'src/app/_models/affiliate';
import { ValidatorService } from 'src/app/_services/validator.service';
import { Chronicle } from 'src/app/_models/Chronicle';
import { ChronicleService } from 'src/app/_services/chronicle.service';
import { ArchetypesService } from 'src/app/_services/archetypes.service';
import { Archetype } from 'src/app/_models/archetype';
import { ClanService } from 'src/app/_services/clan.service';
import { Clan } from 'src/app/_models/clan';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  affilations: Affilate[];
  archetypes: Archetype[];
  chronicles: Chronicle[];
  clans: Clan[];

  stepZeroForm: FormGroup;
  stepOneForm: FormGroup;
  stepTwoForm: FormGroup;

  Editor = ClassicEditor;
  configEditor: any;
  placeholderForEditor: string;

  constructor(private affilateService: AffiliateService,
              private fb: FormBuilder,
              private validatorService: ValidatorService,
              private chronicleService: ChronicleService,
              private translateService: TranslateService,
              private archetypeService: ArchetypesService,
              private clanService: ClanService) { }

  ngOnInit() {
    this.initializeStepZeroForm();
    this.initializeStepOneForm();
    this.initializeStepTwoForm();

    this.affilateService.getAllAffiliations().subscribe(result => {
      this.affilations = result;
    });
    this.archetypeService.getAllArchetypes().subscribe(result => {
      this.archetypes = result;
    });
    this.chronicleService.getAll().subscribe(result => {
      this.chronicles = result;
    });
    this.clanService.getAllClans().subscribe(result => {
      this.clans = result;
    });
    this.translateService.get('CREATION_CHARACTER_CONCEPT_PLACEHOLDER').subscribe(res => {
      this.placeholderForEditor = res;
    });

    this.initializeEditor();
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

  initializeStepTwoForm() {
    this.stepTwoForm = this.fb.group({
      clan: [null, Validators.required]
    });
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
      language: this.translateService.getDefaultLang(),
      placeholder: this.placeholderForEditor
    };
  }
}
