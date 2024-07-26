import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  constructor(private authService: AuthService, private userService : UserService,private router: Router) {}
  loginForm = new FormGroup({
    username: new FormControl<string>(''),
    password: new FormControl<string>(''),
  });
  isLoading : boolean = false;
  errors : string = '';

  onSubmit() {
    const username = this.loginForm.value.username;
    const password = this.loginForm.value.password;
    this.isLoading = true;

    this.authService.login(username ?? '', password ?? '').subscribe({
      next : response => {
        console.log(response);
        this.isLoading = false;
        this.userService.setUser(response);
        this.router
          .navigateByUrl('/', { skipLocationChange: true, replaceUrl: true })
          .then(() => {
            this.router.navigate([this.router.url]);
          });

      },
      error : err => {
        console.error("Error en la solicitud", err);
        this.errors = err.error.message;
        this.isLoading = false;
      }
    });
  }
}
