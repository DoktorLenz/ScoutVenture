import { Route, Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { ConfirmEmailComponent } from './auth/confirm-email/confirm-email.component';
import { ForgotPasswordComponent } from './auth/forgot-password/forgot-password.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { ResetPasswordComponent } from './auth/reset-password/reset-password.component';
import { NamiComponent } from './main/administration/nami/nami.component';
import { SettingsComponent } from './main/administration/settings/settings.component';
import { UserManagementComponent } from './main/administration/user-management/user-management.component';
import { EventsOverviewComponent } from './main/events/events-overview/events-overview.component';
import { EventsRegistrationsComponent } from './main/events/events-registrations/events-registrations.component';
import { MainComponent } from './main/main.component';
import { PersonComponent } from './main/person/person.component';
import { ImprintComponent } from './public/imprint/imprint.component';
import { PrivacyPolicyComponent } from './public/privacy-policy/privacy-policy.component';
import { authGuard } from './shared/guards/auth.guard';

const authRoute: Route = {
  path: 'auth',
  component: AuthComponent,
  children: [
    {
      path: 'login',
      component: LoginComponent,
    },
    {
      path: 'register',
      component: RegisterComponent,
    },
    {
      path: 'confirmEmail',
      component: ConfirmEmailComponent,
    },
    {
      path: 'forgotPassword',
      component: ForgotPasswordComponent,
    },
    {
      path: 'resetPassword',
      component: ResetPasswordComponent,
    },
    {
      path: '**',
      redirectTo: 'login',
      pathMatch: 'full',
    },
  ],
};

const eventsRoutes: Route = {
  path: 'events',
  children: [
    {
      path: 'overview',
      component: EventsOverviewComponent,
      title: 'Veranstaltungen - Übersicht',
      data: { title: 'Veranstaltungen - Übersicht' },
    },
    {
      path: 'registrations',
      component: EventsRegistrationsComponent,
      title: 'Veranstaltungen - Meine Anmeldungen',
      data: { title: 'Veranstaltungen - Meine Anmeldungen' },
    },
    {
      path: '**',
      redirectTo: 'overview',
      pathMatch: 'full',
    },
  ],
};

const administrationRoutes: Route = {
  path: 'administration',
  children: [
    {
      path: 'user-management',
      component: UserManagementComponent,
      title: 'Administration - Nutzerverwaltung',
      data: { title: 'Administration - Nutzerverwaltung' },
    },
    {
      path: 'nami',
      component: NamiComponent,
      title: 'Administration - NaMi',
      data: { title: 'Administration - NaMi' },
    },
    {
      path: 'settings',
      component: SettingsComponent,
      title: 'Administration - Einstellungen',
      data: { title: 'Administration - Einstellungen' },
    },
  ],
};

export const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    canActivate: [authGuard],
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'events/overview',
      },
      eventsRoutes,
      {
        path: 'person/:id',
        component: PersonComponent,
        title: 'Meine Personen',
        data: { title: 'Meine Personen' },
      },
      administrationRoutes,
    ],
  },
  {
    path: 'privacy-policy',
    component: PrivacyPolicyComponent,
  },
  {
    path: 'imprint',
    component: ImprintComponent,
  },
  authRoute,
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full',
  },
];
