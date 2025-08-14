import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './login.component.html',
    styleUrl: './login.component.css'
})
export class LoginComponent {
    private readonly auth = inject(AuthService);
    private readonly router = inject(Router);

    email = signal('profesor@uni.test');
    password = signal('Parola123!');
    loading = signal(false);
    error = signal<string | null>(null);

    submit(): void {
        this.error.set(null);
        this.loading.set(true);
        this.auth.login(this.email(), this.password()).subscribe({
            next: () => {
                this.loading.set(false);
                this.router.navigateByUrl('/courses');
            },
            error: (err) => {
                this.loading.set(false);
                this.error.set(err?.error?.message ?? 'Autentificare eșuată');
            }
        });
    }
}


