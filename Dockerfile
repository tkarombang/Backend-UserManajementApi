# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore
COPY ["Backend_UserManagementApi.csproj", "."]
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet build -c Release --no-restore
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime stage  
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

# Copy published files
COPY --from=build /app/publish .

# Perhatikan nama DLL yang sesuai
ENTRYPOINT ["dotnet", "Backend_UserManagementApi.dll"]