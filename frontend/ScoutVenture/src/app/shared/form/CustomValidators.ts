import { ValidatorFn } from '@angular/forms';

export class CustomValidators {
  static repeats(passwordKey: string, repeatPasswordKey: string): ValidatorFn {
    return (group) => {
      const password = group.get(passwordKey)?.value;
      const repeatPassword = group.get(repeatPasswordKey)?.value;
      return password === repeatPassword ? null : { repeats: true };
    };
  }
}
