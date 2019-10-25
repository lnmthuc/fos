import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { MatDialogRef, MatSnackBar } from '@angular/material';
import { Constant } from 'src/app/models/constant';
import { UserService } from 'src/app/services/user/user.service';
import { DelegateHost } from 'src/app/models/delegate-host';
import { DelegateHostService } from 'src/app/services/delegate-host/delegate-host.service';

@Component({
  selector: 'app-delegate-setting-dialog',
  templateUrl: './delegate-setting-dialog.component.html',
  styleUrls: ['./delegate-setting-dialog.component.less']
})
export class DelegateSettingDialogComponent implements OnInit {
  selectedUser: Array<User> = [];
  constructor(public dialogRef: MatDialogRef<DelegateSettingDialogComponent>,
              private userService: UserService,
              private delegateHostService: DelegateHostService,
              private snackBar: MatSnackBar
              ) { }
  SettingType = Constant.Delegate;
  loading = false;
  ngOnInit() {
  }
  notifyMessage(data: Array<User>) {
    this.selectedUser = data;
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  onUpdateClick(): void {
    const self = this;
    self.loading = true;
    this.userService.getCurrentUser().then( user => {
      const delegateInfo = new DelegateHost();
      delegateInfo.Mail = user.Mail;
      delegateInfo.DelegateUser = this.selectedUser;
      self.delegateHostService.create(delegateInfo).then( () => {
        self.loading = false;
        self.toast('Update delegate people successfully', 'Dismiss');
        this.dialogRef.close();
      }
      );
    });
  }
  toast(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 3000
    });
  }
}
