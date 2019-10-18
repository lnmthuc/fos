import { Component, OnInit, ViewChild } from '@angular/core';
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
  constructor(private eventFormService: EventFormService) {}
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
        data.Data.forEach( u => {
          if (u.Mail) {
            const notExistUser: Group = this.selectedUsers.find(s => s.Mail === u.Mail);
            if (notExistUser) {
              notExistUser.IsSelected = true;
              filterList.push(notExistUser);
            } else {
              u.IsSelected = false;
              filterList.push(u);
            }
          }
        });
        this.filteredUsers = filterList;
        console.log('search done');
        if (this.userControl.value === '') {
          this.loading = false;
        }
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
    this.selectedUsers.push(user);
    this.userControl.setValue(null);
    }
  }
}
