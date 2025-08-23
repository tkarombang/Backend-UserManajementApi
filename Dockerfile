FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["Backend_UserManagementApi.csproj", "."]
RUN dotnet restore "Backend_UserManagementApi.csproj"

COPY . .
RUN dotnet build "Backend_UserManagementApi.csproj" -c Release -o /app/build
RUN dotnet publish "Backend_UserManagementApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Environment Variables untuk Production
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_NOLOGO=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV ASPNETCORE_URLS=http://+:8080

# Expose port (Railway akan handle port secara otomatis)
EXPOSE 8080

# Create non-root user untuk security
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Backend_UserManagementApi.dll"]