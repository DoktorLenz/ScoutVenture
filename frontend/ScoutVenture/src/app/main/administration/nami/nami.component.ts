import { Component } from '@angular/core';

import { FormControl, FormGroup } from '@angular/forms';
import { Validators } from '../../../shared/form/Validators';
import { NamiImportComponent } from './components/import/nami-import.component';
import { NamiOverviewComponent } from './components/overview/nami-overview.component';

@Component({
  selector: 'sv-nami',
  imports: [NamiOverviewComponent, NamiImportComponent],
  templateUrl: './nami.component.html',
  styleUrl: './nami.component.scss',
})
export class NamiComponent {
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
