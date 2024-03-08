import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AnalysisRoutingModule } from './analysis-routing.module';
import { AnalysisComponent } from './analysis.component';
import { UserTableAnalysisComponent } from './pages/user-table-analysis/user-table-analysis.component';
import { ChartModule } from 'primeng/chart';

@NgModule({
  declarations: [
    AnalysisComponent,
    UserTableAnalysisComponent,
  ],
  imports: [
    CommonModule,
    AnalysisRoutingModule,
    ChartModule,
  ]
})
export class AnalysisModule { }
