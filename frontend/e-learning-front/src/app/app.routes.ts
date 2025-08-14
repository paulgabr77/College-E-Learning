import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { CoursesComponent } from './pages/courses/courses.component';

export const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: 'login' },
    { path: 'login', component: LoginComponent },
    { path: 'courses', component: CoursesComponent }
];
