import { Component, OnInit } from "@angular/core";
import { concat, Observable, of, Subject } from "rxjs";
import {
  catchError,
  debounceTime,
  distinctUntilChanged,
  switchMap,
  tap,
  elementAt
} from "rxjs/operators";
import { MatDialogRef, MatSnackBar } from "@angular/material";
import { UserService } from "src/app/services/user/user.service";
import { group } from "@angular/animations";
import { User } from "src/app/models/user";
import { Group } from "src/app/models/group";
import { element } from 'protractor';
import { debug } from 'util';
@Component({
  selector: "app-permission-setting-dialog",
  templateUrl: "./permission-setting-dialog.component.html",
  styleUrls: ["./permission-setting-dialog.component.less"]
})
export class PermissionSettingDialogComponent implements OnInit {
  tableSource: User[] = [];
  ngOnInit() {}
  constructor(
    public dialogRef: MatDialogRef<PermissionSettingDialogComponent>,
    private userService: UserService,
    private snackBar: MatSnackBar
  ) {
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  selectedUser: User[];
  loading: boolean = false;

  getMemberAdminGroup() {
    this.loading = true;
    const promises: Array<Promise<void>> = [];
    const groupName = 'FOS Owners';
    let currentUser: Array<User> = [];
    const deleteUser: Array<User> = [];
    const newUser: Array<User> = [];
    const promise = this.userService.SiteGroupListMemers(groupName).toPromise().then(
      user => {
        currentUser = user.Data;
      }
    );
    promises.push(promise);
    // const self = this;
    // self.loading = true;
    // this.selectedUser.forEach(user => {
    //   let pro = this.userService
    //     .SiteGroupAddMembers(user)
    //     .toPromise()
    //     .then(rs => {
    //       if (rs.Success === false) {
    //         this.toast(String(rs.ErrorMessage), "Dismiss");
    //       } else {
    //         this.toast("Add user to admin group done", "Dismiss");
    //       }
    //     });
    //   promises.push(pro);
    // });
    Promise.all(promises)
      .then(result => {
        debugger;
        currentUser.forEach(element => {
          const user: User = this.selectedUser.find(u => u.Mail === element.Mail);
          if (user === undefined) {
            deleteUser.push(element);
          }
        });
        this.selectedUser.forEach(sUser => {
          const checkNewUser = currentUser.find(usr => usr.Mail === sUser.Mail);
          if (checkNewUser === undefined) {
            newUser.push(sUser);
          }
        });
      })
      .catch(reason => {
        this.toast(reason, 'Dismiss');
      })
      .finally(() => {
        const listPromise: Array<Promise<void>> = [];
        deleteUser.forEach((u: User) => {
          let removePrommise = this.userService.SiteGroupRemoveMembers(u).toPromise().then(() => {

          });
          listPromise.push(removePrommise);
        });
        newUser.forEach((u: User) => {
          let newPromise = this.userService.SiteGroupAddMembers(u).toPromise().then(() => {
          });
          listPromise.push(newPromise);
        });
        Promise.all(listPromise).then(() => {
          this.loading = false;
          this.toast('Update admin role finished', "Dismiss");
          this.dialogRef.close();
        });
      });
  }

  notifyMessage(data: any) {
    console.log(data);
    this.selectedUser = data;
  }

  toast(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 3000
    });
  }
}
