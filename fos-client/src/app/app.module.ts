import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TestComponent } from './components/test/test.component';
import { OrderService } from './services/order/order.service';
import { HttpClientModule } from '@angular/common/http';
import { ListRestaurantComponent } from './components/list-restaurant/list-restaurant.component';
import { ServiceTabComponent } from './components/service-tab/service-tab.component';
import { RestaurantsPageComponent } from './pages/restaurants-page/restaurants-page.component';
import { MatCheckboxModule } from '@angular/material/checkbox';

import { MatSnackBarModule } from '@angular/material/snack-bar';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './auth/TokenInterceptor';

import { CookieService } from 'ngx-cookie-service';
import { AuthService } from './auth/auth.service';

import { HeaderComponent } from './components/navigation/header/header.component';
import { SidenavListComponent } from './components/navigation/sidenav-list/sidenav-list.component';

import { OrdersPageComponent } from './pages/orders-page/orders-page.component';
import { SummaryPageComponent } from './pages/summary-page/summary-page.component';

import {
  MatTableModule,
  MatSortModule,
  MatPaginatorModule,
  MatTabsModule,
  MatSidenavModule,
  MatListModule,
  MatInputModule,
  MatTooltipModule,
  MatRadioModule,
} from '@angular/material';
import {
  DlDateTimeDateModule,
  DlDateTimePickerModule
} from 'angular-bootstrap-datetimepicker';
import { MaterialTimePickerModule } from '@candidosales/material-time-picker';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularDateTimePickerModule } from 'angular2-datetimepicker';
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SearchComponent } from './components/search/search.component';


import {
  MatButtonModule,
  MatCardModule,
  MatDialogModule,
  MatToolbarModule,
  MatMenuModule,
  MatIconModule,
  MatProgressSpinnerModule,
  MatSlideToggleModule
} from '@angular/material';

import { MatSelectModule } from "@angular/material/select";
import { MatGridListModule } from "@angular/material/grid-list";
import { SelectAutocompleteModule } from "mat-select-autocomplete";
import { MenuComponent } from "./components/menu/menu.component";
import { DialogComponent } from "./components/dialog/dialog.component";
import { LightboxModule } from 'ngx-lightbox';

import { MatProgressBarModule } from "@angular/material/progress-bar";
import { FoodCategoriesComponent } from "./components/dialog/food-categories/food-categories.component";
import { LoadingComponent } from "./components/loading/loading.component";
import { OrderTabComponent } from "./components/order-tab/order-tab.component";
import { ListOrderComponent } from "./components/list-order/list-order.component";
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { OrderDetailComponent } from "./components/order-detail/order-detail.component";
import { MenuEventComponent } from "./components/menu-event/menu-event.component";
import { EventDialogComponent } from "./components/event-dialog/event-dialog.component";
import { EventDialogViewComponent } from "./components/event-dialog-view/event-dialog-view.component";
import { NgxStarsModule } from "ngx-stars";
import { ShowActionComponent } from "./components/show-action/show-action.component";
import { EventSummaryDialogComponent } from "./components/event-summary-dialog/event-summary-dialog.component";
import { FoodComponent } from "./components/dialog/food/food.component";
import { ListOrderedFoodsComponent } from "./components/order-detail/list-ordered-foods/list-ordered-foods.component";
import { ReminderDialogComponent } from "./components/reminder-dialog/reminder-dialog.component";
import { EventDialogEditComponent } from "./components/event-dialog-edit/event-dialog-edit.component";
import { EventDialogConfirmComponent } from "./components/event-dialog-confirm/event-dialog-confirm.component";
import { SettingDialogComponent } from "./components/navigation/header/setting-dialog/setting-dialog.component";
import { MatDatepickerModule, MatNativeDateModule } from "@angular/material";
import { PrintLayoutComponent } from './print-layout/print-layout.component';
import { EventSummaryPrintComponent } from './components/event-summary-dialog/event-summary-print/event-summary-print.component';
import { PhotoComponent } from './components/photo/photo/photo.component';
import { SummaryListComponent } from './components/summary-list/summary-list.component';
import { SummaryTabComponent } from './components/summary-tab/summary-tab.component';
import { SummaryDishesDialogComponent } from './components/summary-dishes-dialog/summary-dishes-dialog.component';
import { SettingDialogComponent } from './components/navigation/header/setting-dialog/setting-dialog.component';
import { EventDialogUserpickerComponent } from './components/event-dialog/event-dialog-userpicker/event-dialog-userpicker.component';
import { DatetimepickerComponent } from './components/datetimepicker/datetimepicker/datetimepicker.component';

@NgModule({
  declarations: [
    AppComponent,
    TestComponent,
    ListRestaurantComponent,
    RestaurantsPageComponent,
    ServiceTabComponent,
    HeaderComponent,
    SidenavListComponent,
    OrdersPageComponent,
    SummaryPageComponent,
    SearchComponent,
    DialogComponent,
    MenuComponent,
    FoodCategoriesComponent,
    LoadingComponent,
    OrderDetailComponent,
    MenuEventComponent,
    EventDialogComponent,
    OrderTabComponent,
    ListOrderComponent,
    OrderDetailComponent,
    ShowActionComponent,
    EventDialogViewComponent,
    EventSummaryDialogComponent,
    FoodComponent,
    ListOrderedFoodsComponent,
    ReminderDialogComponent,
    EventDialogEditComponent,
    EventDialogConfirmComponent,
    SettingDialogComponent,
    PrintLayoutComponent,
    EventSummaryPrintComponent,
    PhotoComponent,
    SummaryListComponent,
    SummaryTabComponent,
    SummaryDishesDialogComponent,
    SettingDialogComponent,
    EventDialogUserpickerComponent
    SettingDialogComponent,
    DatetimepickerComponent
  ],
  // declarations: [
  //     AppComponent,
  //     TestComponent,
  //     ListRestaurantComponent,
  //     RestaurantsPageComponent,
  //     ServiceTabComponent,
  //     EventFormComponent
  // ],
  imports: [
    NgbModule,
    BrowserModule,
    AppRoutingModule,
    DlDateTimeDateModule,
    DlDateTimePickerModule,
    FormsModule,
    AngularDateTimePickerModule,
    AngularMultiSelectModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    MatInputModule,
    MatDialogModule,
    MatTableModule,
    MatMenuModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatProgressBarModule,
    MatAutocompleteModule,
    MatGridListModule,
    SelectAutocompleteModule,
    MatSortModule,
    MatPaginatorModule,
    NgxStarsModule,
    MatTabsModule,
    HttpClientModule,
    MatCheckboxModule,
    MatPaginatorModule,
    MatTabsModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatSnackBarModule,
    MatTooltipModule,
    MatSlideToggleModule,
    MatRadioModule,
    LightboxModule,
    MaterialTimePickerModule,
    MatDatepickerModule, // <----- import(must)
    MatNativeDateModule // <----- import for date formating(optional)
  ],
  providers: [
    OrderService,
    CookieService,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  exports: [
    HeaderComponent,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    DialogComponent,
    MenuEventComponent,
    EventDialogComponent,
    EventDialogViewComponent,
    ReminderDialogComponent,
    EventDialogEditComponent,
    SummaryDishesDialogComponent,
    SettingDialogComponent
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    DialogComponent,
    EventDialogComponent,
    EventDialogViewComponent,
    EventSummaryDialogComponent,
    ReminderDialogComponent,
    EventDialogEditComponent,
    EventDialogConfirmComponent,
    SummaryDishesDialogComponent,
    SettingDialogComponent
  ]
})
export class AppModule {}
