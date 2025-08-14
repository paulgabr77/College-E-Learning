import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CourseService } from '../../services/course.service';
import { CourseDto } from '../../models/api.models';

@Component({
    selector: 'app-courses',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './courses.component.html',
    styleUrl: './courses.component.css'
})
export class CoursesComponent {
    private readonly courseService = inject(CourseService);

    loading = signal(true);
    error = signal<string | null>(null);
    courses = signal<CourseDto[]>([]);

    constructor() {
        this.courseService.getCourses().subscribe({
            next: (data) => {
                this.courses.set(data);
                this.loading.set(false);
            },
            error: () => {
                this.error.set('Nu am putut încărca cursurile');
                this.loading.set(false);
            }
        });
    }
}


