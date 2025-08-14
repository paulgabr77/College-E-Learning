export interface UserDto {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    role: number; // enum on backend
    createdAt: string;
    isActive: boolean;
    fullName?: string;
}

export interface LoginResponseDto {
    user: UserDto;
    token: string;
    expiresAt: string;
}

export interface CourseDto {
    id: number;
    title: string;
    description: string;
    code: string;
    credits: number;
    status: number; // enum on backend
    createdAt: string;
    updatedAt?: string | null;
    professorId: number;
    professorName: string;
    enrolledStudentsCount: number;
    lessonsCount: number;
    assessmentsCount: number;
}


