import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { MatDialogRef } from '@angular/material';
import { Constant } from 'src/app/models/constant';

@Component({
  selector: 'app-delegate-setting-dialog',
  templateUrl: './delegate-setting-dialog.component.html',
  styleUrls: ['./delegate-setting-dialog.component.less']
})
export class DelegateSettingDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DelegateSettingDialogComponent>) { }
  SettingType = Constant.Delegate;
  ngOnInit() {
  }
  notifyMessage(data: Array<User>) {
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
