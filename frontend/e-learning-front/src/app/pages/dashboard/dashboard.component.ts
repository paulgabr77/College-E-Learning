import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CourseService } from '../../services/course.service';
import { CourseDto } from '../../models/api.models';

@Component({
    selector: 'app-dashboard',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
    private readonly courseService = inject(CourseService);
    private readonly router = inject(Router);

    loading = signal(true);
    courses = signal<CourseDto[]>([]);
    stats = signal({
        totalCourses: 0,
        enrolledCourses: 0,
        totalStudents: 0,
        totalLessons: 0
    });

    constructor() {
        this.loadDashboardData();
    }

    private loadDashboardData(): void {
        this.courseService.getCourses().subscribe({
            next: (data) => {
                this.courses.set(data);
                this.calculateStats(data);
                this.loading.set(false);
            },
            error: () => {
                this.loading.set(false);
            }
        });
    }

    private calculateStats(courses: CourseDto[]): void {
        const totalStudents = courses.reduce((sum, course) => sum + course.enrolledStudentsCount, 0);
        const totalLessons = courses.reduce((sum, course) => sum + course.lessonsCount, 0);

        this.stats.set({
            totalCourses: courses.length,
            enrolledCourses: courses.filter(c => c.enrolledStudentsCount > 0).length,
            totalStudents,
            totalLessons
        });
    }

    viewCourse(courseId: number): void {
        this.router.navigateByUrl(`/courses/${courseId}`);
    }

    createCourse(): void {
        this.router.navigateByUrl('/create-course');
    }
}
