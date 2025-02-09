import { CommonModule } from '@angular/common';
import { Component, ContentChild, Input, TemplateRef } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ValidationMessagePipe } from '../../pipes/validation-message.pipe';

@Component({
  selector: 'sv-error-wrapper',
  imports: [CommonModule, ValidationMessagePipe],
  templateUrl: './error-wrapper.component.html',
  styleUrl: './error-wrapper.component.scss',
})
export class ErrorWrapperComponent {
  @Input() control: FormControl | null = null;

  @ContentChild('input', { static: false }) input: TemplateRef<any> | null =
    null;

  @ContentChild('title', { static: false }) title: TemplateRef<any> | null =
    null;

  @ContentChild('errorMessage', { static: false })
  errorMessage: TemplateRef<any> | null = null;

  ngAfterContentInit() {
    console.log(this.errorMessage);
  }
}
