using Backend_UserManagementApi.Data;
using Microsoft.EntityFrameworkCore;
using Backend_UserManagementApi.Profiles;
using System;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if(connectionString != null && connectionString.StartsWith("postgres://"))
{
    try
    {
        var databaseUri = new Uri(connectionString);
        var userInfo = databaseUri.UserInfo.Split(':');
        
        var updatedConnectionString = new NpgsqlConnectionStringBuilder
        {
            Host = databaseUri.Host,
            Port = databaseUri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = databaseUri.LocalPath.TrimStart('/'),
            SslMode = SslMode.Require
        }.ToString();
        
        connectionString = updatedConnectionString;
    }
    catch (Exception ex)
    {
         Console.WriteLine($"Error parsing DATABASE_URL: {ex.Message}");
    }
}

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MappingProfile>();
}, typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

Console.WriteLine($"Starting application on port: {port}");
Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");


app.Run();

