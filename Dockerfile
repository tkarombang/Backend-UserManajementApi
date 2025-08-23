FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file
COPY ["Backend_UserManagementApi.csproj", "./"]
RUN dotnet restore "Backend_UserManagementApi.csproj"

# Copy everything else
COPY . .
RUN dotnet build "Backend_UserManagementApi.csproj" -c Release -o /app/build
RUN dotnet publish "Backend_UserManagementApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

RUN ls -la
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
ENV PORT=8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "Backend_UserManagementApi.dll"]