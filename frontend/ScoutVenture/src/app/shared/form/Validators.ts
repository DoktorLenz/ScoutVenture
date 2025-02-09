import { Validators as AngularValidators, ValidatorFn } from '@angular/forms';

export class Validators {
  static required = AngularValidators.required;

  static minLength(minLength: number): ValidatorFn {
    return (control) => {
      if (control.value.length < minLength)
        return {
          minlength: {
            requiredLength: minLength,
            actualLength: control.value.legnth,
          },
        };
      return null;
    };
  }

  static maxLength = AngularValidators.maxLength;

  static email(): ValidatorFn {
    return (control) => {
      if (control.value == null || control.value === '') {
        return { email: true };
      }
      return AngularValidators.email(control);
    };
  }

  static passwordMatch(
    passwordControlName: string,
    confirmPasswordControlName: string
  ): ValidatorFn {
    return (formGroup) => {
      const password = formGroup.get(passwordControlName)?.value;
      const confirmPassword = formGroup.get(confirmPasswordControlName)?.value;

      if (password !== confirmPassword || password === '') {
        formGroup
          .get(confirmPasswordControlName)
          ?.setErrors({ passwordMismatch: true });
      } else {
        formGroup.get(confirmPasswordControlName)?.setErrors(null);
      }
      return null;
    };
  }
}
