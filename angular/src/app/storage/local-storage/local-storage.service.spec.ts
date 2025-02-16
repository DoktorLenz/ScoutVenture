import { TestBed } from '@angular/core/testing';

import { Role } from 'src/app/auth/models/role.enum';
import { UserData } from 'src/app/auth/models/user-data';
import { LocalStorageKey } from './local-storage-key.enum';
import { LocalStorageService } from './local-storage.service';

describe('LocalStorageService', () => {
  let service: LocalStorageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LocalStorageService);
  });

  it('should get empty userData', () => {
    expect(service.userData).toEqual({});
  });

  it('should get userData', () => {
    const userData: UserData = {
      sub: '1234',
      given_name: 'Max',
      family_name: 'Musterman',
      name: 'Max Musterman',
      prefered_username: 'Maxl',
      authorities: [Role.VERIFIED],
    };

    spyOn(window.localStorage, 'getItem').and.callFake(() => {
      return JSON.stringify(userData);
    });

    expect(service.userData).toEqual(userData);
  });

  it('should save userData', () => {
    const userData: UserData = {
      sub: '1234',
      given_name: 'Max',
      family_name: 'Musterman',
      name: 'Max Musterman',
      prefered_username: 'Maxl',
      authorities: [Role.VERIFIED],
    };

    spyOn(window.localStorage, 'setItem').and.callFake(() => {});

    service.userData = userData;

    expect(window.localStorage.setItem).toHaveBeenCalledWith(
      'scoutventure.' + LocalStorageKey.USER_DATA,
      JSON.stringify(userData)
    );
  });
});
