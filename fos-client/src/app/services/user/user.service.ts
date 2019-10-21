import { Injectable } from "@angular/core";
import { HttpClient, XhrFactory } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { User } from "src/app/models/user";
import { OauthService } from '../oauth/oauth.service';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: "root"
})
export class UserService {
  constructor(private http: HttpClient, private oauthService: OauthService) {}

  getCurrentUser(): Promise<User> {
    return new Promise<User>((resolve, reject) => {
      this.http
        .get<ApiOperationResult<User>>(
          environment.apiUrl + "api/spuser/GetCurrentUser"
        )
        .toPromise()
        .then(result => {
          if (result.Success) {
            resolve(result.Data);
          } else reject(new Error(JSON.stringify(result.ErrorMessage)));
        })
        .catch(alert => this.oauthService.checkAuthError(alert));
    });
  }
  getUserById(id: string): Promise<User> {
    return new Promise<User>((resolve, reject) => {
      this.http
        .get<ApiOperationResult<User>>(
          environment.apiUrl + "api/spuser/GetUserById",
          {
            params: {
              Id: id
            }
          }
        )
        .toPromise()
        .then(result => {
          if (result.Success) {
            resolve(result.Data);
          } else reject(new Error(JSON.stringify(result.ErrorMessage)));
        })
        .catch(alert => this.oauthService.checkAuthError(alert));
    });
  }
  SiteGroupListMemers(
    groupName: string
  ): Observable<ApiOperationResult<Array<User>>> {
    return this.http
      .get<ApiOperationResult<Array<User>>>(
        environment.apiUrl + "api/SPUser/SiteGroupListMemers",
        {
          params: {
            groupName: groupName
          }
        }
      )
      .pipe(
        tap((response: ApiOperationResult<Array<User>>) => {
          return response;
        })
      );
  }
  SiteGroupAddMembers(
    user: User
  ): Observable<ApiOperationResult<void>> {
    return this.http
      .post(environment.apiUrl + "api/SPUser/SiteGroupAddMembers", user)
      .pipe(
        tap((response: ApiOperationResult<void>) => {
          return response;
        })
      );
  }
}
