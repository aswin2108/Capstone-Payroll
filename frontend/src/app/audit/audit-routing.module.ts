import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuditComponent } from './audit.component';
import { loginGuardGuard } from '../login-guard.guard';

const routes: Routes = [{ path: '', component: AuditComponent ,canActivate: [loginGuardGuard],}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuditRoutingModule { }
