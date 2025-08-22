# User Management Application

Aplikasi manajemen user berbasis web dengan backend menggunakan ASP.NET Core dan frontend menggunakan Next.js dan Tailwind CSS.

## Deskripsi

Aplikasi ini dirancang untuk memungkinkan admin untuk mengelola daftar pengguna (user), termasuk menambah, mengedit, dan menghapus pengguna. Aplikasi ini terdiri dari dua bagian utama:

1. **Backend**: API menggunakan ASP.NET Core untuk menangani data pengguna dan operasi CRUD (Create, Read, Update, Delete).
2. **Frontend**: Antarmuka pengguna berbasis Next.js dan Tailwind CSS untuk interaksi dengan API.

---

## Tech Stack

- **Backend**: 
  - ![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-5C2D91?style=flat&logo=.net&logoColor=white) ASP.NET Core
  - ![Entity Framework Core](https://img.shields.io/badge/Entity_Framework_Core-9B4D96?style=flat&logo=dotnet&logoColor=white)
  - ![SQLite](https://img.shields.io/badge/SQLite-003B57?style=flat&logo=sqlite&logoColor=white)
- **Frontend**: 
  - ![Next.js](https://img.shields.io/badge/Next.js-000000?style=flat&logo=nextdotjs&logoColor=white) Next.js
  - ![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-06B6D4?style=flat&logo=tailwindcss&logoColor=white)

---

## Backend Setup (ASP.NET Core API)

### 1. Persiapan Project
- Buka terminal/command prompt dan jalankan perintah berikut untuk membuat project baru:
  ```bash
  dotnet new webapi -n UserManagementApi

- Masuk Ke folder Project
    ```
    cd UserManagementApi
- Install Dependecies
    ```
    dotnet add package Microsoft.EntityFrameworkCore
    dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.0
    dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
    dotnet add package Microsoft.EntityFrameworkCore.Tools

- Setup Swagger
    ```csharp
    pastikan memiliki kode di bawah ini:

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

Swagger akan tersedia pada http://localhost:5000/swagger di environment pengembangan.

- Menjalankan Backend
    ```
    dotnet run

