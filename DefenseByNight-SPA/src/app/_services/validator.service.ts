import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ValidatorService {
  numberRegex = '^[0-9]*$';
  firstNameRegex = '((^|-)[a-zA-Z][a-zà-ü]+)+';
  lastNameRegex = '((^| )[a-zA-Z][a-zà-ü]+)+';
  cityRegex = '((^| )[a-zA-Z][a-zà-ü]+)+';

  constructor() { }

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

}
