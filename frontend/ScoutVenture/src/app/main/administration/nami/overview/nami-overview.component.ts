import { HttpClient } from '@angular/common/http';
import { Component, effect } from '@angular/core';
import { SkeletonModule } from 'primeng/skeleton';
import { NamiService } from '../nami.service';

@Component({
  selector: 'sv-nami-overview',
  imports: [SkeletonModule],
  templateUrl: './nami-overview.component.html',
  styleUrl: './nami-overview.component.scss',
})
export class NamiOverviewComponent {
  protected woelfingeCount: number | null = null;
  protected jungpfadfinderCount: number | null = null;
  protected pfadiCount: number | null = null;
  protected roverCount: number | null = null;
  protected noneCount: number | null = null;

  constructor(
    private readonly http: HttpClient,
    private readonly namiService: NamiService
  ) {
    effect(() => {
      if (this.namiService.importPending()) {
        this.woelfingeCount = null;
        this.jungpfadfinderCount = null;
        this.pfadiCount = null;
        this.roverCount = null;
        this.noneCount = null;
      } else {
        this.updateOverview();
      }
    });
  }

  private updateOverview() {
    this.http.get('/api/administration/nami/overview').subscribe({
      next: (data: any) => {
        // Assuming the API returns an object with counts
        this.woelfingeCount = data.woelflingCount || 0;
        this.jungpfadfinderCount = data.jungpfadfinderCount || 0;
        this.pfadiCount = data.pfadiCount || 0;
        this.roverCount = data.roverCount || 0;
        this.noneCount = data.noneCount || 0;
      },
      error: (err) => {
        console.error('Error fetching Nami overview:', err);
      },
    });
  }
}
