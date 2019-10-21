import { Component, OnInit } from '@angular/core';
import { concat, Observable, of, Subject } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';
import { MatDialogRef } from '@angular/material';
@Component({
  selector: 'app-permission-setting-dialog',
  templateUrl: './permission-setting-dialog.component.html',
  styleUrls: ['./permission-setting-dialog.component.less']
})
export class PermissionSettingDialogComponent implements OnInit {
  ngOnInit() {
  }
  constructor(public dialogRef: MatDialogRef<PermissionSettingDialogComponent>) {
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
