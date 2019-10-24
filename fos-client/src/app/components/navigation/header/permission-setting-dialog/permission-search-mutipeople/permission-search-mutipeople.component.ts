import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import {
  map,
  startWith,
  debounceTime,
  tap,
  switchMap,
  finalize,
  elementAt
} from 'rxjs/operators';
import { EventFormService } from 'src/app/services/event-form/event-form.service';
import { Group } from 'src/app/models/group';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-permission-search-mutipeople',
  templateUrl: './permission-search-mutipeople.component.html',
  styleUrls: ['./permission-search-mutipeople.component.less']
})
export class PermissionSearchMutipeopleComponent implements OnInit {
  userControl = new FormControl();

  selectedUsers: Group[] = new Array<Group>();

  filteredUsers: Array<Group>;
  loading = false;
  tableDatasource: User[] = [];
  apiUrl = environment.apiUrl;

  @Output() ListenChildComponentEvent = new EventEmitter<Array<Group>>();

  constructor(private eventFormService: EventFormService, private userService: UserService) {
  }
  ngOnInit() {
    this.userControl.valueChanges
      .pipe(
        debounceTime(2000),
        tap(() => (this.loading = true)),
        switchMap(value =>
          this.eventFormService.SearchGroupOrUserByName(value).pipe(
            finalize(() => {
              this.loading = false;
            })
          )
        ),
      )
      .subscribe((data: ApiOperationResult<Array<Group>>) => {
        const filterList: Group[] = [];
        if ( data.Data !== undefined || data.Data.length > 0 ) {
          data.Data.forEach( u => {
            if (u.Mail) {
              const notExistUser: Group = this.selectedUsers.find(s => s.Mail === u.Mail);
              if (notExistUser) {
                notExistUser.IsSelected = true;
                filterList.push(notExistUser);
              } else {
                u.IsSelected = false;
                filterList.push(u);
                // this.userControl.setValue(null);
              }
            }
          });
        }
        this.filteredUsers = filterList;
      });
  }

  optionClicked(event: Event, user: Group) {
    event.stopPropagation();
    this.toggleSelection(user);
  }

  toggleSelection(user: Group) {
    if (user.IsSelected) {
      for ( let i = 0; i < this.selectedUsers.length; i++) {
        if ( this.selectedUsers[i].Mail === user.Mail) {
          this.selectedUsers.splice(i, 1);
          i--;
        }
    }
      this.userControl.setValue(null);
   } else {
     const checkUser: Group = this.selectedUsers.find( u => u.Mail === user.Mail);
     if (checkUser === undefined) {
      this.selectedUsers.push(user);
      this.ListenChildComponentEvent.emit(this.selectedUsers);
      this.userControl.setValue(null);
     }
    }
  }
  removeUser(user: Group) {
    for ( let i = 0; i < this.selectedUsers.length; i++) {
      if ( this.selectedUsers[i].Mail === user.Mail) {
        this.selectedUsers.splice(i, 1);
        i--;
      }
    }
  }
}
