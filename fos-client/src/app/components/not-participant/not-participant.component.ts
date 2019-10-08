import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { OrderService } from "src/app/services/order/order.service";
import { EventFormService } from "src/app/services/event-form/event-form.service";
import { environment } from "src/environments/environment";
import { MatSnackBar } from '@angular/material';
@Component({
  selector: "app-not-participant",
  templateUrl: "./not-participant.component.html",
  styleUrls: ["./not-participant.component.less"]
})
export class NotParticipantComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private orderService: OrderService,
    private eventService: EventFormService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    const guid = this.route.snapshot.paramMap.get('id');
    this.orderService.GetOrder(guid).then(o => {
      const eventId = o.IdEvent;
      if ( o.OrderStatus === 2 ) {
        this.edited = true;
      } else {
        this.edited = false;
      }
      this.eventService.GetEventById(eventId).then(e => {
        const eventStatus = e.Status;
        if (eventStatus === 'Closed') {
          this.router.navigateByUrl('events');
        } else {
        }
      });
    });
  }
  public edited = true;
  ngOnInit() {
  }
  clickYes() {
    const guid = this.route.snapshot.paramMap.get('id');
    this.orderService.UpdateOrderStatusByOrderId(guid, 2).then(value => {
      this.orderService.UpdateFoodDetailByOrderId(guid, '{}').then(rs => {
        this.toast('Update status success', 'Dismiss');
        this.router.navigateByUrl('events');
      });
    });
  }
  clickNo() {
    const guid = this.route.snapshot.paramMap.get('id');
    this.router.navigate(['/make-order', guid]).then( (e) => {
      if (e) {
        // console.log('Navigation is successful!');
      } else {
        // console.log('Navigation has failed!');
      }
    });
  }
  toast(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000
    });
  }
}
