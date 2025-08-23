FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o ./out
# RUN dotnet publish -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy dari folder publish yang benar
COPY --from=build /app/publish .

# Debug: List files untuk verifikasi
RUN echo "Contents of /app:" && ls -la

ENV ASPNETCORE_URLS=http://+:8080
ENV PORT=8080

ENTRYPOINT ["dotnet", "Backend_UserManagementApi.dll"]