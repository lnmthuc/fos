import { Component, OnInit, Inject, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { MAT_DIALOG_DATA, MatTable } from '@angular/material';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user/user.service';
import { debug } from 'util';

@Component({
  selector: 'app-permisson-setting-usetable',
  templateUrl: './permisson-setting-usetable.component.html',
  styleUrls: ['./permisson-setting-usetable.component.less']
})
export class PermissonSettingUsetableComponent implements OnInit {
  @Output() ListenChildComponentEvent = new EventEmitter<Array<User>>();
  @ViewChild(MatTable, { static: true }) table: MatTable<any>;
  tbSource: User[];
  displayedColumns = ['Name', 'Email','Delete'];
  constructor(private userService: UserService) {
    const self = this;
    const groupName = 'FOS Owners';
    this.userService
      .SiteGroupListMemers(groupName)
      .toPromise()
      .then(u => {
        self.tbSource = u.Data;
      });
  }

  ngOnInit() {
  }

  DeleteUser( user: User) {
    for (let j = 0; j < this.tbSource.length; j++) {
      if (user.Mail === this.tbSource[j].Mail) {
        this.tbSource.splice(j, 1);
        j--;
        this.table.renderRows();
      }
    }
    this.ListenChildComponentEvent.emit(this.tbSource);
  }
  notifyMessage(data: Array<User>) {
    this.addUserToTable(data);
  }
  addUserToTable(user: Array<User>) {
    user.forEach( usr => {
      const newUser: User = this.tbSource.find(u => u.Mail === usr.Mail);
      if (newUser === undefined) {
        this.tbSource.push(usr);
      }
    });
    this.ListenChildComponentEvent.emit(this.tbSource);
    this.table.renderRows();
  }
}
