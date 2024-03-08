import { Component, OnInit } from '@angular/core';
import { Salary } from 'src/app/shared/model/Salary';
import { SalaryService } from 'src/app/shared/salary/salary.service';
import html2canvas from 'html2canvas';
import { jsPDF } from 'jspdf';

declare var require: any;

import * as pdfMake from 'pdfmake/build/pdfmake';
import * as pdfFonts from 'pdfmake/build/vfs_fonts';
const htmlToPdfmake = require('html-to-pdfmake');
(pdfMake as any).vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-salary-slip',
  templateUrl: './salary-slip.component.html',
  styleUrls: ['./salary-slip.component.scss'],
})
export class SalarySlipComponent implements OnInit {
  constructor(private salaryService: SalaryService) {}

  salaryData: Salary;
  taxPercent:string;

  ngOnInit(): void {
    this.salaryData = this.salaryService.transferData();
    this.taxPercent=((this.salaryData.taxCut*100)/(this.salaryData.taxCut+this.salaryData.creditAmount)).toFixed(2);
    if(this.taxPercent>'30')this.taxPercent='30';
    console.log(this.taxPercent);
    console.log(this.salaryData);
    
  }

  downloadAsPDF() {
    const doc = new jsPDF();
    const element = document.querySelector('#pdfTable') as HTMLElement;

    html2canvas(element, { scale: 2 }) 
      .then(canvas => {
        const imgData = canvas.toDataURL('image/png');
        const imgWidth = doc.internal.pageSize.getWidth();
        const imgHeight = canvas.height * imgWidth / canvas.width;
        doc.addImage(imgData, 'PNG', 0, 0, imgWidth, imgHeight);
        doc.save('my-pdf.pdf');
      });
  }

}
