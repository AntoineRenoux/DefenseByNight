import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ValidatorService {
  phoneNumberRegex = '(0|\\+33)[1-9](( |\\.)*[0-9]{2}){4}';
  firstNameRegex = '^[a-zA-Z](( |-)?[a-zA-Zà-ü]*){2,20}';
  lastNameRegex = '^[a-zA-Z](( |-)?[a-zA-Zà-ü]*){2,20}';
  cityRegex = '^[a-zA-Z](( |-)?[a-zA-Z]*)+';
  zipCodeRegex = '[0-9]{5}';
  emailRegex = '([a-z0-9])*((\\.|_)*[a-z0-9])+@[a-z]+\\.[a-z]{2,3}';

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
