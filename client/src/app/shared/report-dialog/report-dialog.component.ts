import { Component, Inject, OnInit } from '@angular/core';
import { BattleReportDto } from 'src/app/_models/BattleReportDto/BattleReportDto';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-report-dialog',
  templateUrl: './report-dialog.component.html',
  styleUrls: ['./report-dialog.component.scss']
})
export class ReportDialogComponent implements OnInit {

  reportData:any;
  constructor(public dialogRef: MatDialogRef<ReportDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.reportData = data;
   }

  ngOnInit(): void {
    console.log("oninitdata");
    console.log(this.reportData);
  }

}
