import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SalaryRoutingModule } from './salary-routing.module';
import { SalaryComponent } from './salary.component';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';
import { TooltipModule } from 'primeng/tooltip';
import { SalarySlipComponent } from './pages/salary-slip/salary-slip.component';


@NgModule({
  declarations: [
    SalaryComponent,
    SalarySlipComponent
  ],
  imports: [
    CommonModule,
    SalaryRoutingModule,
    TableModule,
    ButtonModule,
    InputTextModule,
    RippleModule,
    TooltipModule 
  ]
})
export class SalaryModule {
  

}
