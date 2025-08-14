import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CourseDto } from '../models/api.models';

@Injectable({ providedIn: 'root' })
export class CourseService {
    private readonly http = inject(HttpClient);
    private readonly apiBase = 'http://localhost:5273/api';

    getCourses(): Observable<CourseDto[]> {
        return this.http.get<CourseDto[]>(`${this.apiBase}/Courses`);
    }
}


