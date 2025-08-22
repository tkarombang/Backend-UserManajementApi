# User Management Application

Aplikasi manajemen user berbasis web dengan backend menggunakan ASP.NET Core

## Deskripsi

Aplikasi ini dirancang untuk memungkinkan admin untuk mengelola daftar pengguna (user), termasuk menambah, mengedit, dan menghapus pengguna. Requirement untuk membangun aplikasi ini:

1. **Backend**: API menggunakan ASP.NET Core untuk menangani data pengguna dan operasi CRUD (Create, Read, Update, Delete) - PostgreSQL database.
2. **C# (dibaca "C sharp")**: merupaka bahasa pemrograman yang dikembangkan oleh Microsoft sebagai bagian dari platform .NET. C# adalah bahasa berorientasi objek yang dirancang untuk membangun berbagai jenis aplikasi, termasuk aplikasi desktop, web, dan mobile.
3. **Vscode**: Visual Studio Code (VS Code) adalah editor kode gratis dan open-source yang dikembangkan oleh Microsoft.
4. **C#, C#-DevKit, .NET Install Tool**: Extension C# adalah ekstensi Visual Studio Code (VS Code) yang menyediakan layanan bahasa dasar untuk C#, seperti intellisense dan debugging.  C# Dev Kit adalah sekumpulan ekstensi VS Code yang dibangun di atas ekstensi C# untuk menyediakan pengalaman pengembangan C# yang lebih kaya, termasuk manajemen solusi, templat, penelusuran kesalahan, dan AI. .NET Install Tool adalah alat yang otomatis terinstal dengan C# Dev Kit untuk memastikan lingkungan pengembangan Anda siap, termasuk alat-alat yang diperlukan untuk .NET. 
---

## Tech Stack

- **Backend**: 
  - ![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-5C2D91?style=flat&logo=.net&logoColor=white)
  - ![Entity Framework Core](https://img.shields.io/badge/Entity_Framework_Core-9B4D96?style=flat&logo=dotnet&logoColor=white)
  - ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
  - ![Csharp](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
  - ![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=Swagger&logoColor=white)

---

## Backend Setup (ASP.NET Core API)

### 1. Persiapan Project
- Buka terminal/command prompt dan jalankan perintah berikut untuk membuat project baru:
  ```bash
  dotnet new webapi -n Backend-UserManajementApi

- Masuk Ke folder Project
    ```
    cd Backend-UserManajementApi

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


- Menjalankan Backend
    ```
    dotnet run

## Dokumentasi API

Swagger akan tersedia di http://localhost:5101/swagger untuk melihat endpoint-endpoint yang ada di backend.

- Contoh Payload JSON    
    - POST /api/User
    ```
    201 Response body  
        {
        "nama": "Muhammad Azwar Anas",
        "email": "muhanaz@example.com",
        "nomorTelepon": "085159501107",
        "statusAktif": true,
        "departemen": "WebDev"
        }
    ```
    - PUT /api/User/{id}
    ```
    200 Response body
        {
        "nama": "Azwar",
        "email": "az@example.com",
        "nomorTelepon": "085159500011",
        "statusAktif": false,
        "departemen": "IT"
        }    
    ```
## Best Practice
1. **Validation(CreateUser & UpdateUser)**: Jika ada masalah pada model (misalnya tidak valid) controller akan mengembalikan respons BadRequest dengan detail error yang diambil dari ModelState.

2. **DTOs (Data Transfer Object)**: Data yang masuk lebih terkontrol (misalnya client tidak bisa iseng mengirim Id atau field lain yang seharusnya hanya ditentukan server)

3. **Error Handling**: Menambah Class ErrorResponse untuk menangani format response error yang konsisten di seluruh API
Struktur Response Error = setiap response error menyertakan StatusCode, Message, dan list Errors yang memuat detail masalah.

4. **AutoMapper**: yang akan mempermudah untuk memetakan antara entitas model (seperti User) dan objek DTO (seperti UserReadDto, UserCreateDto, dan UserUpdateDto). Dengan AutoMapper, tidak perlu lagi melakukan mapping manual satu per satu properti seperti sebelumnya.