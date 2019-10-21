import { Component, OnInit } from '@angular/core';
import { concat, Observable, of, Subject } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';
import { MatDialogRef, MatSnackBar } from '@angular/material';
import { UserService } from 'src/app/services/user/user.service';
import { group } from '@angular/animations';
import { User } from 'src/app/models/user';
import { Group } from 'src/app/models/group';
@Component({
  selector: 'app-permission-setting-dialog',
  templateUrl: './permission-setting-dialog.component.html',
  styleUrls: ['./permission-setting-dialog.component.less']
})
export class PermissionSettingDialogComponent implements OnInit {
  ngOnInit() {
  }
  constructor(public dialogRef: MatDialogRef<PermissionSettingDialogComponent>,
              private userService: UserService, private snackBar: MatSnackBar) {
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  selectedUser: User[];
  loading: boolean = false;
  getMemberAdminGroup() {
    var self = this;
    self.loading = true;
    // const groupName = 'FOS Owners';
    // this.userService.SiteGroupListMemers(groupName).toPromise().then(
    //   // tslint:disable-next-line: no-shadowed-variable
    //   group => {
    //     debugger;
    //     if (group.Data !== null) {
    //     group.Data.forEach(element => {
    //       debugger;
          
    //     });
    //     }
    //   });
    // const newUser: User = {
    //   Mail: 'hau.nguyen.cong@preciofishbone.se',
    //   DisplayName: '',
    //   GivenName: '',
    //   Id: '',
    //   JobTitle: '',
    //   LoginName: '',
    //   MobilePhone: '',
    //   OfficeLocation: '',
    //   PreferredLanguage: '',
    //   Surname: '',
    //   UserPrincipalName: '',
    // };
    const promises: Array<Promise<void>> = [];
    this.selectedUser.forEach(user => {
      let pro = this.userService.SiteGroupAddMembers(user).toPromise().then(rs =>{

      });
      promises.push(pro);
    });
    Promise.all(promises).then(() => {
      this.loading = false;
      this.toast('Add user to admin group success', 'Dismiss');
      this.dialogRef.close();
    });
  }

  notifyMessage(data: any) {
    console.log(data);
    this.selectedUser = data;
  }

  toast(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000
    });
  }
}
