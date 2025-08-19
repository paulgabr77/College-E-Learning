import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UserDto } from '../../models/api.models';

@Component({
    selector: 'app-navbar',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './navbar.component.html',
    styleUrl: './navbar.component.css'
})
export class NavbarComponent {
    private readonly auth = inject(AuthService);
    private readonly router = inject(Router);

    user = signal<UserDto | null>(null);

    constructor() {
        // În viitor, vom citi user-ul din localStorage sau din un service de state
        this.loadUser();
    }

    private loadUser(): void {
        // Placeholder - în viitor vom avea un service de state management
        const token = this.auth.getToken();
        if (token) {
            // Decodifică token-ul pentru a obține user-ul
            // Pentru moment, folosim date mock
            this.user.set({
                id: 1,
                firstName: 'Ion',
                lastName: 'Ionescu',
                email: 'profesor@uni.test',
                role: 1, // Professor
                createdAt: new Date().toISOString(),
                isActive: true,
                fullName: 'Ion Ionescu'
            });
        }
    }

    logout(): void {
        this.auth.logout();
        this.user.set(null);
        this.router.navigateByUrl('/login');
    }

    isProfessor(): boolean {
        return this.user()?.role === 1;
    }

    isStudent(): boolean {
        return this.user()?.role === 0;
    }
}
