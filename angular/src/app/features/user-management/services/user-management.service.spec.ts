import { provideHttpClient } from '@angular/common/http';
import {
  HttpTestingController,
  provideHttpClientTesting,
} from '@angular/common/http/testing';
import { MockBuilder, ngMocks } from 'ng-mocks';
import { Gender } from '../../../lib/models/gender.enum';
import { Rank } from '../../../lib/models/rank.enum';
import { NamiFetchDetails } from '../models/nami-fetch-details';
import { NamiMember } from '../models/nami-member';
import { UserManagementService } from './user-management.service';

describe('UserManagementService', () => {
  beforeEach(() =>
    MockBuilder(UserManagementService)
      .provide(provideHttpClient())
      .provide(provideHttpClientTesting())
  );

  it('should be created', () => {
    const service = ngMocks.findInstance(UserManagementService);

    expect(service).toBeTruthy();
  });

  it('should read all nami members from api', () => {
    const service = ngMocks.findInstance(UserManagementService);
    const httpMock = ngMocks.findInstance(HttpTestingController);
    const data: NamiMember[] = [
      {
        firstname: 'Maximilian',
        lastname: 'Musterman',
        gender: Gender.MALE,
        memberId: 0,
        rank: Rank.ROVER,
        userAssignments: [],
      },
    ];

    let actual: NamiMember[] = [];
    service.getNamiMembers().subscribe(value => (actual = value));

    const req = httpMock.expectOne('/api/v1/usermanagement/nami-members');

    expect(req.request.method).toEqual('GET');
    req.flush(data);
    httpMock.verify();

    expect(actual).toEqual(data);
  });

  it('should send a new fetch job to the api', () => {
    const service = ngMocks.findInstance(UserManagementService);
    const httpMock = ngMocks.findInstance(HttpTestingController);

    const namiFetchDetails: NamiFetchDetails = {
      groupingId: '1234',
      password: 'testingpassword',
      username: '1234',
    };

    service.fetchNamiMembers(namiFetchDetails).subscribe();

    const req = httpMock.expectOne('/api/v1/usermanagement/trigger-import');

    expect(req.request.method).toEqual('POST');
    expect(req.request.body).toEqual(namiFetchDetails);
  });
});
