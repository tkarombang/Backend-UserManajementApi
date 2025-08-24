using Backend_UserManagementApi.Data;
using Microsoft.EntityFrameworkCore;
using Backend_UserManagementApi.Profiles;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "";
Console.WriteLine($"[DEBUG] Connection String: {connectionString}");

if (builder.Environment.IsProduction())
{
    var uriString = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (!string.IsNullOrEmpty(uriString))
    {
        var uri = new Uri(uriString);
        connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.Segments.Last()};Username={uri.UserInfo.Split(':')[0]};Password={uri.UserInfo.Split(':')[1]};Ssl Mode=Require";
    }
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

// string? connectionString;



// if (builder.Environment.IsProduction())
// {
//     var host = Environment.GetEnvironmentVariable("PGHOST");
//     var port = Environment.GetEnvironmentVariable("PGPORT");
//     var database = Environment.GetEnvironmentVariable("PGDATABASE");
//     var user = Environment.GetEnvironmentVariable("PGUSER");
//     var password = Environment.GetEnvironmentVariable("PGPASSWORD");

//     connectionString = $"Host={host};Port={port};Database={database};Username={user};Password={password};Ssl Mode=Require";
// }
// else
// {
//     connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// }


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MappingProfile>();
}, typeof(Program).Assembly);



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.MapGet("/", () => "Wellcome in My Server");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


// var port = Environment.GetEnvironmentVariable("PORT") ?? "5101";
// app.Urls.Add($"http://0.0.0.0:{port}");

// Console.WriteLine($"Starting application on port: {port}");
Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");


app.Run();

