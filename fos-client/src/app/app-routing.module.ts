import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RestaurantsPageComponent } from './pages/restaurants-page/restaurants-page.component';
import { EventFormComponent } from './event-form/event-form.component';
import { OrdersPageComponent } from './pages/orders-page/orders-page.component';
import { MealsPageComponent } from './pages/meals-page/meals-page.component';
import { OrderDetailComponent } from './components/order-detail/order-detail.component';

const routes: Routes = [
  {
    path: 'event-form',
    component: EventFormComponent
  },
  {
    path: 'make-order/:id',
    component: OrderDetailComponent
  },
  {
    path: 'home',
    component: RestaurantsPageComponent
  },
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'events',
    component: OrdersPageComponent
  },
  {
    path: 'meals',
    component: MealsPageComponent
  },
  {
    path: 'events/detail/:id',
    component: EventFormComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
