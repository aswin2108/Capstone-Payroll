import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ChartDataset, ChartOptions, ChartConfiguration } from 'chart.js';
import { PrimeNGConfig } from 'primeng/api';
import { DisplayEmployeeService } from 'src/app/shared/displayEmployee/display-employee.service';
import { EmployeeDetails } from 'src/app/shared/model/EmployeeDetails';

@Component({
  selector: 'app-user-table-analysis',
  templateUrl: './user-table-analysis.component.html',
  styleUrls: ['./user-table-analysis.component.scss'],
})
export class UserTableAnalysisComponent implements OnInit {
  constructor(
    private displayService: DisplayEmployeeService,
    private primengConfig: PrimeNGConfig
  ) {}

  allEmployee: EmployeeDetails[] = [];

  getEmpData() {
    this.displayService.getEmpData().subscribe({
      next: (response) => {
        this.allEmployee = response;

        this.allEmployee.forEach((emp) => {
          const totCost = emp.bonus + emp.overTime * 700 + emp.salary;
          if (emp.payFreq === 1) {
            this.payTrend[1] += totCost;
            this.payTrend[2] += totCost;
            this.payTrend[3] += totCost;
            this.payTrend[4] += totCost;
          } else if (emp.payFreq === 2) {
            this.payTrend[2] += totCost;
            this.payTrend[4] += totCost;
          } else {
            this.payTrend[4] += totCost;
          }
        });
      },
      error: (error: HttpErrorResponse) => {
        console.log(error);
      },
    });
  }

  payTrend: number[] = [0, 0, 0, 0, 0, 0];

  data: any;

  options: any;

  ngOnInit() {
    this.primengConfig.ripple = true;
    this.getEmpData();
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const textColorSecondary = documentStyle.getPropertyValue(
      '--text-color-secondary'
    );
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

    this.data = {
      labels: ['Day-0', 'Day-7', 'Day-14', 'Day-21', 'Month End'],
      datasets: [
        {
          label: 'Payroll Trend',
          data: this.payTrend,
          fill: true,
          tension: 0.4,
          borderColor: documentStyle.getPropertyValue('--blue-500'),
          backgroundColor: 'rgba(0, 150, 255,0.2)',
        },
      ],
    };

    this.options = {
      maintainAspectRatio: false,
      aspectRatio: 0.6,
      plugins: {
        legend: {
          labels: {
            color: textColor,
          },
        },
      },
      scales: {
        x: {
          ticks: {
            color: textColorSecondary,
          },
          grid: {
            color: surfaceBorder,
          },
        },
        y: {
          ticks: {
            color: textColorSecondary,
          },
          grid: {
            color: surfaceBorder,
          },
        },
      },
    };
  }
}
