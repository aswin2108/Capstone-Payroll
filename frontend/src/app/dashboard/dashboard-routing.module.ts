import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { loginGuardGuard } from '../login-guard.guard';

const routes: Routes = [{ path: '', component: DashboardComponent ,canActivate: [loginGuardGuard], children:[{
  path:'',
  component: ProfileComponent,
  canActivate: [loginGuardGuard],
}] }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
