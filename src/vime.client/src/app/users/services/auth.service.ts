import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  login(userName: string, password: string): Observable<any> {
    const body = {
      Username: userName,
      Password: password,
    };
    return this.http.post<any>(`${environment.apiUrl}/api/LoginUser`, body);
  }

  register(userName: string, password: string): Observable<any> {
    const body = {
      Username: userName,
      Password: password,
    };
    return this.http.post<any>(`${environment.apiUrl}/api/RegisterUser`, body);
  }
}
