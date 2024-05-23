using AutoMapper;
using CarInfo.DataAccess.Domain.Interfaces;
using CarInfo.DataAccess.Persistence.Mapping;
using CarInfo.DataAccess.Persistence.Repository;
using CarInfo.DataAccess.Persistence.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarInfo.DataAccess.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using CarInfo.DataAccess.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CarInfoAdminAPI.Middleware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<ICarRepository, CarRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddScoped<MapperData>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyHeader();
});
app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
