import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './component/home/home.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'authentication',
    pathMatch: 'full',
  },
  {
    path: 'authentication',
    loadChildren: () =>
      import('./authentication/authentication.module').then(
        (m) => m.AuthenticationModule
      ),
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'manageEmployees',
    loadChildren: () =>
      import('./manage-emplyee/manage-emplyee.module').then(
        (m) => m.ManageEmplyeeModule
      ),
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
  },
  { path: 'salary', loadChildren: () => import('./salary/salary.module').then(m => m.SalaryModule) },
  { path: 'checkin', loadChildren: () => import('./check-in/check-in.module').then(m => m.CheckInModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
