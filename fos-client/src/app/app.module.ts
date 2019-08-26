import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TestComponent } from './components/test/test.component';
import { OrderService } from './services/order/order.service';
import { HttpClientModule } from '@angular/common/http';
import { ListRestaurantComponent } from './components/list-restaurant/list-restaurant.component';
import { ServiceTabComponent } from './components/service-tab/service-tab.component';
import { RestaurantsPageComponent } from './pages/restaurants-page/restaurants-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import {
  MatTableModule,
  MatSortModule,
  MatPaginatorModule,
  MatTabsModule
} from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    TestComponent,
    ListRestaurantComponent,
    RestaurantsPageComponent,
    ServiceTabComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCheckboxModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatTabsModule
  ],
  providers: [OrderService],
  bootstrap: [AppComponent]
})
export class AppModule {}
