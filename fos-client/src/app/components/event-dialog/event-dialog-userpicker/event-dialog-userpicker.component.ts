import {
  Component,
  OnInit,
  EventEmitter,
  Output,
  Input,
  forwardRef
} from "@angular/core";
import {
  FormControl,
  FormGroup,
  Validators,
  FormBuilder,
  AbstractControl,
  ValidatorFn,
  FormGroupDirective,
  NgForm,
  NG_VALUE_ACCESSOR
} from "@angular/forms";
import { debounceTime, tap, switchMap, finalize } from "rxjs/operators";
import { EventFormService } from "src/app/services/event-form/event-form.service";
import { Group } from "src/app/models/group";
import { environment } from "src/environments/environment";
import { User } from "src/app/models/user";
import { EventUser } from "src/app/models/eventuser";
import { debug } from "util";
import { DelegateHost } from "src/app/models/delegate-host";
import { DelegateHostService } from "src/app/services/delegate-host/delegate-host.service";
import { UserService } from "src/app/services/user/user.service";
export class UserPicker {
  Name: string;
  Email: string;
  Img: string;
  Id: string;
  IsGroup: number;
}

@Component({
  selector: "app-event-dialog-userpicker",
  templateUrl: "./event-dialog-userpicker.component.html",
  styleUrls: ["./event-dialog-userpicker.component.less"]
})
export class EventDialogUserpickerComponent implements OnInit {
  @Output() ListenChildComponentEvent = new EventEmitter<UserPicker>();
  @Input() formGroup: FormGroup;
  @Input() pickupTitle: string;
  @Input() formControlName: string;
  @Input() participantList: EventUser[];

  apiUrl = environment.apiUrl;
  isHostLoading = false;
  userHost: UserPicker[];
  showOption = false;
  displayUser(user: UserPicker) {
    if (user) {
      return user.Name;
    }
  }

  constructor(
    private eventFormService: EventFormService,
    private delegateHostService: DelegateHostService,
    private userService: UserService
  ) {}

  ngOnInit() {
    const self = this;

    self.formGroup
      .get(self.formControlName)
      .valueChanges.pipe(
        debounceTime(200),
        tap(() => (this.isHostLoading = true)),
        switchMap(value =>
          self.eventFormService.SearchGroupOrUserByName(value).pipe(
            finalize(() => {
              if (self.formGroup.get(self.formControlName).value !== "") {
                const user: UserPicker = self.formGroup.get(
                  self.formControlName
                ).value;
                if (user !== null && user.Email) {
                  this.ListenChildComponentEvent.emit(user);
                }
              } else {
                self.showOption = false;
              }
            })
          )
        )
      )
      .subscribe((data: ApiOperationResult<Array<Group>>) => {
        if (data.Data !== undefined && data.Data != null) {
          if (data.Data.length > 0) {
            self.showOption = false;
            const dataSourceTemp: UserPicker[] = [];
            data.Data.map(user => {
              self.userService.getCurrentUser().then(usr => {
                if (user.Mail === usr.Mail) {
                  const currentUser = new UserPicker();
                  currentUser.Email = user.Mail;
                  currentUser.Id = user.Id;
                  currentUser.Name = user.DisplayName;
                  dataSourceTemp.push(currentUser);
                  if (dataSourceTemp.length > 0) {
                    self.userHost = dataSourceTemp;
                    self.showOption = true;
                    self.isHostLoading = false;
                  }
                } else {
                  const checkUser = new User();
                  checkUser.Mail = user.Mail;
                  self.delegateHostService
                    .read(checkUser)
                    .then(rs => {
                      self.userService.getCurrentUser().then(current => {
                        const findUser = rs.Data.DelegateUser.find(
                          u => u.Mail === current.Mail
                        );
                        if (findUser !== undefined) {
                          if (user.DisplayName) {
                            dataSourceTemp.push({
                              Name: user.DisplayName,
                              Email: user.Mail,
                              Img: '',
                              Id: user.Id,
                              IsGroup: 0
                            });
                            if (dataSourceTemp.length > 0) {
                              self.userHost = dataSourceTemp;
                              self.showOption = true;
                              self.isHostLoading = false;
                            }
                          }
                        } else {
                          self.showOption = false;
                          self.isHostLoading = false;
                        }
                      });
                    })
                    .catch(err => {
                      self.isHostLoading = false;
                    });
                }
              });
            });
          } else {
            self.showOption = false;
            self.isHostLoading = false;
          }
        } else {
          self.showOption = false;
          self.isHostLoading = false;
        }
      });
  }
}
