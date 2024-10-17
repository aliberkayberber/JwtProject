using JwtExample.Context;
using JwtExample.Managers;
using JwtExample.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<JwtDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {

            ValidateIssuer = true, // Issuer validationý yap.
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // appsettingsteki deðer.
            ValidateAudience = true, // Audience validationý yap.
            ValidAudience = builder.Configuration["Jwt:Audience"], // appsettingsteki deðer
            ValidateLifetime = true, // Geçerlilik zamaný validationý yap.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)) //  appsettingsteki key.
        };
    });

builder.Services.AddScoped<IUserService, UserManager>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
