import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { LoginResponseDto } from '../models/api.models';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly http = inject(HttpClient);
    private readonly apiBase = 'http://localhost:5273/api';

    login(email: string, password: string): Observable<LoginResponseDto> {
        return this.http
            .post<LoginResponseDto>(`${this.apiBase}/Users/login`, { email, password })
            .pipe(tap((res) => localStorage.setItem('token', res.token)));
    }

    getToken(): string | null {
        return localStorage.getItem('token');
    }

    logout(): void {
        localStorage.removeItem('token');
    }
}


