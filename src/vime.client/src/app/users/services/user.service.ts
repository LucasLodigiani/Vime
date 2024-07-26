import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor() {
    this.updateUserData();
  }

  private userDataSubject = new BehaviorSubject<any>(null);
  userData$ = this.userDataSubject.asObservable();

  setUser(data: any) {
    const { username, role, jwt } = data;
    localStorage.setItem('username', username);
    localStorage.setItem('role', role);
    localStorage.setItem('jwt', jwt);
    this.updateUserData();
  }

  private updateUserData() {
    const username = localStorage.getItem('username');
    const role = localStorage.getItem('role');
    const isAuthenticated = localStorage.getItem('jwt') || false;
    this.userDataSubject.next({ username, role, isAuthenticated });
  }

  logout(){
    const username = localStorage.removeItem('username');
    const role = localStorage.removeItem('role');
    const isAuthenticated = localStorage.removeItem('jwt');
    this.updateUserData();
  }

  getToken(){
    return localStorage.getItem("jwt");
  }
}
