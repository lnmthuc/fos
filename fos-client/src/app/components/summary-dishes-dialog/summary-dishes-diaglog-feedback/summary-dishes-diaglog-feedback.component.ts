import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FeedbackService } from 'src/app/services/feedback/feedback.service';
import { UserService } from 'src/app/services/user/user.service';
import { environment } from 'src/environments/environment';
import { DishesSummary } from 'src/app/models/dishes-summary';

@Component({
  selector: 'app-summary-dishes-diaglog-feedback',
  templateUrl: './summary-dishes-diaglog-feedback.component.html',
  styleUrls: ['./summary-dishes-diaglog-feedback.component.less']
})
export class SummaryDishesDiaglogFeedbackComponent implements OnInit {
  constructor(public dialogRef: MatDialogRef<SummaryDishesDiaglogFeedbackComponent>,
              @Inject(MAT_DIALOG_DATA) public foodDetail: DishesSummary,
              private feedBackService: FeedbackService,
              private userService: UserService) { }
  private foodName = this.foodDetail.Food;
  private chatAlignRight = false;
  private chatAlignLeft = true;
  loading: boolean;
  apiUrl = environment.apiUrl;
  public messages = [
    // {
    //   // face : this.imagePath,
    //   what: 'Good',
    //   who: 'User1',
    //   when: '3:08PM',
    //   notes: " I'll be in your neighborhood doing errands",
    //   chatAlignLeft: true
    // },
    // {
    //   // face : this.imagePath,
    //   what: 'Very good',
    //   who: 'User2',
    //   when: '3:08PM',
    //   notes: " I'll be in your neighborhood doing errands",
    //   chatAlignLeft: false
    // },
    // {
    //   // face : this.imagePath,
    //   what: 'Not bad',
    //   who: 'User3',
    //   when: '3:08PM',
    //   notes: " I'll be in your neighborhood doing errands",
    //   chatAlignLeft: true
    // },
    // {
    //   // face : this.imagePath,
    //   what: 'Not bad',
    //   who: 'User3',
    //   when: '3:08PM',
    //   notes: " I'll be in your neighborhood doing errands",
    //   chatAlignLeft: false
    // }
  ];
  ngOnInit() {
    const self = this;
    self.loading = true;
    const foodId = this.foodDetail.FoodId;
    this.feedBackService.GetByFoodId(foodId.toString()).then(
     value => {
       if (value.length === 0) {
        self.loading = false;
       }
       value.forEach(element => {
        element.FoodFeedbacks.forEach(ff => {
          if ( ff.FoodId === foodId.toString()) {
            ff.UserFeedBacks.forEach(user => {
              self.userService.getUserById(user.UserId).then( u => {
                console.log(u.Mail, user.Comment);
                self.messages.push(
                {
                  what: user.Comment,
                  who: u.DisplayName,
                  chatAlignLeft: true,
                  notes: '',
                  when: null,
                  Name: u.DisplayName,
                  Id: u.Id
                }
                );
                self.loading = false;
              });
            });
          }
        });
      });
     }
    ).catch( err => {

    })
    .finally( () => {
    });
  }
  closeDialog($event) {
    this.dialogRef.close();
  }
}
