import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

import { AffiliateService } from 'src/app/_services/affiliate.service';
import { Affilate } from 'src/app/_models/affiliate';
import { ValidatorService } from 'src/app/_services/validator.service';
import { Chronicle } from 'src/app/_models/Chronicle';
import { ChronicleService } from 'src/app/_services/chronicle.service';
import { ArchetypesService } from 'src/app/_services/archetypes.service';
import { Archetype } from 'src/app/_models/archetype';
import { ClanService } from 'src/app/_services/clan.service';
import { Clan } from 'src/app/_models/clan';
import { Character } from 'src/app/_models/character';
import { AttributeService } from 'src/app/_services/attribute.service';
import { Focus } from 'src/app/_models/focus';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  newCharacter = new Character();

  affilations: Affilate[];
  archetypes: Archetype[];
  physicalFocus: Focus[];
  socialFocus: Focus[];
  mentalFocus: Focus[];
  chronicles: Chronicle[];
  clans: Clan[];

  stepZeroForm: FormGroup;
  stepOneForm: FormGroup;
  stepTwoForm: FormGroup;
  stepThreeForm: FormGroup;

  maxRatePrimaryAttribut = 7;
  maxRateSecondaryAttribut = 5;
  maxRateThirdAttribut = 3;

  maxPhysicalAttribut: number;
  maxSocialAttribut: number;
  maxMentalAttribut: number;

  constructor(private affilateService: AffiliateService,
              private fb: FormBuilder,
              private validatorService: ValidatorService,
              private chronicleService: ChronicleService,
              private archetypeService: ArchetypesService,
              private clanService: ClanService,
              private attributeService: AttributeService) { }

  ngOnInit() {
    this.maxPhysicalAttribut = this.maxRatePrimaryAttribut;
    this.maxSocialAttribut = this.maxRatePrimaryAttribut;
    this.maxMentalAttribut = this.maxRatePrimaryAttribut;

    this.initializeStepZeroForm();
    this.initializeStepOneForm();
    this.initializeStepTwoForm();
    this.initializeStepThreeForm();

    this.affilateService.getAllAffiliations().subscribe(result => {
      this.affilations = result;
    });
    this.archetypeService.getAllArchetypes().subscribe(result => {
      this.archetypes = result;
    });
    this.attributeService.getAll().subscribe(result => {
      this.physicalFocus = result.find(a => a.attributeKey === 'PHYSICAL_KEY').focus;
      this.socialFocus = result.find(a => a.attributeKey === 'SOCIAL_KEY').focus;
      this.mentalFocus = result.find(a => a.attributeKey === 'MENTAL_KEY').focus;
    });
    this.chronicleService.getAll().subscribe(result => {
      this.chronicles = result;
    });
    this.clanService.getAllClans().subscribe(result => {
      this.clans = result;
    });
  }

  initializeStepZeroForm() {
    this.stepZeroForm = this.fb.group({
      chronicle: [null, Validators.required],
      characterName: [null, [Validators.required,
            Validators.pattern(this.validatorService.firstNameRegex), Validators.maxLength(30)]],
      sect: [null, Validators.required]
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

  initializeStepThreeForm() {
    this.stepThreeForm = this.fb.group({
      physic: [null, [Validators.required]],
      social: [null, Validators.required],
      mental: [null, Validators.required],
      physicalFocus: [null, Validators.required],
      socialFocus: [null, Validators.required],
      mentalFocus: [null, Validators.required]
    }, { validators: [this.validityAttribut], updateOn: 'change' });
  }

  validityAttribut(g: FormGroup) {
    // tslint:disable-next-line: max-line-length
    const hasPrimaryAttribut = (g.get('physic').value === 7 || g.get('social').value === 7 || g.get('mental').value === 7);
    // tslint:disable-next-line: max-line-length
    const hasSecondaryAttribut = (g.get('physic').value === 5 || g.get('social').value === 5 || g.get('mental').value === 5);
    // tslint:disable-next-line: max-line-length
    const hasThridAttribut = (g.get('physic').value === 3 || g.get('social').value === 3 || g.get('mental').value === 3);

    if (hasPrimaryAttribut === false) {
      return ({ wrongAttribute: true });
    }
    if (hasSecondaryAttribut === false) {
      return ({ wrongAttribute: true });
    }
    if (hasThridAttribut === false) {
      return ({ wrongAttribute: true });
    }
    return null;
  }

  setPhysicalAttribut(score: number) {
    this.newCharacter.physicalAttribut = score;
    this.stepThreeForm.patchValue({ physic: score });
  }

  setSocialAttribut(score: number) {
    this.newCharacter.socialAttribut = score;
    this.stepThreeForm.patchValue({ social: score });
  }

  setMentalAttribut(score: number) {
    this.newCharacter.mentalAttribut = score;
    this.stepThreeForm.patchValue({ mental: score });
  }
}
