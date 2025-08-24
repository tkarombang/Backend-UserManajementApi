using Backend_UserManagementApi.Data;
using Microsoft.EntityFrameworkCore;
using Backend_UserManagementApi.Profiles;
using Npgsql;


var builder = WebApplication.CreateBuilder(args);
var connectionString = "";


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
Console.WriteLine($"[DEBUG] Connection String: {connectionString}");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MappingProfile>();
}, typeof(Program).Assembly);



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// AUTO-MIGRATION IN Railway - START
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}
// AUTO-MIGRATION IN Railway - END

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

// DEBUG Lokasi Environment
Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");


app.Run();

