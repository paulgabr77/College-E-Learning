import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CourseService } from '../../services/course.service';

@Component({
    selector: 'app-create-course',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './create-course.component.html',
    styleUrl: './create-course.component.css'
})
export class CreateCourseComponent {
    private readonly courseService = inject(CourseService);
    private readonly router = inject(Router);

    loading = signal(false);
    error = signal<string | null>(null);

    course = signal({
        title: '',
        description: '',
        code: '',
        credits: 5,
        professorId: 1 // Placeholder - în viitor vom citi din user-ul autentificat
    });

    updateTitle(value: string): void {
        this.course.update(course => ({ ...course, title: value }));
    }

    updateDescription(value: string): void {
        this.course.update(course => ({ ...course, description: value }));
    }

    updateCode(value: string): void {
        this.course.update(course => ({ ...course, code: value }));
    }

    updateCredits(value: string): void {
        this.course.update(course => ({ ...course, credits: +value }));
    }

    submit(): void {
        this.error.set(null);
        this.loading.set(true);

        // În viitor, vom avea un service pentru crearea cursurilor
        // Pentru moment, simulăm o operație
        setTimeout(() => {
            this.loading.set(false);
            this.router.navigateByUrl('/courses');
        }, 1000);
    }

    cancel(): void {
        this.router.navigateByUrl('/dashboard');
    }
}
