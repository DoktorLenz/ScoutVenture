import { Pipe, PipeTransform } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Pipe({
  name: 'errorTooltip',
})
export class ErrorTooltipPipe implements PipeTransform {

  transform(control: AbstractControl): string {
    const errorMessages = Object.entries(control.errors ?? [])
      .map(([property]) => this.getErrorMessage(property));

    return errorMessages.join(', ');
  }

  getErrorMessage(error: string): string {
    switch (error) {
      case 'required':
        return 'Erforderlich';
      case 'email':
        return 'Ungültige E-Mail Adresse';
      default:
        return `unknown error "${error}"`;
    }
  }

}
