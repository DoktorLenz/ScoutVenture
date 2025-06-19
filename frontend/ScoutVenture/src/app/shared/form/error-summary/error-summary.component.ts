import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ValidationMessagePipe } from '../../pipes/validation-message.pipe';

@Component({
  selector: 'sv-error-summary',
  imports: [ValidationMessagePipe],
  templateUrl: './error-summary.component.html',
  styleUrl: './error-summary.component.scss',
})
export class ErrorSummaryComponent {
  @Input() group: FormGroup | null = null;
}
