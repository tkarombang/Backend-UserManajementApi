
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS build-ENV
WORKDIR /app

COPY ["Backend_UserManagementApi.csproj", "./"]
RUN dotnet restore "Backend_UserManagementApi.csproj"

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Backend_UserManagementApi.dll"]