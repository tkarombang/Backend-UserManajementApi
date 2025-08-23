# Tahap 1: Build aplikasi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Salin semua file proyek, termasuk solusi dan folder proyek
COPY . .

# Publish aplikasi
RUN dotnet publish "Backend_UserManagementApi.csproj" -c Release -o /app/publish

# Tahap 2: Jalankan aplikasi
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "Backend_UserManagementApi.dll"]