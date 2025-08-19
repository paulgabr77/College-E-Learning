# 🎓 College E-Learning Platform

A modern, full-stack e-learning platform built with .NET 9 and Angular 17, designed for educational institutions to manage courses, students, and professors.

## ✨ Features

### 🔐 Authentication & Authorization
- JWT-based authentication system
- Role-based access control (Student, Professor, Admin)
- Secure password hashing with BCrypt
- Protected routes with authentication guards

### 📚 Course Management
- Create and manage courses with detailed information
- Course enrollment system
- Credit-based course structure
- Course status tracking

### 👥 User Management
- Student and professor profiles
- User registration and profile management
- Active/inactive user status

### 🎨 Modern UI/UX
- Responsive design with Tailwind CSS
- Modern dashboard with statistics
- Intuitive navigation
- Mobile-friendly interface

## 🛠️ Tech Stack

### Backend
- **.NET 9** - Latest .NET framework
- **Entity Framework Core** - ORM for database operations
- **SQL Server LocalDB** - Local database
- **JWT Authentication** - Token-based security
- **Swagger/OpenAPI** - API documentation
- **CORS** - Cross-origin resource sharing

### Frontend
- **Angular 17** - Latest Angular framework
- **Tailwind CSS** - Utility-first CSS framework
- **Angular Signals** - Modern state management
- **Angular Router** - Client-side routing
- **HTTP Client** - API communication

## 🚀 Quick Start

### Prerequisites
- .NET 9 SDK
- Node.js 18+ and npm
- SQL Server LocalDB (included with Visual Studio)

### Backend Setup
```bash
cd backend/e-learning-back/e-learning-back
dotnet restore
dotnet run
```

The backend will start on `http://localhost:5273`

### Frontend Setup
```bash
cd frontend/e-learning-front
npm install
npm start
```

The frontend will start on `http://localhost:4200`

### Database Setup
The application uses Entity Framework migrations. The database will be created automatically on first run.

### Test Data
To populate the database with test data, make a POST request to:
```
http://localhost:5273/api/testdata/seed
```

## 🔑 Test Credentials

### Professor Account
- **Email:** profesor@uni.test
- **Password:** Parola123!

### Student Account
- **Email:** student@uni.test
- **Password:** Parola123!

## 📁 Project Structure

```
College-E-Learning/
├── backend/
│   └── e-learning-back/
│       ├── Controllers/          # API endpoints
│       ├── Models/              # Entity models
│       ├── DTOs/                # Data transfer objects
│       ├── Services/            # Business logic
│       ├── DataOps/             # Repository pattern
│       └── Data/                # Database context
└── frontend/
    └── e-learning-front/
        ├── src/app/
        │   ├── pages/           # Main application pages
        │   ├── components/      # Reusable components
        │   ├── services/        # API services
        │   ├── models/          # TypeScript interfaces
        │   └── guards/          # Route guards
        └── public/              # Static assets
```

## 🔧 API Endpoints

### Authentication
- `POST /api/Users/login` - User authentication
- `POST /api/Users` - User registration

### Courses
- `GET /api/Courses` - Get all courses
- `POST /api/Courses` - Create new course
- `GET /api/Courses/{id}` - Get course by ID
- `PUT /api/Courses/{id}` - Update course
- `DELETE /api/Courses/{id}` - Delete course

### Users
- `GET /api/Users` - Get all users
- `GET /api/Users/{id}` - Get user by ID
- `PUT /api/Users/{id}` - Update user

### Test Data
- `POST /api/TestData/seed` - Populate test data

## 🎨 UI Components

### Pages
- **Login** - Authentication page with modern design
- **Dashboard** - Overview with statistics and recent courses
- **Courses** - Course listing with search and filters
- **Create Course** - Form for creating new courses
- **Profile** - User profile management

### Components
- **Navbar** - Responsive navigation with user info
- **Course Cards** - Modern course display
- **Forms** - Styled input forms with validation
- **Loading States** - User feedback during operations

## 🔒 Security Features

- JWT token authentication
- Password hashing with BCrypt
- CORS configuration for frontend-backend communication
- Role-based authorization
- Protected API endpoints

## 📱 Responsive Design

The application is fully responsive and works on:
- Desktop computers
- Tablets
- Mobile phones

## 🚀 Deployment

### Backend Deployment
- Deploy to Azure App Service
- Configure connection string for production database
- Set environment variables for JWT secrets

### Frontend Deployment
- Build with `ng build --prod`
- Deploy to Azure Static Web Apps or similar
- Configure API base URL for production

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 🆘 Support

For support and questions:
- Create an issue in the repository
- Contact the development team