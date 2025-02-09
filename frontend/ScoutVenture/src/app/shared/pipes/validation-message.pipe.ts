import { Pipe, PipeTransform } from '@angular/core';
import { ValidationErrors } from '@angular/forms';

@Pipe({
  name: 'validationMessage',
})
export class ValidationMessagePipe implements PipeTransform {
  transform(errors: ValidationErrors | null, fieldName: string): string {
    const errorMessages = [];
    if (errors === null) {
      return '';
    }
    if (errors['required']) {
      errorMessages.push(`${fieldName} darf nicht leer sein`);
    }
    if (errors['email']) {
      errorMessages.push('Ungültige E-Mail-Adresse');
    }
    if (errors['minlength']) {
      errorMessages.push(
        `${fieldName} muss mindestens ${errors['minlength'].requiredLength} Zeichen lang sein`
      );
    }

    return errorMessages[0];
  }
}
