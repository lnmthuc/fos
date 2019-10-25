import {
  Component,
  OnInit,
  Inject,
  Input,
  ViewChild,
  Output,
  EventEmitter
} from '@angular/core';
import { MAT_DIALOG_DATA, MatTable, MatSnackBar } from '@angular/material';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user/user.service';
import { debug } from 'util';
import { environment } from 'src/environments/environment';
import { Constant } from 'src/app/models/constant';
import { DelegateHostService } from 'src/app/services/delegate-host/delegate-host.service';
import { DelegateHost } from 'src/app/models/delegate-host';

@Component({
  selector: 'app-permisson-setting-usetable',
  templateUrl: './permisson-setting-usetable.component.html',
  styleUrls: ['./permisson-setting-usetable.component.less']
})
export class PermissonSettingUsetableComponent implements OnInit {
  @Output() ListenChildComponentEvent = new EventEmitter<Array<User>>();
  @Input() SettingType: string;
  @ViewChild(MatTable, { static: true }) table: MatTable<any>;
  tbSource: User[];
  displayedColumns = ['Avatar', 'Name', 'Email', 'Delete'];
  apiUrl = environment.apiUrl;
  loading = false;
  constructor(
    private userService: UserService,
    private snackBar: MatSnackBar,
    private delegateHostService: DelegateHostService
  ) {}

  ngOnInit() {
    const type = this.SettingType;
    const self = this;
    this.tbSource = [];
    if (type !== undefined && type === Constant.Permission) {
      const groupName = Constant.groupName;
      this.userService
        .SiteGroupListMemers(groupName)
        .toPromise()
        .then(u => {
          self.tbSource = u.Data;
        });
    } else if (type !== undefined && type === Constant.Delegate) {
      this.loading = true;
      this.userService.getCurrentUser().then(user => {
        this.delegateHostService.read(user).then(delegateHost => {
          if (delegateHost !== undefined && delegateHost.Data && delegateHost.Data.DelegateUser.length > 0) {
          self.tbSource = delegateHost.Data.DelegateUser;
          this.loading = false;
          }
        }).catch(err => {
          this.toast(err, 'Dismiss');
          this.loading = false;
        });
      });
    }
  }

  DeleteUser(user: User) {
    if (this.tbSource.length === 1) {
      this.toast('System must have at least one user', 'Dimiss');
    } else {
      for (let j = 0; j < this.tbSource.length; j++) {
        if (user.Mail === this.tbSource[j].Mail) {
          this.tbSource.splice(j, 1);
          j--;
          this.table.renderRows();
        }
      }
      this.ListenChildComponentEvent.emit(this.tbSource);
    }
  }
  notifyMessage(data: Array<User>) {
    this.addUserToTable(data);
  }
  addUserToTable(user: Array<User>) {
    if (this.tbSource !== null) {
      user.forEach(usr => {
        const newUser: User = this.tbSource.find(u => u.Mail === usr.Mail);
        if (newUser === undefined) {
          this.tbSource.push(usr);
        }
      });
    } else {
      user.forEach(usr => {
        this.tbSource.push(usr);
      });
    }
    this.ListenChildComponentEvent.emit(this.tbSource);
    this.table.renderRows();
  }
  toast(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 3000
    });
  }
}
