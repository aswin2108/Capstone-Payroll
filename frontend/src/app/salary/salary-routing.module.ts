import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SalaryComponent } from './salary.component';
import { SalarySlipComponent } from './pages/salary-slip/salary-slip.component';

const routes: Routes = [
  {
    path: '',
    component: SalaryComponent,
  },
  {
    path:'print',
    component: SalarySlipComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SalaryRoutingModule {}
