# Examonimy - Online Exam and Mark Tracking System

Examonimy is a web application designed to facilitate online exams, mark tracking, and overall academic performance management for educational institutions. Built with ASP.NET (.NET 7), Examonimy provides a user-friendly interface for both students and administrators to manage exams, track marks, and receive timely notifications.

## Features

1. **User Authentication:**
   - Users can register and log in securely to access their personalized dashboards.

2. **Dashboard:**
   - A central hub for students and admins to view important information, including upcoming exams, recent results, and notifications.

3. **Exam Creation:**
   - Admins can easily create exams, define questions, set durations, and configure exam properties.

4. **Online Exam Taking:**
   - Students can take exams online with features such as question navigation, timer countdown, and automatic submission.

5. **Exam Retake with Registration:**
   - Students can register for exam retakes with a form, providing reasons and additional preparation details.

6. **Mark Tracking:**
   - Admins can track and manage students' exam marks, view detailed exam reports, and export mark data.

7. **Grade Calculation:**
   - The system calculates overall grades for students based on exam performance and configurable weightage.

8. **Exam History:**
   - Users can access a detailed history of completed exams, review answers, and track overall performance.

9. **Notification System:**
   - Users receive timely notifications for upcoming exams, new results, and system announcements.

10. **Feedback System:**
    - Users can provide feedback on exams, courses, or the system, promoting continuous improvement.

11. **User Profile Management:**
    - Users can manage their profiles, update information, change passwords, and configure notification preferences.

## Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download)
- [ASP.NET Core Runtime](https://dotnet.microsoft.com/download/dotnet/6.0)

### Installation

1. Clone the repository: `git clone https://github.com/leonghia/examonimy.git`
2. Navigate to the project directory: `cd examonimy`

### Database Setup

1. Open a terminal and navigate to the project directory.
2. Run the following commands to apply migrations and update the database:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update

