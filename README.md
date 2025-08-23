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



## Instruksi Pengoperasian (Operating Instructions)
### 1. Meng-clone Repository
Untuk mulai menggunakan aplikasi, langkah pertama adalah meng-clone repository dari GitHub ke komputer Anda. Jalankan perintah berikut di terminal atau command prompt:

```bash
git clone https://github.com/tkarombang/Backend-UserManajementApi.git
```
### 2. Masuk ke Folder Project
Setelah repository berhasil di-clone, masuk ke folder project menggunakan perintah cd:
```bash
cd repository-name
```

### 3. Install Dependencies
Setelah masuk ke folder project, pastikan semua dependencies yang diperlukan terinstall. Jalankan perintah berikut untuk menambahkan dependencies yang dibutuhkan oleh aplikasi:
```bash
dotnet restore
```
### 4. Menjalankan Aplikasi
Setelah dependencies terinstall, jalankan aplikasi menggunakan perintah:
```bash
dotnet run
```
Aplikasi akan berjalan di http://localhost:5101.

### 5. Mengakses API melalui Swagger
Buka browser dan kunjungi http://localhost:5101/swagger. Anda akan melihat antarmuka Swagger yang memungkinkan Anda untuk mengakses berbagai endpoint yang tersedia di backend.

### 6. Melakukan Operasi CRUD
Tambah User: Gunakan endpoint POST /api/User untuk menambahkan pengguna baru. Isi data yang diperlukan seperti nama, email, nomor telepon, status aktif, dan departemen.

Edit User: Gunakan endpoint PUT /api/User/{id} untuk mengupdate data pengguna berdasarkan ID. Anda bisa mengubah nama, email, dan detail lainnya.

Hapus User: Gunakan endpoint DELETE /api/User/{id} untuk menghapus pengguna berdasarkan ID.

Lihat Daftar Pengguna: Gunakan endpoint GET /api/User untuk melihat daftar seluruh pengguna.

### 7. Menambahkan Data ke Database
Pastikan Anda telah mengkonfigurasi database PostgreSQL dengan benar. Anda bisa menggunakan tools seperti pgAdmin untuk memverifikasi dan mengelola data dalam database.

### 8. Struktur Folder

```
UserManagementApi/
│── Controllers/
│   └── UsersController.cs      # Endpoint CRUD user
│
│── Data/
│   └── AppDbContext.cs         # DbContext untuk EF Core
|
│── DTOs/
│   └── UserCreateDto.cs        # DTO untuk create user
│   └── UserReadDto.cs          # DTO untuk menemukan user
│   └── UserUpdateDto.cs        # DTO untuk update user
|
│── Migrations/                 # Folder auto-generated EF Core
│
│── Models/
│   └── Response                
|   │   └── ErrorResponse.cs    # Error Handling
│   └── User.cs                 # Entity User
│
│── Profiles/
│   └── MappingProfiles.cs      # AutoMapper properti
│
│
│── Program.cs                  # Entry point ASP.NET
│── appsettings.json            # Konfigurasi database, logging, dll
│── Backend_UserManagementApi.csproj            

```
---


## Kontak

Jika Anda memiliki pertanyaan atau ingin berkontribusi, silakan hubungi saya melalui GitHub atau kirim email ke [tuangkarombang@gmail.com].

## Terima Kasih

Terima kasih telah menggunakan aplikasi ini! Jangan ragu untuk membuka *issues* di GitHub jika Anda menemukan bug atau memiliki saran fitur.

---

> **Note:** Jika Anda merasa aplikasi ini bermanfaat, jangan lupa untuk memberikan ⭐ pada repository ini di GitHub! 😊