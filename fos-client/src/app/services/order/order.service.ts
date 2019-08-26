import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
// import { EnvironmentService } from "../shared/service/environment.service";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private baseUrl: string;

  constructor(
    private http: HttpClient // , private envService: EnvironmentService
  ) {
    // this.baseUrl = envService.getApiUrl() + "/api/order";
  }

  getOrder(orderId: string) {
    return this.http.get<any>(environment.apiUrl + '/api/order/getOrder', {
      params: {
        id: orderId
      }
    });
  }
}