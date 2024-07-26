import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  constructor(private authService: AuthService, private router: Router) {}
  registerForm = new FormGroup({
    username: new FormControl<string>(''),
    password: new FormControl<string>(''),
  });
  isLoading: boolean = false;
  errors: string = '';

  onSubmit() {
    const username = this.registerForm.value.username;
    const password = this.registerForm.value.password;
    this.isLoading = true;

    this.authService.register(username ?? '', password ?? '').subscribe({
      next: (response) => {
        console.log(response);
        this.isLoading = false;
        this.router.navigate(['/auth/login']);
      },
      error: (err) => {
        console.error('Error en la solicitud', err);
        this.errors = err.error.message;
        this.isLoading = false;
      },
    });
  }
}
