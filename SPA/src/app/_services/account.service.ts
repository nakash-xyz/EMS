import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ILoginModel } from '../_models/login-model';
import { IUserModel } from '../_models/user-model';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  apiUrl: string = environment.apiBaseUrl + '/account/';

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  login(data: ILoginModel) {
    return this.http.post<IUserModel>(this.apiUrl + 'login', data).pipe(
      map((user: IUserModel) => {
        if (user) {
          sessionStorage.setItem('token', user.token);
        }
      })
    );
  }

  isSessionValid(): boolean {
    const token = sessionStorage.getItem('token');

    if (token) {
      return !this.jwtHelper.isTokenExpired(token);
    }

    return false;
  }

  getDisplayName(): string {
    const token = sessionStorage.getItem('token');

    if (token) {
      return this.jwtHelper.decodeToken(token)?.displayName!;
    }

    return '';
  }

  getUserRole(): string {
    const token = sessionStorage.getItem('token');

    if (token) {
      return this.jwtHelper.decodeToken(token)?.role!;
    }

    return '';
  }
}
