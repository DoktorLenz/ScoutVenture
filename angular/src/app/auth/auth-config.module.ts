import { HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import {
  AuthInterceptor,
  AuthModule,
  StsConfigHttpLoader,
  StsConfigLoader,
} from 'angular-auth-oidc-client';
import { map } from 'rxjs';
import { AuthRoute } from '../lib/routes/auth-route.enum';
import { BaseRoute } from '../lib/routes/base-route.enum';
import { Configuration } from './models/configuration';
import * as fromAuth from './reducers';

export const httpLoaderFactory = (httpClient: HttpClient) => {
  const config$ = httpClient.get<Configuration>('/api/v1/configuration').pipe(
    map((config: Configuration) => ({
      authority: config.oauth2Configuration.authority,
      redirectUrl: `${window.location.origin}/${BaseRoute.AUTH}/${AuthRoute.CALLBACK}`,
      postLogoutRedirectUri: window.location.origin,
      clientId: config.oauth2Configuration.clientId,
      secureRoutes: ['/api'],
      scope: 'openid profile offline_access',
      responseType: 'code',
      silentRenew: true,
      useRefreshToken: true,
      renewTimeBeforeTokenExpiresInSeconds: 30,
    }))
  );

  return new StsConfigHttpLoader(config$);
};

@NgModule({
  imports: [
    AuthModule.forRoot({
      loader: {
        provide: StsConfigLoader,
        useFactory: httpLoaderFactory,
        deps: [HttpClient],
      },
    }),
    StoreModule.forFeature(fromAuth.authFeature),
  ],
  exports: [AuthModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
})
export class AuthConfigModule {}
