import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class NamiService {
  importPending = signal(false);

  constructor(private readonly http: HttpClient) {}
}
