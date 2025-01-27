using Microsoft.EntityFrameworkCore;
using TABP.Application.ApplicationServices;
using TABP.Infrastructure.InfrastructureServices;
using Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.ImageServices;
using TABP.API.Extra;
using TABP.Infrastructure.EmailServices.EmailService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add authorization services before building the application
builder.Services.AddAuthorization(options =>
    options.AddPolicy("IsAdmin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("Role", "Admin");
    }));

// Add authentication services
builder.Services.AddAuthentication("Bearer").AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWTAuthenticationSettings:Issuer"],
            ValidAudience = builder.Configuration["JWTAuthenticationSettings:Audience"],
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWTAuthenticationSettings:SecretForKey"]))
        };
    });

// Configure DbContext with SQL Server (or your database provider)
builder.Services.AddDbContext<InfrastructureDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add other services (Swagger, API Explorer, etc.)
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = false;
}).AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ImageServiceInterface, CloudinaryImageService>();
builder.Services.AddScoped<GuestHelperMethods>();
//builder.Services.AddScoped<EmailServiceMethods>();


// Register custom services
builder.Services.AddApplicationServices().AddInfrastructureServices();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
