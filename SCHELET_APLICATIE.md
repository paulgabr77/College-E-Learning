# Schelet Aplicație E-Learning

## 📋 Prezentare Generală
Aplicația este o platformă de e-learning construită cu:
- **Backend**: ASP.NET Core Web API (.NET 8)
- **Frontend**: Angular 20
- **Database**: SQL Server (LocalDB)
- **ORM**: Entity Framework Core

## 🏗️ Structura Backend

### 1. **Program.cs** - Configurația Principală
```csharp
// Servicii configurate:
- Entity Framework (SQL Server)
- CORS (permite localhost:4200)
- Dependency Injection pentru Repository și Services
- OpenAPI/Swagger
```

### 2. **Models** - Entitățile de Date

#### **User.cs** - Utilizatori
```csharp
Proprietăți:
- Id, FirstName, LastName, Email, Password
- Role (Student/Professor/Admin)
- CreatedAt, IsActive
- Navigation: EnrolledCourses, TeachingCourses
```

#### **Course.cs** - Cursuri
```csharp
Proprietăți:
- Id, Title, Description, Code, Credits
- Status (Draft/Published/Archived)
- ProfessorId (FK către User)
- Navigation: Professor, EnrolledStudents, Lessons, Assessments
```

#### **Lesson.cs** - Lecții
```csharp
Proprietăți:
- Id, Title, Content, OrderIndex
- CourseId (FK către Course)
- Navigation: Course, Materials
```

#### **Material.cs** - Materiale
```csharp
Proprietăți:
- Id, Title, FilePath, FileType
- LessonId (FK către Lesson)
- Navigation: Lesson
```

#### **Assessment.cs** - Evaluări
```csharp
Proprietăți:
- Id, Title, Description, DueDate
- CourseId (FK către Course)
- Navigation: Course, Questions, Submissions
```

#### **Question.cs** - Întrebări
```csharp
Proprietăți:
- Id, Text, QuestionType, Points
- AssessmentId (FK către Assessment)
- Navigation: Assessment, Answers
```

#### **Submission.cs** - Predări
```csharp
Proprietăți:
- Id, SubmittedAt, Grade, Feedback
- AssessmentId, StudentId, GradedBy (FK-uri)
- Navigation: Assessment, Student, Grader, Answers
```

#### **Answer.cs** - Răspunsuri
```csharp
Proprietăți:
- Id, Text, IsCorrect
- QuestionId, SubmissionId (FK-uri)
- Navigation: Question, Submission
```

### 3. **Data** - Contextul Bazei de Date

#### **ApplicationDbContext.cs**
```csharp
DbSet-uri configurate:
- Users, Courses, Lessons, Materials
- Assessments, Questions, Submissions, Answers

Relații configurate:
- Many-to-Many: Course-User (enrollments)
- One-to-Many: Course-Lessons, Course-Assessments
- One-to-Many: Lesson-Materials, Assessment-Questions
- One-to-Many: Assessment-Submissions, Question-Answers

Indexuri unice:
- User.Email, Course.Code, Lesson(CourseId, OrderIndex)
```

### 4. **Controllers** - API Endpoints

#### **UsersController.cs**
```csharp
Endpoints disponibile:
- GET /api/users - Toți utilizatorii
- GET /api/users/{id} - Utilizator după ID
- POST /api/users - Creează utilizator
- PUT /api/users/{id} - Actualizează utilizator
- DELETE /api/users/{id} - Șterge utilizator
- POST /api/users/login - Autentificare
- GET /api/users/students - Toți studenții
- GET /api/users/professors - Toți profesorii
```

#### **TestController.cs** - Controller de test
#### **WeatherForecastController.cs** - Controller template

### 5. **Services** - Logica de Business

#### **IUserService.cs & UserService.cs**
```csharp
Metode implementate:
- GetAllUsersAsync()
- GetUserByIdAsync(id)
- CreateUserAsync(dto)
- UpdateUserAsync(id, dto)
- DeleteUserAsync(id)
- LoginAsync(dto)
- GetStudentsAsync()
- GetProfessorsAsync()
```

### 6. **DataOps** - Repository Pattern

#### **Interfaces**
- **IUserRepository.cs** - Operații cu utilizatori
- **ICourseRepository.cs** - Operații cu cursuri

#### **Repositories**
- **UserRepository.cs** - Implementarea pentru utilizatori
- **CourseRepository.cs** - Implementarea pentru cursuri

### 7. **DTOs** - Data Transfer Objects

#### **UserDto.cs**
```csharp
Proprietăți pentru transfer:
- Id, FirstName, LastName, Email, Role
- (fără Password pentru securitate)
```

#### **CourseDto.cs**
```csharp
Proprietăți pentru transfer:
- Id, Title, Description, Code, Credits, Status
- ProfessorId, ProfessorName
```

### 8. **Configurații**

#### **appsettings.json**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ELearningDb;..."
  }
}
```

## 🎨 Structura Frontend

### 1. **Angular 20** - Framework
```json
Dependințe principale:
- @angular/common, @angular/core, @angular/forms
- @angular/platform-browser, @angular/router
- rxjs, tslib, zone.js
```

### 2. **Structura Fișierelor**
```
src/
├── app/
│   ├── app.component.ts
│   ├── app.routes.ts (gol momentan)
│   ├── app.config.ts
│   └── app.html
├── main.ts
├── index.html
└── styles.css
```

### 3. **Configurații**
- **angular.json** - Configurația Angular
- **tailwind.config.js** - Tailwind CSS configurat
- **tsconfig.json** - TypeScript configurat

## 🔄 Migrații și Database

### **Migrations**
- **20250801160400_InitialCreate.cs** - Prima migrare
- **ApplicationDbContextModelSnapshot.cs** - Snapshot-ul modelului

## 🚀 Pași Următori Recomandați

### 1. **Backend**
- [ ] Implementează autentificare JWT
- [ ] Adaugă validări și autorizare
- [ ] Creează controller pentru cursuri
- [ ] Implementează upload de fișiere pentru materiale
- [ ] Adaugă logging și error handling

### 2. **Frontend**
- [ ] Creează componente pentru autentificare
- [ ] Implementează dashboard pentru studenți/profesori
- [ ] Adaugă routing și navigation
- [ ] Creează formulare pentru cursuri și lecții
- [ ] Implementează upload de fișiere

### 3. **Database**
- [ ] Adaugă date de test
- [ ] Optimizează query-urile
- [ ] Implementează backup strategy

### 4. **Securitate**
- [ ] Implementează hash pentru parole
- [ ] Adaugă validări pe frontend
- [ ] Implementează rate limiting
- [ ] Adaugă HTTPS în producție

## 📝 Note Importante

1. **CORS** este configurat pentru `localhost:4200`
2. **Database** folosește LocalDB (SQL Server Express)
3. **Entity Framework** este configurat cu Code First
4. **Repository Pattern** este implementat pentru separarea logicii
5. **DTOs** sunt folosite pentru transferul de date
6. **Angular** este configurat cu Tailwind CSS

## 🔧 Comenzi Utile

### Backend
```bash
cd backend/e-learning-back/e-learning-back
dotnet run
```

### Frontend
```bash
cd frontend/e-learning-front
npm install
ng serve
```

### Database
```bash
# Creare migrare nouă
dotnet ef migrations add NumeMigrare

# Aplicare migrări
dotnet ef database update
```
