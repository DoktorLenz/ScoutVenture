import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, map, of } from 'rxjs';

export interface UserInfo {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  roles: string[];
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userInfoSubject = new BehaviorSubject<UserInfo | null>(null);
  public userInfo$ = this.userInfoSubject.asObservable();

  constructor(private http: HttpClient) {}

  public loadUserInfo(): Observable<UserInfo | null> {
    return this.http.get<UserInfo>('/api/me/info').pipe(
      map(userInfo => {
        this.userInfoSubject.next(userInfo);
        return userInfo;
      }),
      catchError(() => {
        this.userInfoSubject.next(null);
        return of(null);
      })
    );
  }

  public getCurrentUser(): UserInfo | null {
    return this.userInfoSubject.value;
  }

  public isLoggedIn(): boolean {
    return this.userInfoSubject.value !== null;
  }

  public hasRole(role: string): boolean {
    const user = this.getCurrentUser();
    return user?.roles?.includes(role) ?? false;
  }

  public isAdmin(): boolean {
    return this.hasRole('Admin');
  }

  public isCounselor(): boolean {
    return this.hasRole('Counselor');
  }

  public isMember(): boolean {
    return this.hasRole('Member');
  }

  public clearUserInfo(): void {
    this.userInfoSubject.next(null);
  }
}