import { Component } from '@angular/core';
import { SkeletonModule } from 'primeng/skeleton';
import { delay, of } from 'rxjs';

@Component({
  selector: 'sv-nami-overview',
  imports: [SkeletonModule],
  templateUrl: './nami-overview.component.html',
  styleUrl: './nami-overview.component.scss',
})
export class NamiOverviewComponent {
  protected woelfingeCount: number | null = null;
  protected jungpfadfinderCount: number | null = null;
  protected pfadfinderCount: number | null = null;
  protected roverCount: number | null = null;

  constructor() {
    of([])
      .pipe(delay(3000))
      .subscribe({
        next: () => {
          // Simulate fetching data
          this.woelfingeCount = 10;
          this.jungpfadfinderCount = 15;
          this.pfadfinderCount = 20;
          this.roverCount = 5;
        },
        error: (err) => {
          console.error('Error fetching Nami data:', err);
        },
      });
  }
}
