using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using Task.Core.Interfaces;
using Task.Core.Services;
using Task.Core.ServicesInterfaces;
using Task.Data;
using Task.Data.Repositres;

var builder = WebApplication.CreateBuilder(args);

//Add DbContext with MySQL 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WesIdentityContext>(options =>
            options.UseMySQL(connectionString));

// register the repositories
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRpository, UserRepository>();
builder.Services.AddScoped<ITanentRepository, TanentRepository>();
builder.Services.AddScoped<IRoleUserRepository, RoleUserRepository>();


// register the Services
builder.Services.AddScoped<IAddUserRolesRepository, AddUserRolesService>();

// Add services to the container.

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