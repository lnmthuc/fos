import { Injectable } from '@angular/core';
import { DelegateHost } from 'src/app/models/delegate-host';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/models/user';
@Injectable({
  providedIn: 'root'
})
export class DelegateHostService {

  constructor(private http: HttpClient) {
  }
  create(delegateInfo: DelegateHost): Promise<ApiOperationResult<void>> {
    return new Promise<ApiOperationResult<void>>((resolve, reject) => {
      this.http
        .post<ApiOperationResult<ApiOperationResult<void>>>(
          environment.apiUrl + 'api/DelegateHost/Create',
          delegateInfo
        )
        .toPromise()
        .then(result => {
          if (result.Success) {
            resolve(result.Data);
          } else {
            reject(result.ErrorMessage);
          }
        });
    });
  }
  read(userInfo: User): Promise<ApiOperationResult<DelegateHost>> {
    return new Promise<ApiOperationResult<DelegateHost>>((resolve, reject) => {
      this.http
        .post<ApiOperationResult<DelegateHost>>(
          environment.apiUrl + 'api/DelegateHost/Read',
          userInfo
        )
        .toPromise()
        .then(result => {
          if (result.Success) {
            resolve(result);
          } else {
            reject(result.ErrorMessage);
          }
        });
    });
  }
}
