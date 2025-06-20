import { Pipe, PipeTransform } from '@angular/core';
import { ValidationErrors } from '@angular/forms';

@Pipe({
  name: 'validationMessage',
})
export class ValidationMessagePipe implements PipeTransform {
  transform(errors: ValidationErrors | null | undefined): string {
    const errorMessages = [];
    if (errors == null) {
      return '';
    }
    if (errors['required']) {
      errorMessages.push(`Darf nicht leer sein`);
    }
    if (errors['email']) {
      errorMessages.push('Ungültige E-Mail-Adresse');
    }
    if (errors['minlength']) {
      errorMessages.push(
        `Muss mindestens ${errors['minlength'].requiredLength} Zeichen lang sein`
      );
    }
    if (errors['passwordMismatch']) {
      errorMessages.push('Passwörter stimmen nicht überein');
    }
    if (errors['DuplicateEmail']) {
      errorMessages.push('E-Mail-Adresse bereits vergeben');
    }
    if (errors['invalidCredentials']) {
      errorMessages.push('E-Mail-Adresse oder Passwort falsch');
    }
    if (errors['custom']) {
      errorMessages.push(errors['custom']);
    }

    return errorMessages[0];
  }
}
