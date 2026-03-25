import { Component, inject, signal } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './../services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'] 
})
export class LoginComponent {

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  submitting = signal(false);
  error = signal(false);

  loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  submit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.error.set(false);
    this.submitting.set(true);

    const { username, password } = this.loginForm.value;

    this.authService.login(username!, password!)
      .subscribe({
        next: (res) => {
          localStorage.setItem('token', res.token);
          this.submitting.set(false);
          this.router.navigate(['/canciones']);
        },
        error: () => {
          this.submitting.set(false);
          this.error.set(true);
        }
      });
  }
}