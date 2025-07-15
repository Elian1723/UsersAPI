using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Automappers;
using UsersAPI.Data;
using UsersAPI.DTOs;
using UsersAPI.Repository;
using UsersAPI.Services;
using UsersAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// SqlServer and Entity Framework Core
builder.Services.AddDbContext<UsersAPIDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UsersAPIDbContext"))
);

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();

// Validators
builder.Services.AddScoped<IValidator<UserCreateDto>, UserCreateValidator>();
builder.Services.AddScoped<IValidator<UserUpdateDto>, UserUpdateValidator>();

// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.Run();
