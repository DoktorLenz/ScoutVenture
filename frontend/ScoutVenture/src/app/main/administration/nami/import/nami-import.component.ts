import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ErrorWrapperComponent } from '../../../../shared/form/error-wrapper/error-wrapper.component';

@Component({
  selector: 'sv-nami-import',
  imports: [
    ErrorWrapperComponent,
    CardModule,
    InputTextModule,
    PasswordModule,
    ReactiveFormsModule,
    ButtonModule,
  ],
  templateUrl: './nami-import.component.html',
  styleUrl: './nami-import.component.scss',
})
export class NamiImportComponent {
  protected importForm = new FormGroup({
    groupId: new FormControl<string>('', {
      validators: [Validators.required],
      updateOn: 'blur',
    }),
    memberId: new FormControl<string>('', {
      validators: [Validators.required],
      updateOn: 'blur',
    }),
    password: new FormControl<string>('', {
      validators: [Validators.required],
      updateOn: 'blur',
    }),
  });

  protected import() {
    if (this.importForm.valid) {
    } else {
      this.importForm.markAsDirty();
      this.importForm.controls.groupId.markAsDirty();
      this.importForm.controls.memberId.markAsDirty();
      this.importForm.controls.password.markAsDirty();
    }
  }
}
