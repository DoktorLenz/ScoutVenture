import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NamiMembersComponent } from './nami-members/nami-members.component';
import { OverviewComponent } from './overview/overview.component';
import { UserManagementRoute } from 'src/app/lib/routes/user-management-route.enum';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'overview',
    pathMatch: 'full',
  },
  {
    path: UserManagementRoute.OVERVIEW,
    component: OverviewComponent,
  },
  {
    path: UserManagementRoute.NAMI_MEMBERS,
    component: NamiMembersComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserManagementRoutingModule { }
