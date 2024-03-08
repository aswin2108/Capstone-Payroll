import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuditRoutingModule } from './audit-routing.module';
import { AuditComponent } from './audit.component';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';


@NgModule({
  declarations: [
    AuditComponent
  ],
  imports: [
    CommonModule,
    AuditRoutingModule,
    TableModule,
    TagModule,
    ButtonModule,
    InputTextModule,
  ]
})
export class AuditModule { }
