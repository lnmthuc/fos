import { Injectable } from '@angular/core';
import { HttpClient, XhrFactory } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getCurrentUserId(): Promise<User> {
    return new Promise<User>((resolve,reject)=>{
      this.http.get<ApiOperationResult<User>>(environment.apiUrl + 'api/spuser/GetCurrentUser')
      .toPromise().then(result => {
        if(result.Success){resolve(result.Data)}
        else reject(new Error(JSON.stringify(result.ErrorMessage)));        
      }).catch(alert => console.log(alert))
    });
  }
}
